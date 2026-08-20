using System.ComponentModel.DataAnnotations;

namespace SistemaAgendamento.Models
{
    public class Profissional
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A especialidade é obrigatória.")]
        [StringLength(100)]
        public string Especialidade { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        public string Email { get; set; } = string.Empty;

        [StringLength(20)]
        public string Telefone { get; set; } = string.Empty;

        public ICollection<Agendamento> Agendamentos { get; set; }
            = new List<Agendamento>();
    }
}