using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaGestao.Models
{
    public class Movimentacao
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Valor")]
        public decimal Valor { get; set; }

        [Required]
        [Display(Name = "Data")]
        public DateTime Data { get; set; }

        [StringLength(255)]
        [Display(Name = "Descrição")]
        public string? Descricao { get; set; }

        [Required]
        [Display(Name = "Conta")]
        public int ContaId { get; set; }

        [ForeignKey(nameof(ContaId))]
        public Conta? Conta { get; set; }
    }
}