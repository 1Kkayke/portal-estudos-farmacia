using Microsoft.AspNetCore.Identity;

namespace PortalEstudos.API.Models
{
    /// <summary>
    /// Representa o usuário do sistema, estendendo o IdentityUser padrão.
    /// Permite adicionar campos personalizados no futuro (ex: curso, semestre).
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>Nome completo do estudante.</summary>
        public string NomeCompleto { get; set; } = string.Empty;

        /// <summary>Data de cadastro no sistema.</summary>
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

        // Navegação: um usuário pode ter várias anotações
        public ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}
