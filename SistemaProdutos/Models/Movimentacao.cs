using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SistemaEstoque.Models
{
    public class Movimentacao
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
        public int Quantidade { get; set; }

        [Required]
        [Display(Name = "Data da Movimentação")]
        public DateTime Data { get; set; }

        [StringLength(255)]
        public string Observacao { get; set; } = string.Empty;
    }
}