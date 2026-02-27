using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalEstudos.API.Data;
using PortalEstudos.API.DTOs;
using PortalEstudos.API.Services;

namespace PortalEstudos.API.Controllers;

[ApiController]
[Route("api/topics/{topicId}/documents")]
[Authorize]
public class DocumentsController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly DocumentPdfService _pdf;
    public DocumentsController(ApplicationDbContext db, DocumentPdfService pdf)
    {
        _db = db;
        _pdf = pdf;
    }

    private static readonly string[] PagesA =
    {
        "/docs/templates/page-1.svg",
        "/docs/templates/page-2.svg",
        "/docs/templates/page-3.svg",
        "/docs/templates/page-4.svg",
        "/docs/templates/page-5.svg"
    };

    private static readonly string[] PagesB =
    {
        "/docs/templates/page-1.svg",
        "/docs/templates/page-6.svg",
        "/docs/templates/page-7.svg",
        "/docs/templates/page-3.svg",
        "/docs/templates/page-5.svg"
    };

    private static string GetCoverUrl(int topicId)
    {
        var idx = (topicId % 4) + 1;
        return $"/docs/templates/cover-{idx}.svg";
    }

    private static List<string> GetPages(int topicId)
    {
        return (topicId % 2 == 0 ? PagesA : PagesB).ToList();
    }

    /// <summary>Lista todos os documentos de um tópico (sem conteúdo completo).</summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DocumentListDto>>> GetAll(int topicId)
    {
        var docs = await _db.Documents
            .Where(d => d.TopicId == topicId)
            .OrderBy(d => d.Ordem)
            .Select(d => new DocumentListDto
            {
                Id = d.Id,
                Titulo = d.Titulo,
                Resumo = d.Resumo,
                Ordem = d.Ordem,
                Dificuldade = d.Dificuldade,
                LeituraMinutos = d.LeituraMinutos,
                CapaUrl = GetCoverUrl(d.TopicId),
                PdfUrl = $"/api/topics/{topicId}/documents/{d.Id}/pdf"
            })
            .ToListAsync();
        return Ok(docs);
    }

    /// <summary>Retorna um documento completo.</summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<DocumentDto>> GetById(int topicId, int id)
    {
        var doc = await _db.Documents
            .Where(d => d.TopicId == topicId && d.Id == id)
            .Select(d => new DocumentDto
            {
                Id = d.Id,
                Titulo = d.Titulo,
                Resumo = d.Resumo,
                Conteudo = d.Conteudo,
                Ordem = d.Ordem,
                Dificuldade = d.Dificuldade,
                LeituraMinutos = d.LeituraMinutos,
                TopicId = d.TopicId,
                CapaUrl = GetCoverUrl(d.TopicId),
                Paginas = GetPages(d.TopicId),
                PdfUrl = $"/api/topics/{topicId}/documents/{d.Id}/pdf"
            })
            .FirstOrDefaultAsync();

        if (doc is null) return NotFound();
        return Ok(doc);
    }

    /// <summary>Retorna PDF do documento.</summary>
    [HttpGet("{id}/pdf")]
    public async Task<IActionResult> GetPdf(int topicId, int id)
    {
        var doc = await _db.Documents
            .Include(d => d.Topic)
            .FirstOrDefaultAsync(d => d.TopicId == topicId && d.Id == id);

        if (doc is null) return NotFound();

        var bytes = _pdf.BuildPdf(doc, doc.Topic.Nome, doc.Topic.Categoria);
        
        // Normaliza o nome do arquivo removendo caracteres especiais
        var topicNameClean = string.Join("", doc.Topic.Nome.Split(Path.GetInvalidFileNameChars()));
        var docTitleClean = string.Join("", doc.Titulo.Split(Path.GetInvalidFileNameChars()));
        var fileName = $"{topicNameClean} - {docTitleClean}.pdf";
        
        return File(bytes, "application/pdf", fileName);
    }
}
