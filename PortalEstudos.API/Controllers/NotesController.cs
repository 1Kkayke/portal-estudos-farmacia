using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalEstudos.API.Data;
using PortalEstudos.API.DTOs;
using PortalEstudos.API.Models;
using System.Security.Claims;

namespace PortalEstudos.API.Controllers
{
    /// <summary>
    /// Controller de anotações (Notes).
    /// Implementa CRUD completo, filtrando SEMPRE pelo UserId do token JWT,
    /// garantindo que cada aluno veja apenas suas próprias anotações.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Requer autenticação JWT em todos os endpoints
    public class NotesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Extrai o UserId do token JWT de forma centralizada.
        /// </summary>
        private string GetUserId() =>
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? throw new UnauthorizedAccessException("Usuário não identificado.");

        /// <summary>
        /// GET /api/notes?topicId=1
        /// Lista as anotações do usuário logado, opcionalmente filtradas por tópico.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteDto>>> GetNotes([FromQuery] int? topicId)
        {
            var userId = GetUserId();

            // Query base: sempre filtra pelo usuário logado
            var query = _context.Notes
                .Include(n => n.Topic)
                .Where(n => n.UserId == userId);

            // Filtro opcional por tópico
            if (topicId.HasValue)
                query = query.Where(n => n.TopicId == topicId.Value);

            var notes = await query
                .OrderByDescending(n => n.DataAtualizacao) // Mais recentes primeiro
                .Select(n => new NoteDto
                {
                    Id = n.Id,
                    Titulo = n.Titulo,
                    Conteudo = n.Conteudo,
                    DataCriacao = n.DataCriacao,
                    DataAtualizacao = n.DataAtualizacao,
                    TopicId = n.TopicId,
                    TopicNome = n.Topic.Nome
                })
                .ToListAsync();

            return Ok(notes);
        }

        /// <summary>
        /// GET /api/notes/{id}
        /// Retorna uma anotação específica (somente se pertencer ao usuário logado).
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<NoteDto>> GetNote(int id)
        {
            var userId = GetUserId();

            var note = await _context.Notes
                .Include(n => n.Topic)
                .Where(n => n.Id == id && n.UserId == userId) // Filtra por ID e USER
                .Select(n => new NoteDto
                {
                    Id = n.Id,
                    Titulo = n.Titulo,
                    Conteudo = n.Conteudo,
                    DataCriacao = n.DataCriacao,
                    DataAtualizacao = n.DataAtualizacao,
                    TopicId = n.TopicId,
                    TopicNome = n.Topic.Nome
                })
                .FirstOrDefaultAsync();

            if (note == null) return NotFound();
            return Ok(note);
        }

        /// <summary>
        /// POST /api/notes
        /// Cria uma nova anotação vinculada ao usuário logado e ao tópico escolhido.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<NoteDto>> CreateNote([FromBody] CreateNoteDto dto)
        {
            var userId = GetUserId();

            // Verifica se o tópico existe
            var topicExists = await _context.Topics.AnyAsync(t => t.Id == dto.TopicId);
            if (!topicExists) return BadRequest("Tópico não encontrado.");

            var note = new Note
            {
                Titulo = dto.Titulo,
                Conteudo = dto.Conteudo,
                TopicId = dto.TopicId,
                UserId = userId,
                DataCriacao = DateTime.UtcNow,
                DataAtualizacao = DateTime.UtcNow
            };

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            // Carrega o relacionamento com Topic para retornar o nome
            await _context.Entry(note).Reference(n => n.Topic).LoadAsync();

            var result = new NoteDto
            {
                Id = note.Id,
                Titulo = note.Titulo,
                Conteudo = note.Conteudo,
                DataCriacao = note.DataCriacao,
                DataAtualizacao = note.DataAtualizacao,
                TopicId = note.TopicId,
                TopicNome = note.Topic.Nome
            };

            return CreatedAtAction(nameof(GetNote), new { id = note.Id }, result);
        }

        /// <summary>
        /// PUT /api/notes/{id}
        /// Atualiza uma anotação existente (somente se pertencer ao usuário logado).
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(int id, [FromBody] UpdateNoteDto dto)
        {
            var userId = GetUserId();

            // Busca a nota filtrando pelo usuário — impede edição de notas alheias
            var note = await _context.Notes
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);

            if (note == null) return NotFound();

            note.Titulo = dto.Titulo;
            note.Conteudo = dto.Conteudo;
            note.DataAtualizacao = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// DELETE /api/notes/{id}
        /// Exclui uma anotação (somente se pertencer ao usuário logado).
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var userId = GetUserId();

            var note = await _context.Notes
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);

            if (note == null) return NotFound();

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
