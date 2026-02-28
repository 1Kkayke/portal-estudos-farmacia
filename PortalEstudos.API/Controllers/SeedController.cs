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

            // Se já existe conteúdo, faz refresh dos HTMLs das apostilas
            if (_db.Topics.Any())
            {
                var updated = StudyContentSeeder.RefreshDocumentContents(_db);
                _logger.LogInformation("Conteúdo já existente detectado. Apostilas atualizadas: {Updated}", updated);
                return Ok(new
                {
                    message = "Conteúdo já existia; apostilas atualizadas com sucesso",
                    skippedSeed = true,
                    refreshedDocuments = updated
                });
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
    /// Atualiza o conteúdo HTML das apostilas já existentes sem apagar dados do banco.
    /// Requer header: X-Admin-Key com valor correto.
    /// </summary>
    [HttpPost("refresh-documents")]
    public ActionResult<object> RefreshDocuments([FromHeader(Name = "X-Admin-Key")] string adminKey)
    {
        try
        {
            var expectedKey = _config["AdminKey"] ?? _config["JWT_SECRET_KEY"] ?? "default-key";

            if (string.IsNullOrEmpty(adminKey) || !adminKey.Equals(expectedKey, StringComparison.Ordinal))
            {
                _logger.LogWarning("Tentativa de refresh de apostilas com chave inválida");
                return Unauthorized(new { message = "Chave de administração inválida" });
            }

            if (!_db.Documents.Any())
            {
                return Ok(new { message = "Nenhuma apostila encontrada para atualização", updated = 0 });
            }

            var updated = StudyContentSeeder.RefreshDocumentContents(_db);

            return Ok(new
            {
                message = "Apostilas atualizadas com sucesso",
                updated
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar apostilas");
            return StatusCode(500, new { message = "Erro ao atualizar apostilas", error = ex.Message });
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
