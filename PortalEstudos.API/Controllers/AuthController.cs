using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
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
                Expiration = DateTime.UtcNow.AddHours(24),
                Telefone = user.Telefone,
                DataNascimento = user.DataNascimento,
                Bio = user.Bio,
                FotoPerfilUrl = user.FotoPerfilUrl
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
                Expiration = DateTime.UtcNow.AddHours(24),
                Telefone = user.Telefone,
                DataNascimento = user.DataNascimento,
                Bio = user.Bio,
                FotoPerfilUrl = user.FotoPerfilUrl
            });
        }

        /// <summary>
        /// PUT /api/auth/perfil
        /// Atualiza os dados do perfil do usuário autenticado.
        /// </summary>
        [Authorize]
        [HttpPut("perfil")]
        public async Task<IActionResult> UpdateProfile([FromForm] UpdateProfileDto dto)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound("Usuário não encontrado.");

            // Atualiza os dados
            user.NomeCompleto = dto.NomeCompleto;
            
            // Só atualiza email e username se email foi fornecido e é diferente do atual
            if (!string.IsNullOrWhiteSpace(dto.Email) && dto.Email != user.Email)
            {
                user.Email = dto.Email;
                user.UserName = dto.Email;
            }
            
            user.Telefone = dto.Telefone;
            user.DataNascimento = dto.DataNascimento;
            user.Bio = dto.Bio;

            // Upload de foto de perfil
            if (dto.FotoPerfil != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "profiles");
                Directory.CreateDirectory(uploadsFolder);

                var fileName = $"{Guid.NewGuid()}_{dto.FotoPerfil.FileName}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.FotoPerfil.CopyToAsync(stream);
                }

                user.FotoPerfilUrl = $"/uploads/profiles/{fileName}";
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(result.Errors.Select(e => e.Description));

            return Ok(new
            {
                nomeCompleto = user.NomeCompleto,
                email = user.Email,
                telefone = user.Telefone,
                dataNascimento = user.DataNascimento,
                bio = user.Bio,
                fotoPerfilUrl = user.FotoPerfilUrl
            });
        }

        /// <summary>
        /// PUT /api/auth/alterar-senha
        /// Altera a senha do usuário autenticado.
        /// </summary>
        [Authorize]
        [HttpPut("alterar-senha")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound("Usuário não encontrado.");

            // Verifica a senha atual
            var validPassword = await _userManager.CheckPasswordAsync(user, dto.SenhaAtual);
            if (!validPassword)
                return Unauthorized(new { message = "Senha atual incorreta." });

            // Altera a senha
            var result = await _userManager.ChangePasswordAsync(user, dto.SenhaAtual, dto.NovaSenha);
            if (!result.Succeeded)
                return BadRequest(result.Errors.Select(e => e.Description));

            return Ok(new { message = "Senha alterada com sucesso." });
        }

        /// <summary>
        /// POST /api/auth/recuperar-senha
        /// Envia um email de recuperação de senha.
        /// </summary>
        [HttpPost("recuperar-senha")]
        public async Task<IActionResult> RecoverPassword([FromBody] RecoverPasswordDto dto, [FromServices] EmailService emailService)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                // Por segurança, não informamos se o email existe ou não
                return Ok(new { message = "Se o email existir, você receberá instruções de recuperação." });

            // Gera token de recuperação
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Envia email de recuperação
            await emailService.SendPasswordResetEmailAsync(user.Email!, token, user.NomeCompleto);

            return Ok(new { message = "Email de recuperação enviado com sucesso." });
        }

        /// <summary>
        /// PUT /api/auth/redefinir-senha
        /// Redefine a senha do usuário usando o token de recuperação.
        /// </summary>
        [HttpPut("redefinir-senha")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return BadRequest(new { message = "Usuário não encontrado.", code = "USER_NOT_FOUND" });

            var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NovaSenha);
            
            if (!result.Succeeded)
            {
                // Verifica se o erro é de token inválido/expirado
                var errors = result.Errors.Select(e => e.Code).ToList();
                
                if (errors.Contains("InvalidToken"))
                {
                    return BadRequest(new { 
                        message = "Este link de recuperação expirou (24 horas) ou já foi utilizado. Solicite um novo link.",
                        code = "TOKEN_EXPIRED_OR_USED"
                    });
                }
                
                return BadRequest(new { 
                    message = "Não foi possível redefinir a senha. Verifique os dados e tente novamente.",
                    code = "RESET_FAILED",
                    errors = result.Errors 
                });
            }

            return Ok(new { message = "Senha redefinida com sucesso." });
        }
    }
}
