using System.ComponentModel.DataAnnotations;

namespace PortalEstudos.API.Models;

/// <summary>
/// Questão de múltipla-escolha vinculada a um tópico.
/// Cada tópico possui ~40 questões para estudo e avaliação.
/// </summary>
public class Question
{
    public int Id { get; set; }

    [Required, MaxLength(1000)]
    public string Enunciado { get; set; } = string.Empty;

    /// <summary>Explicação detalhada da resposta correta.</summary>
    [Required]
    public string Explicacao { get; set; } = string.Empty;

    /// <summary>Índice da alternativa correta (0-3).</summary>
    public int RespostaCorreta { get; set; }

    /// <summary>Nível: Fácil, Médio, Difícil.</summary>
    [MaxLength(30)]
    public string Dificuldade { get; set; } = "Médio";

    /// <summary>Ordem de exibição.</summary>
    public int Ordem { get; set; }

    // FK
    public int TopicId { get; set; }
    public Topic Topic { get; set; } = null!;

    public ICollection<QuestionOption> Opcoes { get; set; } = new List<QuestionOption>();
}

/// <summary>Alternativa de uma questão (A-D).</summary>
public class QuestionOption
{
    public int Id { get; set; }

    [Required, MaxLength(500)]
    public string Texto { get; set; } = string.Empty;

    /// <summary>Índice da alternativa (0=A, 1=B, 2=C, 3=D).</summary>
    public int Indice { get; set; }

    // FK
    public int QuestionId { get; set; }
    public Question Question { get; set; } = null!;
}
