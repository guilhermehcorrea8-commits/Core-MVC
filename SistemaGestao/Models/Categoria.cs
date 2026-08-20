using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestao.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome da categoria.")]
        [StringLength(100)]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = string.Empty;

        [StringLength(255)]
        [Display(Name = "Descrição")]
        public string? Descricao { get; set; }

        public ICollection<Receita> Receitas { get; set; } = new List<Receita>();

        public ICollection<Despesa> Despesas { get; set; } = new List<Despesa>();
    }
}