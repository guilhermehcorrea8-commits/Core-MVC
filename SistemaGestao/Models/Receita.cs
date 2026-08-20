using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestao.Models
{
    public class Receita
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe a descrição.")]
        [StringLength(150)]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe o valor.")]
        [Range(0.01, 999999999, ErrorMessage = "Informe um valor válido.")]
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "Informe a data.")]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        [Required]
        public int CategoriaId { get; set; }

        public Categoria? Categoria { get; set; }
    }
}