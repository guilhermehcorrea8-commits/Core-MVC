using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CadastroProdutos.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(500)]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 999999999)]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "O estoque é obrigatório.")]
        [Range(0, int.MaxValue)]
        public int Estoque { get; set; }
    }
}