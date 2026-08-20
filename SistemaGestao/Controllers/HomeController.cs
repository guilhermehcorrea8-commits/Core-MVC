using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestao.Data;
using SistemaGestao.Models;

namespace SistemaGestao.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var totalReceitas = await _context.Receitas
                .SumAsync(r => (decimal?)r.Valor) ?? 0;

            var totalDespesas = await _context.Despesas
                .SumAsync(d => (decimal?)d.Valor) ?? 0;

            var saldoContas = await _context.Contas
                .Where(c => c.Ativa)
                .SumAsync(c => (decimal?)c.Saldo) ?? 0;

            var totalMetas = await _context.MetasFinanceiras
                .SumAsync(m => (decimal?)m.ValorObjetivo) ?? 0;

            var totalEconomizado = await _context.MetasFinanceiras
                .SumAsync(m => (decimal?)m.ValorAtual) ?? 0;

            var ultimasMovimentacoes = await _context.Movimentacoes
                .Include(m => m.Conta)
                .OrderByDescending(m => m.Data)
                .Take(5)
                .ToListAsync();

            var model = new DashboardViewModel
            {
                TotalReceitas = totalReceitas,
                TotalDespesas = totalDespesas,
                SaldoContas = saldoContas,
                SaldoAtual = totalReceitas - totalDespesas,
                QuantidadeContas = await _context.Contas.CountAsync(),
                QuantidadeMetas = await _context.MetasFinanceiras.CountAsync(),
                TotalMetas = totalMetas,
                TotalEconomizado = totalEconomizado,
                UltimasMovimentacoes = ultimasMovimentacoes
            };

            return View(model);
        }
    }
}