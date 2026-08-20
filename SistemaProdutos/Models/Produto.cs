using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SistemaProdutos.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(150)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Range(0.01, 999999.99, ErrorMessage = "Informe um preço válido.")]
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "A categoria é obrigatória.")]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        public Categoria? Categoria { get; set; }
    }
}