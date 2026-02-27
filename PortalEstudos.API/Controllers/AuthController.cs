using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PortalEstudos.API.DTOs;
using PortalEstudos.API.Models;
using PortalEstudos.API.Services;

namespace PortalEstudos.API.Controllers
{
    /// <summary>
    /// Controller de autenticação: registro e login de usuários.
    /// Retorna um token JWT válido para autenticar chamadas subsequentes.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TokenService _tokenService;

        public AuthController(UserManager<ApplicationUser> userManager, TokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        /// <summary>
        /// POST /api/auth/register
        /// Registra um novo estudante no sistema.
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            // Cria o objeto de usuário com os dados fornecidos
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                NomeCompleto = dto.NomeCompleto
            };

            // Tenta criar o usuário com a senha informada
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                // Retorna os erros de validação do Identity (senha fraca, email duplicado, etc.)
                return BadRequest(result.Errors.Select(e => e.Description));
            }

            // Gera o token JWT imediatamente após o registro
            var token = await _tokenService.GenerateToken(user);

            return Ok(new AuthResponseDto
            {
                Token = token,
                Email = user.Email!,
                NomeCompleto = user.NomeCompleto,
                Expiration = DateTime.UtcNow.AddHours(24)
            });
        }

        /// <summary>
        /// POST /api/auth/login
        /// Autentica um estudante e retorna o token JWT.
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            // Busca o usuário pelo email
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return Unauthorized("Credenciais inválidas.");

            // Verifica a senha
            var validPassword = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!validPassword)
                return Unauthorized("Credenciais inválidas.");

            // Gera o token JWT
            var token = await _tokenService.GenerateToken(user);

            return Ok(new AuthResponseDto
            {
                Token = token,
                Email = user.Email!,
                NomeCompleto = user.NomeCompleto,
                Expiration = DateTime.UtcNow.AddHours(24)
            });
        }
    }
}
