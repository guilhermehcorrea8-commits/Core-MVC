using System.ComponentModel.DataAnnotations;

namespace SistemaAgendamento.Models
{
    public class Agendamento
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A data e hora são obrigatórias.")]
        [Display(Name = "Data e Hora")]
        public DateTime DataHora { get; set; }

        [Required]
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }

        public Cliente? Cliente { get; set; }

        [Required]
        [Display(Name = "Serviço")]
        public int ServicoId { get; set; }

        public Servico? Servico { get; set; }

        [Required]
        [Display(Name = "Profissional")]
        public int ProfissionalId { get; set; }

        public Profissional? Profissional { get; set; }

        [StringLength(500)]
        public string Observacao { get; set; } = string.Empty;
    }
}