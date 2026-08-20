using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestao.Models
{
    public class MovimentacaoEstoque
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Produto")]
        public int ProdutoId { get; set; }

        public Produto? Produto { get; set; }

        [Required]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantidade { get; set; }

        [Required]
        [Display(Name = "Data")]
        public DateTime Data { get; set; } = DateTime.Now;

        [StringLength(500)]
        public string? Observacao { get; set; }
    }
}