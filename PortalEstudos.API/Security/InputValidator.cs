using System.Text.RegularExpressions;
using System.Net;

namespace PortalEstudos.API.Security;

/// <summary>
/// Validação e sanitização de entrada para prevenção de XSS, SQL Injection, etc
/// </summary>
public static class InputValidator
{
    // ═══════════════════════════════════════════════════════════
    // REGEX PATTERNS PARA VALIDAÇÃO
    // ═══════════════════════════════════════════════════════════

    private static readonly Regex SqlInjectionPattern = new(
        @"(union|select|insert|update|delete|drop|create|exec|execute|script|javascript|onerror|onclick|eval|alert|prompt|confirm)",
        RegexOptions.IgnoreCase | RegexOptions.Compiled
    );

    private static readonly Regex XssPattern = new(
        @"(<script|javascript:|onerror|onclick|onload|eval\()",
        RegexOptions.IgnoreCase | RegexOptions.Compiled
    );

    private static readonly Regex EmailPattern = new(
        @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$",
        RegexOptions.Compiled
    );

    private static readonly Regex UrlPattern = new(
        @"^(https?|ftp)://[^\s/$.?#].[^\s]*$",
        RegexOptions.IgnoreCase | RegexOptions.Compiled
    );

    private static readonly Regex HtmlTagPattern = new(
        @"<[^>]+>",
        RegexOptions.Compiled
    );

    // ═══════════════════════════════════════════════════════════
    // VALIDAÇÃO E SANITIZAÇÃO GENÉRICA
    // ═══════════════════════════════════════════════════════════

    /// <summary>
    /// Valida se a string contém padrões de SQL Injection
    /// </summary>
    public static bool IsSuspiciousSql(string? input)
    {
        if (string.IsNullOrEmpty(input)) return false;
        return SqlInjectionPattern.IsMatch(input);
    }

    /// <summary>
    /// Valida se a string contém padrões de XSS
    /// </summary>
    public static bool IsSuspiciousXss(string? input)
    {
        if (string.IsNullOrEmpty(input)) return false;
        return XssPattern.IsMatch(input);
    }

    /// <summary>
    /// Sanitiza string remova tags HTML e scripts
    /// </summary>
    public static string SanitizeHtml(string? input)
    {
        if (string.IsNullOrEmpty(input)) return string.Empty;

        // Remove tags HTML
        var withoutTags = HtmlTagPattern.Replace(input, " ");
        
        // Decode HTML entities perigosas
        var decoded = WebUtility.HtmlDecode(withoutTags);
        
        // Remove caracteres de controle
        decoded = Regex.Replace(decoded, @"[\x00-\x08\x0B\x0C\x0E-\x1F]", "");
        
        return decoded.Trim();
    }

    /// <summary>
    /// Valida e sanitiza entrada de usuário genérica
    /// </summary>
    public static (bool isValid, string sanitized) ValidateAndSanitizeInput(
        string? input, 
        int minLength = 0,
        int maxLength = 1000,
        bool allowHtml = false
    )
    {
        if (input == null)
            return (false, string.Empty);

        // Verifica comprimento
        if (input.Length < minLength || input.Length > maxLength)
            return (false, string.Empty);

        // Verifica padrões suspeitos
        if (IsSuspiciousSql(input) || (allowHtml == false && IsSuspiciousXss(input)))
            return (false, string.Empty);

        // Sanitiza
        var sanitized = allowHtml ? input : SanitizeHtml(input);

        return (true, sanitized);
    }

    // ═══════════════════════════════════════════════════════════
    // VALIDAÇÕES ESPECÍFICAS
    // ═══════════════════════════════════════════════════════════

    /// <summary>
    /// Valida email
    /// </summary>
    public static bool IsValidEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        if (email.Length > 254)
            return false;

        return EmailPattern.IsMatch(email);
    }

    /// <summary>
    /// Valida senha (força mínima)
    /// </summary>
    public static bool IsValidPassword(string? password)
    {
        if (string.IsNullOrEmpty(password))
            return false;

        // Mínimo 8 caracteres
        if (password.Length < 8)
            return false;

        // Deve ter maiúscula
        if (!Regex.IsMatch(password, @"[A-Z]"))
            return false;

        // Deve ter minúscula
        if (!Regex.IsMatch(password, @"[a-z]"))
            return false;

        // Deve ter número
        if (!Regex.IsMatch(password, @"[0-9]"))
            return false;

        // Deve ter caractere especial
        if (!Regex.IsMatch(password, @"[@$!%*?&]"))
            return false;

        return true;
    }

    /// <summary>
    /// Valida URL
    /// </summary>
    public static bool IsValidUrl(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return false;

        return UrlPattern.IsMatch(url);
    }

    /// <summary>
    /// Valida ID numérico
    /// </summary>
    public static bool IsValidId(int id)
    {
        return id > 0;
    }

    /// <summary>
    /// Valida ID numérico aceitar 0
    /// </summary>
    public static bool IsValidIdOrZero(int id)
    {
        return id >= 0;
    }

    /// <summary>
    /// Sanitiza nome de arquivo
    /// </summary>
    public static string SanitizeFileName(string? fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            return "file";

        // Remove caracteres inválidos
        var invalid = new string(Path.GetInvalidFileNameChars());
        foreach (var c in invalid)
        {
            fileName = fileName.Replace(c.ToString(), "");
        }

        // Remove espaços múltiplos
        fileName = Regex.Replace(fileName, @"\s+", " ");

        // Limita comprimento
        return fileName.Substring(0, Math.Min(255, fileName.Length)).Trim();
    }

    /// <summary>
    /// Valida texto contém apenas caracteres permitidos
    /// </summary>
    public static bool IsValidText(string? text, int maxLength = 5000)
    {
        if (string.IsNullOrWhiteSpace(text))
            return false;

        if (text.Length > maxLength)
            return false;

        // Verifica caracteres de controle
        if (Regex.IsMatch(text, @"[\x00-\x08\x0B\x0C\x0E-\x1F\x7F]"))
            return false;

        return true;
    }

    /// <summary>
    /// Extrai apenas números de string
    /// </summary>
    public static string ExtractNumbers(string? input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        return Regex.Replace(input, @"[^\d]", "");
    }

    /// <summary>
    /// Sanitiza nome de usuário
    /// </summary>
    public static (bool isValid, string sanitized) ValidateAndSanitizeUsername(string? username)
    {
        if (string.IsNullOrWhiteSpace(username))
            return (false, string.Empty);

        if (username.Length < 3 || username.Length > 50)
            return (false, string.Empty);

        // Apenas letras, números, underscore, hífen
        if (!Regex.IsMatch(username, @"^[a-zA-Z0-9_-]+$"))
            return (false, string.Empty);

        return (true, username);
    }
}
