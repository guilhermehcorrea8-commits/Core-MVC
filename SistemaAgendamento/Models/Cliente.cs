using System.ComponentModel.DataAnnotations;

namespace SistemaAgendamento.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(14)]
        public string CPF { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        public string Email { get; set; } = string.Empty;

        [StringLength(20)]
        public string Telefone { get; set; } = string.Empty;

        public ICollection<Agendamento> Agendamentos { get; set; }
            = new List<Agendamento>();
    }
}