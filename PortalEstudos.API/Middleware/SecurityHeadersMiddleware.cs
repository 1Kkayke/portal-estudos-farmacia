using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace PortalEstudos.API.Middleware;

/// <summary>
/// Middleware de segurança com headers de proteção contra ataques comuns
/// </summary>
public class SecurityHeadersMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    private readonly ILogger<SecurityHeadersMiddleware> _logger;

    public SecurityHeadersMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<SecurityHeadersMiddleware> logger)
    {
        _next = next;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var response = context.Response;

        // ═══════════════════════════════════════════════════════════
        // PROTEÇÃO CONTRA ATAQUES HTTP
        // ═══════════════════════════════════════════════════════════

        // Força HTTPS em produção
        if (context.Request.IsHttps || 
            context.Request.Headers.ContainsKey("X-Forwarded-Proto") && 
            context.Request.Headers["X-Forwarded-Proto"].ToString() == "https")
        {
            var hstsConfig = _configuration.GetSection("Security:EnableHSTS").Get<bool>();
            if (hstsConfig)
            {
                // HSTS: força HTTPS por 1 ano, incluindo subdomínios
                response.Headers["Strict-Transport-Security"] = "max-age=31536000; includeSubDomains; preload";
            }
        }

        // ═══════════════════════════════════════════════════════════
        // PROTEÇÃO AGAINST XSS (Cross-Site Scripting)
        // ═══════════════════════════════════════════════════════════
        response.Headers["X-Content-Type-Options"] = "nosniff";
        response.Headers["X-Frame-Options"] = "SAMEORIGIN";
        response.Headers["X-XSS-Protection"] = "1; mode=block";

        // ═══════════════════════════════════════════════════════════
        // CONTENT SECURITY POLICY (CSP)
        // ═══════════════════════════════════════════════════════════
        var cspEnabled = _configuration.GetSection("Security:EnableCSP").Get<bool>();
        if (cspEnabled)
        {
            var cspPolicy = _configuration.GetSection("Security:ContentSecurityPolicy").Get<string>() 
                ?? "default-src 'self'; script-src 'self'; style-src 'self' 'unsafe-inline'; img-src 'self' data: https:; font-src 'self' data:; connect-src 'self' https://www.youtube.com;";
            
            response.Headers["Content-Security-Policy"] = cspPolicy;
        }

        // ═══════════════════════════════════════════════════════════
        // CONTROLE DE REFERÊNCIA
        // ═══════════════════════════════════════════════════════════
        response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";

        // ═══════════════════════════════════════════════════════════
        // PERMISSÕES DO NAVEGADOR
        // ═══════════════════════════════════════════════════════════
        response.Headers["Permissions-Policy"] = 
            "geolocation=(), microphone=(), camera=(), payment=(), usb=(), magnetometer=(), gyroscope=(), accelerometer=()";

        // ═══════════════════════════════════════════════════════════
        // CACHE
        // ═══════════════════════════════════════════════════════════
        response.Headers.Remove("Cache-Control");
        
        // Instrui navegador não cachear dados sensíveis
        if (context.Request.Path.StartsWithSegments("/api"))
        {
            response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate, proxy-revalidate";
            response.Headers["Pragma"] = "no-cache";
            response.Headers["Expires"] = "0";
        }

        // ═══════════════════════════════════════════════════════════
        // IDENTIDADE DO SERVIDOR
        // ═══════════════════════════════════════════════════════════
        // Oculta informação do servidor
        response.Headers.Remove("Server");
        response.Headers.Remove("X-AspNet-Version");
        response.Headers.Remove("X-PoweredBy");

        // Log de requisições suspeitas
        var isSuspicious = DetectSuspiciousRequest(context.Request);
        if (isSuspicious)
        {
            _logger.LogWarning($"Requisição suspeita detectada: {context.Request.Method} {context.Request.Path} de {context.Connection.RemoteIpAddress}");
        }

        // Continua com próxima requisição
        await _next(context);
    }

    /// <summary>
    /// Detecta requisições suspeitas (SQL injection, XSS, etc)
    /// </summary>
    private bool DetectSuspiciousRequest(HttpRequest request)
    {
        // Padrões de SQL injection
        var sqlPatterns = new[] 
        { 
            @"('(\s|;|--)|union|select|insert|update|delete|drop|create|exec|execute|script|javascript|onerror|onclick)",
            @"(union.*select|select.*from|insert.*into|delete.*from|update.*set|drop.*table|create.*table)"
        };

        var queryString = request.QueryString.Value ?? string.Empty;
        var pathAndQuery = $"{request.Path}{request.QueryString}";

        foreach (var pattern in sqlPatterns)
        {
            if (Regex.IsMatch(pathAndQuery, pattern, RegexOptions.IgnoreCase))
            {
                return true;
            }
        }

        return false;
    }
}

/// <summary>
/// Extensão para facilitar uso do middleware
/// </summary>
public static class SecurityHeadersMiddlewareExtensions
{
    public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SecurityHeadersMiddleware>();
    }
}
