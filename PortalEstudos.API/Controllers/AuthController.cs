using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PortalEstudos.API.DTOs;
using PortalEstudos.API.Models;
using PortalEstudos.API.Services;

namespace PortalEstudos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TokenService _tokenService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            UserManager<ApplicationUser> userManager, 
            TokenService tokenService,
            ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            {
                var result = await ProcessUserRegistrationAsync(dto);
                return result.IsSuccess 
                    ? Ok(result.Data) 
                    : BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro durante registro do usuário: {Email}", dto.Email);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                var result = await ProcessUserLoginAsync(dto);
                return result.IsSuccess 
                    ? Ok(result.Data) 
                    : Unauthorized("Credenciais inválidas");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro durante login do usuário: {Email}", dto.Email);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        private async Task<AuthResult> ProcessUserRegistrationAsync(RegisterDto dto)
        {
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                return AuthResult.Failure("Email já cadastrado no sistema");
            }

            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                NomeCompleto = dto.NomeCompleto
            };

            var createResult = await _userManager.CreateAsync(user, dto.Password);
            if (!createResult.Succeeded)
            {
                return AuthResult.Failure(createResult.Errors.Select(e => e.Description));
            }

            var token = await _tokenService.GenerateToken(user);
            var authResponse = CreateAuthResponse(user, token);

            _logger.LogInformation("Usuário registrado com sucesso: {Email}", dto.Email);
            return AuthResult.Success(authResponse);
        }

        private async Task<AuthResult> ProcessUserLoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return AuthResult.Failure("Usuário não encontrado");
            }

            var isValidPassword = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!isValidPassword)
            {
                return AuthResult.Failure("Senha inválida");
            }

            var token = await _tokenService.GenerateToken(user);
            var authResponse = CreateAuthResponse(user, token);

            _logger.LogInformation("Login realizado com sucesso: {Email}", dto.Email);
            return AuthResult.Success(authResponse);
        }

        private static AuthResponseDto CreateAuthResponse(ApplicationUser user, string token)
        {
            return new AuthResponseDto
            {
                Token = token,
                Email = user.Email!,
                NomeCompleto = user.NomeCompleto,
                Expiration = DateTime.UtcNow.AddHours(24),
                Telefone = user.Telefone,
                DataNascimento = user.DataNascimento,
                Bio = user.Bio,
                FotoPerfilUrl = user.FotoPerfilUrl
            };
        }

        private class AuthResult
        {
            public bool IsSuccess { get; private set; }
            public AuthResponseDto? Data { get; private set; }
            public IEnumerable<string> Errors { get; private set; } = Enumerable.Empty<string>();

            public static AuthResult Success(AuthResponseDto data) => new()
            {
                IsSuccess = true,
                Data = data
            };

            public static AuthResult Failure(string error) => new()
            {
                IsSuccess = false,
                Errors = new[] { error }
            };

            public static AuthResult Failure(IEnumerable<string> errors) => new()
            {
                IsSuccess = false,
                Errors = errors
            };
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
