using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalEstudos.API.Data;
using PortalEstudos.API.DTOs;
using PortalEstudos.API.Models;
using System.Security.Claims;

namespace PortalEstudos.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserTopicsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserTopicsController(ApplicationDbContext context)
        {
            _context = context;
        }

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        /// <summary>
        /// GET /api/usertopics/favorites
        /// Retorna as matérias favoritas do usuário
        /// </summary>
        [HttpGet("favorites")]
        public async Task<IActionResult> GetFavorites()
        {
            var userId = GetUserId();

            var favorites = await _context.UserTopicInterests
                .Where(x => x.UserId == userId)
                .Include(x => x.Topic)
                    .ThenInclude(t => t.Notes)
                .OrderByDescending(x => x.DataMarcacao)
                .Select(x => new TopicCardDto
                {
                    Id = x.Topic.Id,
                    Nome = x.Topic.Nome,
                    Descricao = x.Topic.Descricao,
                    Categoria = x.Topic.Categoria,
                    Icone = x.Topic.Icone,
                    Cor = x.Topic.Cor,
                    TotalNotas = x.Topic.Notes.Count,
                    IsFavorite = true
                })
                .ToListAsync();

            return Ok(favorites);
        }

        /// <summary>
        /// GET /api/usertopics/recent
        /// Retorna as matérias recentemente estudadas (apenas a atividade mais recente de cada tópico)
        /// </summary>
        [HttpGet("recent")]
        public async Task<IActionResult> GetRecentStudies()
        {
            var userId = GetUserId();

            // Passo 1: Pega os IDs da atividade mais recente de cada tópico
            var recentActivityIds = await _context.UserTopicActivities
                .Where(x => x.UserId == userId)
                .GroupBy(x => x.TopicId)
                .Select(g => g.OrderByDescending(x => x.UltimoAcesso).First().Id)
                .ToListAsync();

            // Passo 2: Carrega os registros completos com os tópicos
            var recent = await _context.UserTopicActivities
                .Where(x => recentActivityIds.Contains(x.Id))
                .Include(x => x.Topic)
                .OrderByDescending(x => x.UltimoAcesso)
                .Take(5)
                .Select(x => new TopicActivityDto
                {
                    TopicId = x.Topic.Id,
                    TopicNome = x.Topic.Nome,
                    TopicCategoria = x.Topic.Categoria,
                    TopicIcone = x.Topic.Icone,
                    TopicCor = x.Topic.Cor,
                    UltimoAcesso = x.UltimoAcesso,
                    TotalAcessos = x.TotalAcessos
                })
                .ToListAsync();

            return Ok(recent);
        }

        /// <summary>
        /// POST /api/usertopics/favorites/{topicId}
        /// Adiciona ou remove uma matéria dos favoritos
        /// </summary>
        [HttpPost("favorites/{topicId}")]
        public async Task<IActionResult> ToggleFavorite(int topicId)
        {
            var userId = GetUserId();

            var topic = await _context.Topics.FindAsync(topicId);
            if (topic == null)
                return NotFound(new { message = "Matéria não encontrada." });

            var existing = await _context.UserTopicInterests
                .FirstOrDefaultAsync(x => x.UserId == userId && x.TopicId == topicId);

            if (existing != null)
            {
                // Remove dos favoritos
                _context.UserTopicInterests.Remove(existing);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Removido dos favoritos.", isFavorite = false });
            }
            else
            {
                // Adiciona aos favoritos
                _context.UserTopicInterests.Add(new UserTopicInterest
                {
                    UserId = userId,
                    TopicId = topicId,
                    DataMarcacao = DateTime.UtcNow
                });
                await _context.SaveChangesAsync();
                return Ok(new { message = "Adicionado aos favoritos.", isFavorite = true });
            }
        }

        /// <summary>
        /// POST /api/usertopics/activity/{topicId}
        /// Registra uma atividade (acesso) do usuário a um tópico
        /// </summary>
        [HttpPost("activity/{topicId}")]
        public async Task<IActionResult> RecordActivity(int topicId)
        {
            var userId = GetUserId();

            var topic = await _context.Topics.FindAsync(topicId);
            if (topic == null)
                return NotFound(new { message = "Matéria não encontrada." });

            var existing = await _context.UserTopicActivities
                .FirstOrDefaultAsync(x => x.UserId == userId && x.TopicId == topicId);

            if (existing != null)
            {
                // Atualiza acesso existente
                existing.UltimoAcesso = DateTime.UtcNow;
                existing.TotalAcessos++;
            }
            else
            {
                // Cria novo registro
                _context.UserTopicActivities.Add(new UserTopicActivity
                {
                    UserId = userId,
                    TopicId = topicId,
                    UltimoAcesso = DateTime.UtcNow,
                    TotalAcessos = 1
                });
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "Atividade registrada." });
        }

        /// <summary>
        /// GET /api/usertopics/check-favorite/{topicId}
        /// Verifica se uma matéria está nos favoritos
        /// </summary>
        [HttpGet("check-favorite/{topicId}")]
        public async Task<IActionResult> CheckFavorite(int topicId)
        {
            var userId = GetUserId();

            var isFavorite = await _context.UserTopicInterests
                .AnyAsync(x => x.UserId == userId && x.TopicId == topicId);

            return Ok(new { isFavorite });
        }

        /// <summary>
        /// GET /api/usertopics/debug
        /// Endpoint de debug para verificar dados salvos
        /// </summary>
        [HttpGet("debug")]
        public async Task<IActionResult> Debug()
        {
            var userId = GetUserId();

            var favoritesCount = await _context.UserTopicInterests
                .CountAsync(x => x.UserId == userId);
            
            var recentCount = await _context.UserTopicActivities
                .CountAsync(x => x.UserId == userId);

            var favorites = await _context.UserTopicInterests
                .Where(x => x.UserId == userId)
                .Select(x => new { x.TopicId, x.DataMarcacao })
                .ToListAsync();

            var recent = await _context.UserTopicActivities
                .Where(x => x.UserId == userId)
                .Select(x => new { x.TopicId, x.UltimoAcesso, x.TotalAcessos })
                .ToListAsync();

            return Ok(new
            {
                userId,
                favoritesCount,
                recentCount,
                favorites = favorites.Take(5),
                recent = recent.Take(5)
            });
        }
    }
}
