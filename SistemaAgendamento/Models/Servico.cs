using System.ComponentModel.DataAnnotations;

namespace SistemaAgendamento.Models
{
    public class Servico
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(255)]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Range(0.01, 999999.99, ErrorMessage = "Informe um preço válido.")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "A duração é obrigatória.")]
        [Range(1, 1440, ErrorMessage = "Informe uma duração válida.")]
        [Display(Name = "Duração (minutos)")]
        public int DuracaoMinutos { get; set; }

        public ICollection<Agendamento> Agendamentos { get; set; }
            = new List<Agendamento>();
    }
}   