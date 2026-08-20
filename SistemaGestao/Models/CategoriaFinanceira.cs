using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestao.Models
{
    public class CategoriaFinanceira
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome da categoria.")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; } = string.Empty;

        public ICollection<LancamentoFinanceiro>? Lancamentos { get; set; }
    }
}