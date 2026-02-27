using System.Net;
using System.Net.Mail;

namespace PortalEstudos.API.Services;

public class EmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<bool> SendPasswordResetEmailAsync(string toEmail, string resetToken, string userName)
    {
        try
        {
            var smtpHost = _configuration["Email:SmtpHost"];
            var smtpPort = int.Parse(_configuration["Email:SmtpPort"] ?? "587");
            var smtpUser = _configuration["Email:SmtpUser"];
            var smtpPassword = _configuration["Email:SmtpPassword"];
            var fromEmail = _configuration["Email:FromEmail"];
            var fromName = _configuration["Email:FromName"] ?? "PharmStudy";

            // Validar configurações
            if (string.IsNullOrEmpty(smtpHost) || string.IsNullOrEmpty(smtpUser) || string.IsNullOrEmpty(smtpPassword))
            {
                _logger.LogWarning("Configurações de email não encontradas. Email não será enviado.");
                Console.WriteLine($"\n=== EMAIL DE RECUPERAÇÃO ===");
                Console.WriteLine($"Para: {toEmail}");
                Console.WriteLine($"Token: {resetToken}");
                Console.WriteLine($"===========================\n");
                return false;
            }

            // Criar URL de reset (ajuste conforme seu frontend)
            var resetUrl = $"http://localhost:5173/redefinir-senha?token={Uri.EscapeDataString(resetToken)}&email={Uri.EscapeDataString(toEmail)}";

            using var client = new SmtpClient(smtpHost, smtpPort)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(smtpUser, smtpPassword)
            };

            var message = new MailMessage
            {
                From = new MailAddress(fromEmail ?? smtpUser, fromName),
                Subject = "Recuperação de Senha - PharmStudy",
                Body = GenerateEmailBody(userName, resetUrl),
                IsBodyHtml = true
            };
            message.To.Add(toEmail);

            await client.SendMailAsync(message);
            _logger.LogInformation($"Email de recuperação enviado para {toEmail}");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erro ao enviar email para {toEmail}");
            
            // Em desenvolvimento, mostra no console mesmo se falhar
            if (_configuration["Environment"] == "Development")
            {
                Console.WriteLine($"\n=== FALHA AO ENVIAR EMAIL ===");
                Console.WriteLine($"Para: {toEmail}");
                Console.WriteLine($"Token: {resetToken}");
                Console.WriteLine($"Erro: {ex.Message}");
                Console.WriteLine($"==============================\n");
            }
            
            return false;
        }
    }

    private string GenerateEmailBody(string userName, string resetUrl)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1'>
</head>
<body style='font-family: Arial, sans-serif; line-height: 1.6; color: #333; max-width: 600px; margin: 0 auto; padding: 20px;'>
    <div style='background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); padding: 30px; text-align: center; border-radius: 10px 10px 0 0;'>
        <h1 style='color: white; margin: 0; font-size: 28px;'>🎓 PharmStudy</h1>
        <p style='color: rgba(255,255,255,0.9); margin: 10px 0 0 0;'>Portal de Estudos de Farmácia</p>
    </div>
    
    <div style='background: #f9f9f9; padding: 30px; border-radius: 0 0 10px 10px; border: 1px solid #e0e0e0; border-top: none;'>
        <h2 style='color: #667eea; margin-top: 0;'>Recuperação de Senha</h2>
        
        <p>Olá <strong>{userName}</strong>,</p>
        
        <p>Recebemos uma solicitação para redefinir a senha da sua conta no PharmStudy.</p>
        
        <p>Para criar uma nova senha, clique no botão abaixo:</p>
        
        <div style='text-align: center; margin: 30px 0;'>
            <a href='{resetUrl}' 
               style='background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); 
                      color: white; 
                      padding: 15px 40px; 
                      text-decoration: none; 
                      border-radius: 8px; 
                      font-weight: bold;
                      display: inline-block;
                      box-shadow: 0 4px 15px rgba(102, 126, 234, 0.4);'>
                Redefinir Senha
            </a>
        </div>
        
        <p style='color: #666; font-size: 14px;'>
            <strong>Importante:</strong> Este link expira em 24 horas por segurança.
        </p>
        
        <p style='color: #666; font-size: 14px;'>
            Se você não solicitou esta redefinição, ignore este email. Sua senha permanecerá a mesma.
        </p>
        
        <hr style='border: none; border-top: 1px solid #e0e0e0; margin: 30px 0;'>
        
        <p style='color: #999; font-size: 12px; text-align: center;'>
            Este é um email automático. Por favor, não responda.
        </p>
    </div>
</body>
</html>";
    }
}
