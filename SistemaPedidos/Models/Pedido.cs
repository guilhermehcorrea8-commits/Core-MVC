using System.ComponentModel.DataAnnotations;

namespace SistemaPedidos.Models
{
    public class Pedido
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Data do Pedido")]
        public DateTime DataPedido { get; set; } = DateTime.Now;

        public int ClienteId { get; set; }

        [Display(Name = "Cliente")]
        public Cliente? Cliente { get; set; }

        public ICollection<ItemPedido> Itens { get; set; } = new List<ItemPedido>();
    }
}