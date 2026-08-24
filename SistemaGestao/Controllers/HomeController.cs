using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestao.Data;
using SistemaGestao.Models;

namespace SistemaGestao.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var usuarioId = _userManager.GetUserId(User);

            // ==========================================
            // CONTAS DO USUÁRIO
            // ==========================================

            var contas = await _context.Contas
                .Where(c => c.UsuarioId == usuarioId)
                .ToListAsync();

            var contasAtivas = contas
                .Where(c => c.Ativa)
                .ToList();

            // ==========================================
            // TODAS AS MOVIMENTAÇÕES DO USUÁRIO
            // ==========================================

            var todasMovimentacoes = await _context.Movimentacoes
                .Include(m => m.Conta)
                .Where(m => m.Conta != null &&
                            m.Conta.UsuarioId == usuarioId)
                .ToListAsync();

            // ==========================================
            // ÚLTIMAS 10 MOVIMENTAÇÕES
            // ==========================================

            var movimentacoes = todasMovimentacoes
                .OrderByDescending(m => m.Data)
                .Take(10)
                .ToList();

            // ==========================================
            // RECEITAS
            // ==========================================

            var totalEntradas = todasMovimentacoes
                .Where(m => m.Tipo == "Entrada")
                .Sum(m => m.Valor);

            // ==========================================
            // DESPESAS
            // ==========================================

            var totalSaidas = todasMovimentacoes
                .Where(m => m.Tipo == "Saída")
                .Sum(m => m.Valor);

            // ==========================================
            // SALDO DAS CONTAS
            // ==========================================

            var saldoTotal = contasAtivas
                .Sum(c => c.Saldo);

            // ==========================================
            // METAS FINANCEIRAS
            // ==========================================

            var metas = await _context.MetasFinanceiras
                .ToListAsync();

            // ==========================================
            // DADOS PARA O DASHBOARD
            // ==========================================

            ViewBag.TotalContas = contasAtivas.Count;

            ViewBag.SaldoTotal = saldoTotal;

            ViewBag.TotalEntradas = totalEntradas;

            ViewBag.TotalSaidas = totalSaidas;

            ViewBag.Movimentacoes = movimentacoes;

            ViewBag.Contas = contasAtivas;

            ViewBag.Metas = metas;

            return View();
        }
    }
}