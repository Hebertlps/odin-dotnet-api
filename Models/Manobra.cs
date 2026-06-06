using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdinApi.Models
{
    [Table("Manobras")]
    public class Manobra
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int SateliteId { get; set; }

        [Required]
        public int OperadorId { get; set; }

        [Required]
        [StringLength(50)]
        public string Tipo { get; set; } = string.Empty; // DESVIO, ACELERACAO, DESACELERACAO

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "PENDENTE"; // PENDENTE, EXECUTADA, CANCELADA

        [Required]
        public DateTime DataSolicitacao { get; set; } = DateTime.UtcNow;

        public DateTime? DataExecucao { get; set; }

        [Range(0, 100)]
        public decimal CombustivelConsumido { get; set; }

        // Relacionamento N:1 com Satelite
        [ForeignKey("SateliteId")]
        public Satelite? Satelite { get; set; }

        // Relacionamento N:1 com Operador
        [ForeignKey("OperadorId")]
        public Operador? Operador { get; set; }
    }
}
