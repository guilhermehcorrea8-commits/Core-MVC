using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaGestao.Models
{
    public class LancamentoFinanceiro
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe a descrição.")]
        [StringLength(150)]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 999999999)]
        public decimal Valor { get; set; }

        [Required]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Data")]
        public DateTime Data { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Categoria")]
        public int CategoriaFinanceiraId { get; set; }

        public CategoriaFinanceira? CategoriaFinanceira { get; set; }

        [StringLength(500)]
        public string? Observacao { get; set; }
    }
}