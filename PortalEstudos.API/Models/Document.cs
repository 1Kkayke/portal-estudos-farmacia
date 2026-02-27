using System.ComponentModel.DataAnnotations;

namespace PortalEstudos.API.Models;

/// <summary>
/// Material de estudo pré-cadastrado para cada disciplina.
/// Cada tópico possui ~20 documentos cobrindo os principais assuntos da matéria.
/// </summary>
public class Document
{
    public int Id { get; set; }

    [Required, MaxLength(300)]
    public string Titulo { get; set; } = string.Empty;

    /// <summary>Resumo curto exibido no card (2-3 linhas).</summary>
    [MaxLength(500)]
    public string Resumo { get; set; } = string.Empty;

    /// <summary>Conteúdo completo em HTML.</summary>
    [Required]
    public string Conteudo { get; set; } = string.Empty;

    /// <summary>Ordem de exibição dentro do tópico.</summary>
    public int Ordem { get; set; }

    /// <summary>Nível de dificuldade: Básico, Intermediário, Avançado.</summary>
    [MaxLength(50)]
    public string Dificuldade { get; set; } = "Intermediário";

    /// <summary>Tempo estimado de leitura em minutos.</summary>
    public int LeituraMinutos { get; set; } = 8;

    // FK
    public int TopicId { get; set; }
    public Topic Topic { get; set; } = null!;
}
