using System.ComponentModel.DataAnnotations;

namespace PortalEstudos.API.Models;

/// <summary>
/// Prova gerada a partir de questões de um tópico.
/// O sistema sorteia N questões do banco para montar a prova.
/// </summary>
public class Exam
{
    public int Id { get; set; }

    [Required, MaxLength(300)]
    public string Titulo { get; set; } = string.Empty;

    /// <summary>Número de questões na prova.</summary>
    public int TotalQuestoes { get; set; } = 20;

    /// <summary>Tempo limite em minutos (0 = sem limite).</summary>
    public int TempoMinutos { get; set; } = 60;

    // FK
    public int TopicId { get; set; }
    public Topic Topic { get; set; } = null!;
}

/// <summary>
/// Tentativa de prova feita por um usuário.
/// Armazena o resultado e as respostas individuais.
/// </summary>
public class ExamAttempt
{
    public int Id { get; set; }

    public DateTime DataInicio { get; set; } = DateTime.UtcNow;
    public DateTime? DataFim { get; set; }

    /// <summary>Nota final (0-100).</summary>
    public double Nota { get; set; }

    public int Acertos { get; set; }
    public int Erros { get; set; }
    public int TotalQuestoes { get; set; }

    /// <summary>Se a prova foi finalizada.</summary>
    public bool Finalizada { get; set; }

    // FK
    public int TopicId { get; set; }
    public Topic Topic { get; set; } = null!;

    [Required, MaxLength(450)]
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;

    public ICollection<ExamAnswer> Respostas { get; set; } = new List<ExamAnswer>();
}

/// <summary>Resposta individual de uma tentativa de prova.</summary>
public class ExamAnswer
{
    public int Id { get; set; }

    /// <summary>Índice da alternativa escolhida pelo aluno (-1 = não respondeu).</summary>
    public int RespostaEscolhida { get; set; } = -1;

    public bool Correta { get; set; }

    // FKs
    public int ExamAttemptId { get; set; }
    public ExamAttempt ExamAttempt { get; set; } = null!;

    public int QuestionId { get; set; }
    public Question Question { get; set; } = null!;
}
