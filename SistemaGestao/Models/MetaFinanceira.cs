using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestao.Models
{
    public class MetaFinanceira
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome da meta.")]
        [StringLength(150)]
        [Display(Name = "Meta")]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 999999999)]
        [Display(Name = "Valor objetivo")]
        [DataType(DataType.Currency)]
        public decimal ValorObjetivo { get; set; }

        [Range(0, 999999999)]
        [Display(Name = "Valor atual")]
        [DataType(DataType.Currency)]
        public decimal ValorAtual { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Prazo")]
        public DateTime Prazo { get; set; }

        [StringLength(500)]
        public string? Descricao { get; set; }

        [Display(Name = "Concluída")]
        public bool Concluida { get; set; }
    }
}