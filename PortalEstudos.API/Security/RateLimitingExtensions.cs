using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace PortalEstudos.API.Security;

/// <summary>
/// Extensão para rate limiting - proteção contra brute force e DDoS
/// </summary>
public static class RateLimitingExtensions
{
    public static IServiceCollection AddCustomRateLimiting(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var windowSeconds = configuration.GetValue<int>("RateLimiting:WindowSeconds", 60);
        var maxRequests = configuration.GetValue<int>("RateLimiting:MaxRequests", 100);

        services.AddRateLimiter(options =>
        {
            // ═══════════════════════════════════════════════════════════
            // RATE LIMITING GERAL - 100 requisições por minuto por IP
            // ═══════════════════════════════════════════════════════════
            options.AddFixedWindowLimiter("default", limiterOptions =>
            {
                limiterOptions.Window = TimeSpan.FromSeconds(windowSeconds);
                limiterOptions.PermitLimit = maxRequests;
                limiterOptions.QueueLimit = 0;
                limiterOptions.QueueProcessingOrder = QueueProcessingOrder.NewestFirst;
            });

            // ═══════════════════════════════════════════════════════════
            // RATE LIMITING PARA AUTENTICAÇÃO - 5 tentativas por minuto
            // ═══════════════════════════════════════════════════════════
            options.AddFixedWindowLimiter("auth", limiterOptions =>
            {
                limiterOptions.Window = TimeSpan.FromMinutes(1);
                limiterOptions.PermitLimit = 5;
                limiterOptions.QueueLimit = 0;
            });

            // ═══════════════════════════════════════════════════════════
            // RATE LIMITING PARA DOWNLOAD DE PDF - 20 por minuto
            // ═══════════════════════════════════════════════════════════
            options.AddFixedWindowLimiter("download", limiterOptions =>
            {
                limiterOptions.Window = TimeSpan.FromMinutes(1);
                limiterOptions.PermitLimit = 20;
                limiterOptions.QueueLimit = 0;
            });

            // ═══════════════════════════════════════════════════════════
            // RATE LIMITING PARA API - 50 requisições por minuto
            // ═══════════════════════════════════════════════════════════
            options.AddFixedWindowLimiter("api", limiterOptions =>
            {
                limiterOptions.Window = TimeSpan.FromMinutes(1);
                limiterOptions.PermitLimit = 50;
                limiterOptions.QueueLimit = 0;
            });

            // Comportamento quando limite é atingido
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            // Key provider - usa IP como identificador padrão
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
            {
                var remoteIp = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
                return RateLimitPartition.GetFixedWindowLimiter(remoteIp, key => new FixedWindowRateLimiterOptions
                {
                    Window = TimeSpan.FromSeconds(windowSeconds),
                    PermitLimit = maxRequests,
                    QueueLimit = 0,
                    QueueProcessingOrder = QueueProcessingOrder.NewestFirst
                });
            });
        });

        return services;
    }
}
