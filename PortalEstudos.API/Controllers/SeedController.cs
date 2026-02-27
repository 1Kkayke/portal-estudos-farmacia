using Microsoft.AspNetCore.Mvc;
using PortalEstudos.API.Data;

namespace PortalEstudos.API.Controllers;

/// <summary>
/// Controlador para seeding de dados. 
/// Protegido por chave secreta de administração.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SeedController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IConfiguration _config;
    private readonly ILogger<SeedController> _logger;

    public SeedController(ApplicationDbContext db, IConfiguration config, ILogger<SeedController> logger)
    {
        _db = db;
        _config = config;
        _logger = logger;
    }

    /// <summary>
    /// Executa o seeding de conteúdo estudado (42 tópicos, 840 documentos, etc)
    /// Requer header: X-Admin-Key com valor correto
    /// </summary>
    [HttpPost("content")]
    public ActionResult<object> SeedContent([FromHeader(Name = "X-Admin-Key")] string adminKey)
    {
        try
        {
            // Validar chave de administração
            var expectedKey = _config["AdminKey"] ?? _config["JWT_SECRET_KEY"] ?? "default-key";
            
            if (string.IsNullOrEmpty(adminKey) || !adminKey.Equals(expectedKey, StringComparison.Ordinal))
            {
                _logger.LogWarning("Tentativa de seeding com chave inválida");
                return Unauthorized(new { message = "Chave de administração inválida" });
            }

            // Verificar se já existe conteúdo
            if (_db.Topics.Any())
            {
                _logger.LogInformation("Conteúdo já foi seeded anteriormente");
                return Ok(new { message = "Conteúdo já existe no banco de dados", skipped = true });
            }

            _logger.LogInformation("Iniciando seeding de conteúdo...");

            // Fazer o seeding
            StudyContentSeeder.EnsureSeeded(_db);

            var topicCount = _db.Topics.Count();
            var docCount = _db.Documents.Count();
            var questionCount = _db.Questions.Count();

            _logger.LogInformation($"Seeding concluído: {topicCount} tópicos, {docCount} documentos, {questionCount} questões");

            return Ok(new
            {
                message = "Seeding concluído com sucesso",
                stats = new
                {
                    topics = topicCount,
                    documents = docCount,
                    questions = questionCount
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro durante seeding de conteúdo");
            return StatusCode(500, new { message = "Erro durante seeding", error = ex.Message });
        }
    }

    /// <summary>
    /// Retorna status atual do banco de dados
    /// </summary>
    [HttpGet("status")]
    public ActionResult<object> GetStatus()
    {
        try
        {
            var topicCount = _db.Topics.Count();
            var docCount = _db.Documents.Count();
            var questionCount = _db.Questions.Count();
            var userCount = _db.Users.Count();

            return Ok(new
            {
                database = new
                {
                    topics = topicCount,
                    documents = docCount,
                    questions = questionCount,
                    users = userCount
                },
                needsSeeding = topicCount == 0
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter status do banco");
            return StatusCode(500, new { message = "Erro ao obter status", error = ex.Message });
        }
    }
}
