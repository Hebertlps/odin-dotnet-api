using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdinApi.Models
{
    [Table("Detritos")]
    public class Detrito
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Identificacao { get; set; } = string.Empty;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Latitude { get; set; }

        [Required]
        [Range(-180, 180)]
        public decimal Longitude { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Altitude { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Velocidade { get; set; }

        [Required]
        [Range(0, 100)]
        public int NivelRisco { get; set; } // 0-100

        [Required]
        public int OperadorId { get; set; }

        [Required]
        public DateTime DataDeteccao { get; set; } = DateTime.UtcNow;

        // Relacionamento N:1 com Operador
        [ForeignKey("OperadorId")]
        public Operador? Operador { get; set; }

        // Relacionamento 1:N com Alertas
        public ICollection<Alerta> Alertas { get; set; } = new List<Alerta>();
    }
}
