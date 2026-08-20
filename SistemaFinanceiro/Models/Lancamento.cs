using System.ComponentModel.DataAnnotations;

namespace SistemaFinanceiro.Models;

public class Lancamento
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Descricao { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Currency)]
    public decimal Valor { get; set; }

    [Required]
    public string Tipo { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Date)]
    public DateTime Data { get; set; }

    public int CategoriaId { get; set; }

    public Categoria? Categoria { get; set; }
}