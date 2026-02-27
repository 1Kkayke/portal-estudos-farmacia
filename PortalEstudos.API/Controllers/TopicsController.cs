using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalEstudos.API.Data;
using PortalEstudos.API.DTOs;

namespace PortalEstudos.API.Controllers
{
    /// <summary>
    /// Controller de tópicos de estudo.
    /// Lista os tópicos pré-carregados de Farmácia, incluindo a contagem 
    /// de anotações que o usuário logado possui em cada tópico.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Requer autenticação JWT
    public class TopicsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TopicsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET /api/topics
        /// Retorna todos os tópicos com a contagem de notas do usuário logado.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TopicDto>>> GetTopics()
        {
            // Recupera o ID do usuário a partir do token JWT
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var topics = await _context.Topics
                .Select(t => new TopicDto
                {
                    Id = t.Id,
                    Nome = t.Nome,
                    Descricao = t.Descricao,
                    Categoria = t.Categoria,
                    Icone = t.Icone,
                    Cor = t.Cor,
                    TotalNotas = t.Notes.Count(n => n.UserId == userId)
                })
                .ToListAsync();

            return Ok(topics);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TopicDto>> GetTopic(int id)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var topic = await _context.Topics
                .Where(t => t.Id == id)
                .Select(t => new TopicDto
                {
                    Id = t.Id,
                    Nome = t.Nome,
                    Descricao = t.Descricao,
                    Categoria = t.Categoria,
                    Icone = t.Icone,
                    Cor = t.Cor,
                    TotalNotas = t.Notes.Count(n => n.UserId == userId)
                })
                .FirstOrDefaultAsync();

            if (topic == null) return NotFound();
            return Ok(topic);
        }
    }
}
