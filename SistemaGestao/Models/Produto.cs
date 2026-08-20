using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaGestao.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome do produto.")]
        [StringLength(150)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(50)]
        public string? Codigo { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 999999999)]
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