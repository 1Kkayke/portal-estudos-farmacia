using Microsoft.AspNetCore.Mvc;
using PortalEstudos.API.Services;

namespace PortalEstudos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogController : ControllerBase
{
    private readonly NewsFeedService _feedService;

    public BlogController(NewsFeedService feedService) => _feedService = feedService;

    /// <summary>GET /api/blog — Todos os artigos (curados + externos)</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? categoria = null, [FromQuery] string? search = null)
    {
        var articles = await _feedService.GetAllArticlesAsync();

        if (!string.IsNullOrWhiteSpace(categoria))
            articles = articles.Where(a => a.Categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase)).ToList();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var s = search.ToLower();
            articles = articles.Where(a =>
                a.Titulo.Contains(s, StringComparison.OrdinalIgnoreCase) ||
                a.Resumo.Contains(s, StringComparison.OrdinalIgnoreCase) ||
                a.Tags.Any(t => t.Contains(s, StringComparison.OrdinalIgnoreCase))
            ).ToList();
        }

        return Ok(articles);
    }

    /// <summary>GET /api/blog/paginated — Artigos com paginação (melhor para frontend)</summary>
    [HttpGet("paginated")]
    public async Task<IActionResult> GetPaginated(
        [FromQuery] int page = 1, 
        [FromQuery] int pageSize = 12,
        [FromQuery] string? categoria = null, 
        [FromQuery] string? search = null)
    {
        if (page < 1) page = 1;
        if (pageSize < 1 || pageSize > 50) pageSize = 12;

        var articles = await _feedService.GetAllArticlesAsync();

        if (!string.IsNullOrWhiteSpace(categoria))
            articles = articles.Where(a => a.Categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase)).ToList();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var s = search.ToLower();
            articles = articles.Where(a =>
                a.Titulo.Contains(s, StringComparison.OrdinalIgnoreCase) ||
                a.Resumo.Contains(s, StringComparison.OrdinalIgnoreCase) ||
                a.Tags.Any(t => t.Contains(s, StringComparison.OrdinalIgnoreCase))
            ).ToList();
        }

        var total = articles.Count;
        var totalPages = (int)Math.Ceiling((double)total / pageSize);
        var items = articles.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return Ok(new
        {
            items,
            page,
            pageSize,
            total,
            totalPages,
            hasMore = page < totalPages
        });
    }

    /// <summary>GET /api/blog/categories — Lista de categorias</summary>
    [HttpGet("categories")]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _feedService.GetCategoriesAsync();
        return Ok(categories);
    }

    /// <summary>GET /api/blog/{id} — Artigo individual</summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var article = await _feedService.GetArticleByIdAsync(id);
        if (article is null) return NotFound();
        return Ok(article);
    }
}
