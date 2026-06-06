using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdinApi.Models
{
    [Table("Alertas")]
    public class Alerta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int SateliteId { get; set; }

        [Required]
        public int DebitoId { get; set; }

        [Required]
        [StringLength(20)]
        public string Severidade { get; set; } = "MEDIA"; // BAIXA, MEDIA, ALTA, CRITICA

        [Required]
        [StringLength(500)]
        public string Mensagem { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "ATIVO"; // ATIVO, RESOLVIDO, IGNORADO

        [Required]
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        public DateTime? DataResolucao { get; set; }

        // Relacionamento N:1 com Satelite
        [ForeignKey("SateliteId")]
        public Satelite? Satelite { get; set; }

        // Relacionamento N:1 com Detrito
        [ForeignKey("DebitoId")]
        public Detrito? Detrito { get; set; }
    }
}
