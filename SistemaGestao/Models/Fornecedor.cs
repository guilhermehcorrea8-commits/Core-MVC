using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestao.Models
{
    public class Fornecedor
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(18)]
        public string? CNPJ { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [StringLength(20)]
        public string? Telefone { get; set; }
    }
}