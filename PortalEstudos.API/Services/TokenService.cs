using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PortalEstudos.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PortalEstudos.API.Services
{
    /// <summary>
    /// Serviço responsável pela geração de tokens JWT.
    /// Centraliza a lógica de criação de tokens para reutilização.
    /// </summary>
    public class TokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public TokenService(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        /// <summary>
        /// Gera um token JWT para o usuário autenticado.
        /// Inclui claims de ID, email e nome.
        /// </summary>
        public async Task<string> GenerateToken(ApplicationUser user)
        {
            // Claims são informações embutidas no token que identificam o usuário
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Name, user.NomeCompleto),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Recupera a chave secreta com fallback para variável de ambiente
            var jwtKey = _configuration["JWT_SECRET_KEY"] ?? _configuration["Jwt:Key"];
            if (string.IsNullOrWhiteSpace(jwtKey))
                throw new InvalidOperationException("JWT Key não configurada");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Cria o token com validade de 24 horas
            var issuer = _configuration["Jwt:Issuer"] ?? "PortalEstudos.API";
            var audience = _configuration["Jwt:Audience"] ?? "PortalEstudos.Client";

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
