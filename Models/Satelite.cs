using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdinApi.Models
{
    [Table("Satelites")]
    public class Satelite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal CombustivelAtual { get; set; }

        [Required]
        [StringLength(20)]
        public string StatusOperacional { get; set; } = "ATIVO"; // ATIVO, INATIVO, MANUTENCAO

        [Required]
        public DateTime DataLancamento { get; set; }

        [Required]
        public int OperadorId { get; set; }

        // Relacionamento N:1 com Operador
        [ForeignKey("OperadorId")]
        public Operador? Operador { get; set; }

        // Relacionamento 1:N com Manobras
        public ICollection<Manobra> Manobras { get; set; } = new List<Manobra>();

        // Relacionamento 1:N com Alertas
        public ICollection<Alerta> Alertas { get; set; } = new List<Alerta>();
    }
}
