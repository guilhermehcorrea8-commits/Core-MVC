using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SistemaBiblioteca.Models
{
    public class Livro
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(150)]
        [Display(Name = "Título")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "O ISBN é obrigatório.")]
        [StringLength(20)]
        public string ISBN { get; set; } = string.Empty;

        [Required(ErrorMessage = "O ano de publicação é obrigatório.")]
        [Range(1000, 2100, ErrorMessage = "Informe um ano válido.")]
        [Display(Name = "Ano de Publicação")]
        public int AnoPublicacao { get; set; }

        // Chave estrangeira do Autor
        [Display(Name = "Autor")]
        public int AutorId { get; set; }

        // Relacionamento com Autor
        public Autor Autor { get; set; } = null!;

        // Chave estrangeira da Categoria
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        // Relacionamento com Categoria
        public Categoria Categoria { get; set; } = null!;
    }
}