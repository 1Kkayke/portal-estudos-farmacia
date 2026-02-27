using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalEstudos.API.Data;
using PortalEstudos.API.DTOs;

namespace PortalEstudos.API.Controllers;

[ApiController]
[Route("api/topics/{topicId}/questions")]
[Authorize]
public class QuestionsController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public QuestionsController(ApplicationDbContext db) => _db = db;

    /// <summary>Lista questões para prática (sem resposta correta).</summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<QuestionDto>>> GetPractice(int topicId)
    {
        var qs = await _db.Questions
            .Include(q => q.Opcoes)
            .Where(q => q.TopicId == topicId)
            .OrderBy(q => q.Ordem)
            .Select(q => new QuestionDto
            {
                Id = q.Id,
                Enunciado = q.Enunciado,
                Dificuldade = q.Dificuldade,
                Ordem = q.Ordem,
                TopicId = q.TopicId,
                Opcoes = q.Opcoes.OrderBy(o => o.Indice).Select(o => new QuestionOptionDto
                {
                    Id = o.Id,
                    Texto = o.Texto,
                    Indice = o.Indice,
                }).ToList(),
            })
            .ToListAsync();
        return Ok(qs);
    }

    /// <summary>Retorna questões com respostas (modo revisão).</summary>
    [HttpGet("review")]
    public async Task<ActionResult<IEnumerable<QuestionWithAnswerDto>>> GetReview(int topicId)
    {
        var qs = await _db.Questions
            .Include(q => q.Opcoes)
            .Where(q => q.TopicId == topicId)
            .OrderBy(q => q.Ordem)
            .Select(q => new QuestionWithAnswerDto
            {
                Id = q.Id,
                Enunciado = q.Enunciado,
                Dificuldade = q.Dificuldade,
                Ordem = q.Ordem,
                TopicId = q.TopicId,
                RespostaCorreta = q.RespostaCorreta,
                Explicacao = q.Explicacao,
                Opcoes = q.Opcoes.OrderBy(o => o.Indice).Select(o => new QuestionOptionDto
                {
                    Id = o.Id,
                    Texto = o.Texto,
                    Indice = o.Indice,
                }).ToList(),
            })
            .ToListAsync();
        return Ok(qs);
    }

    /// <summary>Verifica resposta de uma questão individual.</summary>
    [HttpPost("{questionId}/check")]
    public async Task<ActionResult> CheckAnswer(int topicId, int questionId, [FromBody] ExamAnswerDto answer)
    {
        var q = await _db.Questions.FirstOrDefaultAsync(q => q.Id == questionId && q.TopicId == topicId);
        if (q is null) return NotFound();

        return Ok(new
        {
            Correta = answer.RespostaEscolhida == q.RespostaCorreta,
            RespostaCorreta = q.RespostaCorreta,
            Explicacao = q.Explicacao,
        });
    }
}
