using System.ComponentModel.DataAnnotations;

namespace PortalEstudos.API.Models
{
    /// <summary>
    /// Representa o interesse/favorito de um usuário em um tópico.
    /// Permite marcar matérias como favoritas.
    /// </summary>
    public class UserTopicInterest
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;

        public int TopicId { get; set; }
        public Topic Topic { get; set; } = null!;

        /// <summary>Data em que o usuário marcou como interesse.</summary>
        public DateTime DataMarcacao { get; set; }
    }
}
