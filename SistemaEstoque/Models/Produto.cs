using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaEstoque.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Código")]
        public string Codigo { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(255)]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Preco { get; set; }

        [Required]
        [Display(Name = "Estoque Atual")]
        public int EstoqueAtual { get; set; }

        [Required]
        [Display(Name = "Estoque Mínimo")]
        public int EstoqueMinimo { get; set; }

        [Required]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        public Categoria? Categoria { get; set; }

        public ICollection<MovimentacaoEstoque>? Movimentacoes { get; set; }
    }
}