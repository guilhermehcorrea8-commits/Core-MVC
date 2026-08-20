using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SistemaBiblioteca.Models
{
    public class Autor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do autor é obrigatório.")]
        [StringLength(100)]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = string.Empty;

        public ICollection<Livro> Livros { get; set; } = new List<Livro>();
    }
}