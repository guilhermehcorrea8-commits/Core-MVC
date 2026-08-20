using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SistemaGestao.Models
{
    public class DashboardViewModel
    {
        public decimal TotalReceitas { get; set; }

        public decimal TotalDespesas { get; set; }

        public decimal SaldoAtual { get; set; }

        public decimal SaldoContas { get; set; }

        public int QuantidadeContas { get; set; }

        public int QuantidadeMetas { get; set; }

        public decimal TotalMetas { get; set; }

        public decimal TotalEconomizado { get; set; }

        public List<Movimentacao> UltimasMovimentacoes { get; set; }
            = new List<Movimentacao>();
    }
}