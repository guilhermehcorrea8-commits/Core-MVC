using System.ComponentModel.DataAnnotations;

namespace SistemaFinanceiro.Models;

public class Categoria
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;
}