using System.ComponentModel.DataAnnotations;

namespace SistemaPedidos.Models
{
    public class ItemPedido
    {
        public int Id { get; set; }

        public int PedidoId { get; set; }

        public Pedido? Pedido { get; set; }

        public int ProdutoId { get; set; }

        public Produto? Produto { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        [Range(1, 9999, ErrorMessage = "A quantidade deve ser maior que zero.")]
        public int Quantidade { get; set; }

        [Display(Name = "Preço Unitário")]
        public decimal PrecoUnitario { get; set; }

        [Display(Name = "Subtotal")]
        public decimal Subtotal => Quantidade * PrecoUnitario;
    }
}