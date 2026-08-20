using System.ComponentModel.DataAnnotations;

namespace SistemaPedidos.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(150)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Range(0.01, 999999.99, ErrorMessage = "Informe um preço válido.")]
        public decimal Preco { get; set; }

        public ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();
    }
}