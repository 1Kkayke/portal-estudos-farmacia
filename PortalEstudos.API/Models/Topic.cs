using System.ComponentModel.DataAnnotations;

namespace PortalEstudos.API.Models
{
    /// <summary>
    /// Representa um tópico/disciplina de Farmácia.
    /// Os tópicos são pré-carregados via seed no banco de dados.
    /// </summary>
    public class Topic
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Nome { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Descricao { get; set; } = string.Empty;

        /// <summary>Categoria para agrupamento no frontend (ex: "Ciências Básicas").</summary>
        [MaxLength(100)]
        public string Categoria { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Icone { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Cor { get; set; } = string.Empty;

        public ICollection<Note> Notes { get; set; } = new List<Note>();
        public ICollection<Document> Documents { get; set; } = new List<Document>();
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
