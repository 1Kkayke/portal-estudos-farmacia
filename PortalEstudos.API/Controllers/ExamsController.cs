using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalEstudos.API.Data;
using PortalEstudos.API.DTOs;
using PortalEstudos.API.Models;

namespace PortalEstudos.API.Controllers;

[ApiController]
[Route("api/exams")]
[Authorize]
public class ExamsController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public ExamsController(ApplicationDbContext db) => _db = db;

    private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    /// <summary>Inicia uma nova prova para o tópico, sorteando questões.</summary>
    [HttpPost("start")]
    public async Task<ActionResult<ExamQuestionsDto>> StartExam([FromBody] StartExamDto dto)
    {
        var topic = await _db.Topics.FindAsync(dto.TopicId);
        if (topic is null) return NotFound("Tópico não encontrado.");

        // Sorteia questões aleatoriamente
        var allQuestions = await _db.Questions
            .Include(q => q.Opcoes)
            .Where(q => q.TopicId == dto.TopicId)
            .ToListAsync();

        var take = Math.Min(dto.TotalQuestoes, allQuestions.Count);

        // Shuffle em memória (client-side)
        var questions = allQuestions
            .OrderBy(q => Guid.NewGuid())
            .Take(take)
            .ToList();

        // Cria a tentativa
        var attempt = new ExamAttempt
        {
            TopicId = dto.TopicId,
            UserId = UserId,
            TotalQuestoes = take,
            DataInicio = DateTime.UtcNow,
        };

        // Cria respostas em branco
        foreach (var q in questions)
        {
            attempt.Respostas.Add(new ExamAnswer
            {
                QuestionId = q.Id,
                RespostaEscolhida = -1,
                Correta = false,
            });
        }

        _db.ExamAttempts.Add(attempt);
        await _db.SaveChangesAsync();

        return Ok(new ExamQuestionsDto
        {
            AttemptId = attempt.Id,
            TopicId = dto.TopicId,
            TopicNome = topic.Nome,
            TotalQuestoes = take,
            TempoMinutos = 60,
            Questoes = questions.Select(q => new QuestionDto
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
            }).ToList(),
        });
    }

    /// <summary>Submete as respostas e finaliza a prova.</summary>
    [HttpPost("submit")]
    public async Task<ActionResult<ExamAttemptResultDto>> SubmitExam([FromBody] SubmitExamDto dto)
    {
        var attempt = await _db.ExamAttempts
            .Include(a => a.Respostas)
            .Include(a => a.Topic)
            .FirstOrDefaultAsync(a => a.Id == dto.AttemptId && a.UserId == UserId);

        if (attempt is null) return NotFound("Tentativa não encontrada.");
        if (attempt.Finalizada) return BadRequest("Prova já finalizada.");

        // Busca as questões da prova
        var questionIds = attempt.Respostas.Select(r => r.QuestionId).ToList();
        var questions = await _db.Questions
            .Include(q => q.Opcoes)
            .Where(q => questionIds.Contains(q.Id))
            .ToDictionaryAsync(q => q.Id);

        int acertos = 0;
        foreach (var answer in attempt.Respostas)
        {
            var submitted = dto.Respostas.FirstOrDefault(r => r.QuestionId == answer.QuestionId);
            if (submitted != null)
            {
                answer.RespostaEscolhida = submitted.RespostaEscolhida;
                if (questions.TryGetValue(answer.QuestionId, out var q))
                {
                    answer.Correta = submitted.RespostaEscolhida == q.RespostaCorreta;
                    if (answer.Correta) acertos++;
                }
            }
        }

        attempt.Acertos = acertos;
        attempt.Erros = attempt.TotalQuestoes - acertos;
        attempt.Nota = attempt.TotalQuestoes > 0 ? Math.Round((double)acertos / attempt.TotalQuestoes * 100, 1) : 0;
        attempt.DataFim = DateTime.UtcNow;
        attempt.Finalizada = true;

        await _db.SaveChangesAsync();

        return Ok(MapAttemptResult(attempt, questions));
    }

    /// <summary>Retorna o resultado de uma tentativa específica.</summary>
    [HttpGet("attempts/{attemptId}")]
    public async Task<ActionResult<ExamAttemptResultDto>> GetAttemptResult(int attemptId)
    {
        var attempt = await _db.ExamAttempts
            .Include(a => a.Respostas)
            .Include(a => a.Topic)
            .FirstOrDefaultAsync(a => a.Id == attemptId && a.UserId == UserId);

        if (attempt is null) return NotFound();

        var questionIds = attempt.Respostas.Select(r => r.QuestionId).ToList();
        var questions = await _db.Questions
            .Include(q => q.Opcoes)
            .Where(q => questionIds.Contains(q.Id))
            .ToDictionaryAsync(q => q.Id);

        return Ok(MapAttemptResult(attempt, questions));
    }

    /// <summary>Histórico de provas do usuário, opcionalmente filtrado por tópico.</summary>
    [HttpGet("history")]
    public async Task<ActionResult<IEnumerable<ExamHistoryDto>>> GetHistory([FromQuery] int? topicId)
    {
        var query = _db.ExamAttempts
            .Include(a => a.Topic)
            .Where(a => a.UserId == UserId && a.Finalizada);

        if (topicId.HasValue)
            query = query.Where(a => a.TopicId == topicId.Value);

        var history = await query
            .OrderByDescending(a => a.DataFim)
            .Take(50)
            .Select(a => new ExamHistoryDto
            {
                Id = a.Id,
                DataInicio = a.DataInicio,
                DataFim = a.DataFim,
                Nota = a.Nota,
                Acertos = a.Acertos,
                TotalQuestoes = a.TotalQuestoes,
                Finalizada = a.Finalizada,
                TopicNome = a.Topic.Nome,
            })
            .ToListAsync();

        return Ok(history);
    }

    private static ExamAttemptResultDto MapAttemptResult(ExamAttempt attempt, Dictionary<int, Question> questions)
    {
        return new ExamAttemptResultDto
        {
            Id = attempt.Id,
            DataInicio = attempt.DataInicio,
            DataFim = attempt.DataFim,
            Nota = attempt.Nota,
            Acertos = attempt.Acertos,
            Erros = attempt.Erros,
            TotalQuestoes = attempt.TotalQuestoes,
            Finalizada = attempt.Finalizada,
            TopicId = attempt.TopicId,
            TopicNome = attempt.Topic.Nome,
            Respostas = attempt.Respostas.Select(r =>
            {
                questions.TryGetValue(r.QuestionId, out var q);
                return new ExamAnswerResultDto
                {
                    QuestionId = r.QuestionId,
                    Enunciado = q?.Enunciado ?? "",
                    RespostaEscolhida = r.RespostaEscolhida,
                    RespostaCorreta = q?.RespostaCorreta ?? 0,
                    Correta = r.Correta,
                    Explicacao = q?.Explicacao ?? "",
                    Opcoes = q?.Opcoes.OrderBy(o => o.Indice).Select(o => new QuestionOptionDto
                    {
                        Id = o.Id,
                        Texto = o.Texto,
                        Indice = o.Indice,
                    }).ToList() ?? new(),
                };
            }).ToList(),
        };
    }
}
