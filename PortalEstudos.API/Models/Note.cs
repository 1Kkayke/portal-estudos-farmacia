using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalEstudos.API.Models
{
    /// <summary>
    /// Representa uma anotação criada por um estudante dentro de um tópico.
    /// Cada anotação pertence a UM usuário e a UM tópico, garantindo isolamento de dados.
    /// </summary>
    public class Note
    {
        /// <summary>Identificador único da anotação.</summary>
        public int Id { get; set; }

        /// <summary>Título da anotação.</summary>
        [Required]
        [MaxLength(200)]
        public string Titulo { get; set; } = string.Empty;

        /// <summary>Conteúdo da anotação (suporta HTML do editor rich text).</summary>
        [Required]
        public string Conteudo { get; set; } = string.Empty;

        /// <summary>Data de criação da anotação.</summary>
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        /// <summary>Data da última atualização.</summary>
        public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;

        // ---- Chaves Estrangeiras ----

        /// <summary>FK para o tópico ao qual a anotação pertence.</summary>
        [Required]
        public int TopicId { get; set; }

        /// <summary>Navegação para o tópico.</summary>
        [ForeignKey(nameof(TopicId))]
        public Topic Topic { get; set; } = null!;

        /// <summary>FK para o usuário dono da anotação.</summary>
        [Required]
        public string UserId { get; set; } = string.Empty;

        /// <summary>Navegação para o usuário.</summary>
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;
    }
}
