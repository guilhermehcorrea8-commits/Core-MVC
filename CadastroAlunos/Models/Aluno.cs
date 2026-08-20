using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CadastroAlunos.Models
{
    public class Aluno
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(14, MinimumLength = 11, ErrorMessage = "Informe um CPF válido.")]
        [Display(Name = "CPF")]
        public string CPF { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O curso é obrigatório.")]
        [StringLength(100, ErrorMessage = "O curso deve ter no máximo 100 caracteres.")]
        [Display(Name = "Curso")]
        public string Curso { get; set; } = string.Empty;

        [Required(ErrorMessage = "A nota é obrigatória.")]
        [Range(0, 10, ErrorMessage = "A nota deve estar entre 0 e 10.")]
        [Display(Name = "Nota")]
        public decimal Nota { get; set; }
    }
}