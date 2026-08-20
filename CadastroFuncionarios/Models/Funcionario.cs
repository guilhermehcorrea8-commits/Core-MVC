using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CadastroFuncionarios.Models
{
    public class Funcionario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100)]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O cargo é obrigatório.")]
        [StringLength(100)]
        [Display(Name = "Cargo")]
        public string Cargo { get; set; } = string.Empty;

        [Required(ErrorMessage = "O salário é obrigatório.")]
        [Range(0, 1000000, ErrorMessage = "O salário deve ser maior ou igual a zero.")]
        [Display(Name = "Salário")]
        [DataType(DataType.Currency)]
        public decimal Salario { get; set; }

        [Required(ErrorMessage = "A data de admissão é obrigatória.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Admissão")]
        public DateTime DataAdmissao { get; set; }
    }
}