using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SistemaGestao.Models
{
    public class Conta
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome da conta.")]
        [StringLength(100)]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Saldo")]
        public decimal Saldo { get; set; }

        [StringLength(100)]
        [Display(Name = "Instituição")]
        public string? Instituicao { get; set; }

        [Display(Name = "Ativa")]
        public bool Ativa { get; set; } = true;

        [Required]
        public string UsuarioId { get; set; } = string.Empty;


        public IdentityUser? Usuario { get; set; }

        public ICollection<Movimentacao> Movimentacoes { get; set; }
            = new List<Movimentacao>();
    }
}