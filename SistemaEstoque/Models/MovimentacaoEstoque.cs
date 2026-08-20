using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SistemaEstoque.Models
{
    public class MovimentacaoEstoque
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Produto")]
        public int ProdutoId { get; set; }

        public Produto? Produto { get; set; }

        [Required]
        [Display(Name = "Tipo de Movimentação")]
        public string Tipo { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantidade { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [StringLength(500)]
        public string? Observacao { get; set; }
    }
}