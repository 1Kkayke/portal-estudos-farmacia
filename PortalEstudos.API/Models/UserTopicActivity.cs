using System.ComponentModel.DataAnnotations;

namespace PortalEstudos.API.Models
{
    /// <summary>
    /// Registra atividades do usuário em um tópico.
    /// Usado para rastrear "últimas matérias estudadas".
    /// </summary>
    public class UserTopicActivity
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;

        public int TopicId { get; set; }
        public Topic Topic { get; set; } = null!;

        /// <summary>Última vez que o usuário acessou este tópico.</summary>
        public DateTime UltimoAcesso { get; set; }

        /// <summary>Número total de acessos.</summary>
        public int TotalAcessos { get; set; }
    }
}
