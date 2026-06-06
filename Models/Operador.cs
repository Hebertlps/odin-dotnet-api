using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdinApi.Models
{
    [Table("Operadores")]
    public class Operador
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string NivelAcesso { get; set; } = "USUARIO"; // ADMIN, OPERADOR, USUARIO

        [Required]
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        // Relacionamento 1:N com Satelites
        public ICollection<Satelite> Satelites { get; set; } = new List<Satelite>();

        // Relacionamento 1:N com Detritos
        public ICollection<Detrito> Detritos { get; set; } = new List<Detrito>();

        // Relacionamento 1:N com Manobras
        public ICollection<Manobra> Manobras { get; set; } = new List<Manobra>();
    }
}
