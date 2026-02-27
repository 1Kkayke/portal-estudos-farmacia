using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalEstudos.API.Data;
using PortalEstudos.API.DTOs;

namespace PortalEstudos.API.Controllers;

[ApiController]
[Route("api/topics/{topicId}/documents")]
[Authorize]
public class DocumentsController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public DocumentsController(ApplicationDbContext db) => _db = db;

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
            })
            .FirstOrDefaultAsync();

        if (doc is null) return NotFound();
        return Ok(doc);
    }
}
