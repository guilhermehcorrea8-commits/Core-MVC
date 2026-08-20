using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaGestao.Data;
using SistemaGestao.Models;

namespace SistemaGestao.Controllers
{
    public class MovimentacoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovimentacoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Movimentacoes
        public async Task<IActionResult> Index()
        {
            var movimentacoes = _context.Movimentacoes
                .Include(m => m.Conta)
                .OrderByDescending(m => m.Data);

            return View(await movimentacoes.ToListAsync());
        }

        // GET: Movimentacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var movimentacao = await _context.Movimentacoes
                .Include(m => m.Conta)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movimentacao == null)
                return NotFound();

            return View(movimentacao);
        }

        // GET: Movimentacoes/Create
        public IActionResult Create()
        {
            ViewData["ContaId"] = new SelectList(
                _context.Contas.Where(c => c.Ativa),
                "Id",
                "Nome"
            );

            return View();
        }

        // POST: Movimentacoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Tipo,Valor,Data,Descricao,ContaId")]
            Movimentacao movimentacao)
        {
            if (movimentacao.Valor <= 0)
            {
                ModelState.AddModelError(
                    "Valor",
                    "O valor deve ser maior que zero."
                );
            }

            var conta = await _context.Contas
                .FirstOrDefaultAsync(c => c.Id == movimentacao.ContaId);

            if (conta == null)
            {
                ModelState.AddModelError(
                    "ContaId",
                    "A conta selecionada não existe."
                );
            }
            else if (!conta.Ativa)
            {
                ModelState.AddModelError(
                    "ContaId",
                    "A conta selecionada está inativa."
                );
            }

            // Verifica saldo para saída
            if (conta != null &&
                movimentacao.Tipo.Equals("Saída", StringComparison.OrdinalIgnoreCase) &&
                movimentacao.Valor > conta.Saldo)
            {
                ModelState.AddModelError(
                    "Valor",
                    $"Saldo insuficiente. Saldo disponível: {conta.Saldo:C}"
                );
            }

            if (ModelState.IsValid && conta != null)
            {
                // Entrada aumenta o saldo
                if (movimentacao.Tipo.Equals(
                    "Entrada",
                    StringComparison.OrdinalIgnoreCase))
                {
                    conta.Saldo += movimentacao.Valor;
                }

                // Saída diminui o saldo
                else if (movimentacao.Tipo.Equals(
                    "Saída",
                    StringComparison.OrdinalIgnoreCase))
                {
                    conta.Saldo -= movimentacao.Valor;
                }

                _context.Add(movimentacao);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["ContaId"] = new SelectList(
                _context.Contas.Where(c => c.Ativa),
                "Id",
                "Nome",
                movimentacao.ContaId
            );

            return View(movimentacao);
        }

        // GET: Movimentacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var movimentacao = await _context.Movimentacoes
                .FindAsync(id);

            if (movimentacao == null)
                return NotFound();

            ViewData["ContaId"] = new SelectList(
                _context.Contas,
                "Id",
                "Nome",
                movimentacao.ContaId
            );

            return View(movimentacao);
        }

        // POST: Movimentacoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,Tipo,Valor,Data,Descricao,ContaId")]
            Movimentacao movimentacao)
        {
            if (id != movimentacao.Id)
                return NotFound();

            if (movimentacao.Valor <= 0)
            {
                ModelState.AddModelError(
                    "Valor",
                    "O valor deve ser maior que zero."
                );
            }

            var movimentacaoOriginal = await _context.Movimentacoes
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movimentacaoOriginal == null)
                return NotFound();

            var contaAntiga = await _context.Contas
                .FirstOrDefaultAsync(c => c.Id == movimentacaoOriginal.ContaId);

            var contaNova = await _context.Contas
                .FirstOrDefaultAsync(c => c.Id == movimentacao.ContaId);

            if (contaNova == null)
            {
                ModelState.AddModelError(
                    "ContaId",
                    "A conta selecionada não existe."
                );
            }

            // 1. Desfaz a movimentação antiga
            if (contaAntiga != null)
            {
                if (movimentacaoOriginal.Tipo.Equals(
                    "Entrada",
                    StringComparison.OrdinalIgnoreCase))
                {
                    contaAntiga.Saldo -= movimentacaoOriginal.Valor;
                }
                else if (movimentacaoOriginal.Tipo.Equals(
                    "Saída",
                    StringComparison.OrdinalIgnoreCase))
                {
                    contaAntiga.Saldo += movimentacaoOriginal.Valor;
                }
            }

            // 2. Verifica se a nova saída possui saldo suficiente
            decimal saldoDisponivel = 0;

            if (contaNova != null)
            {
                saldoDisponivel = contaNova.Saldo;

                // Se a conta antiga e a nova forem a mesma,
                // o saldo já foi corrigido acima.
                if (contaAntiga != null &&
                    contaAntiga.Id == contaNova.Id)
                {
                    saldoDisponivel = contaNova.Saldo;
                }

                if (movimentacao.Tipo.Equals(
                    "Saída",
                    StringComparison.OrdinalIgnoreCase) &&
                    movimentacao.Valor > saldoDisponivel)
                {
                    ModelState.AddModelError(
                        "Valor",
                        $"Saldo insuficiente. Saldo disponível: {saldoDisponivel:C}"
                    );
                }
            }

            if (ModelState.IsValid && contaNova != null)
            {
                // 3. Aplica a nova movimentação
                if (movimentacao.Tipo.Equals(
                    "Entrada",
                    StringComparison.OrdinalIgnoreCase))
                {
                    contaNova.Saldo += movimentacao.Valor;
                }
                else if (movimentacao.Tipo.Equals(
                    "Saída",
                    StringComparison.OrdinalIgnoreCase))
                {
                    contaNova.Saldo -= movimentacao.Valor;
                }

                _context.Update(movimentacao);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // Se houver erro, precisamos desfazer a alteração temporária
            // feita no saldo da conta antiga.
            if (contaAntiga != null)
            {
                if (movimentacaoOriginal.Tipo.Equals(
                    "Entrada",
                    StringComparison.OrdinalIgnoreCase))
                {
                    contaAntiga.Saldo += movimentacaoOriginal.Valor;
                }
                else if (movimentacaoOriginal.Tipo.Equals(
                    "Saída",
                    StringComparison.OrdinalIgnoreCase))
                {
                    contaAntiga.Saldo -= movimentacaoOriginal.Valor;
                }
            }

            ViewData["ContaId"] = new SelectList(
                _context.Contas,
                "Id",
                "Nome",
                movimentacao.ContaId
            );

            return View(movimentacao);
        }

        // GET: Movimentacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var movimentacao = await _context.Movimentacoes
                .Include(m => m.Conta)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movimentacao == null)
                return NotFound();

            return View(movimentacao);
        }

        // POST: Movimentacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movimentacao = await _context.Movimentacoes
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movimentacao == null)
                return NotFound();

            var conta = await _context.Contas
                .FirstOrDefaultAsync(c => c.Id == movimentacao.ContaId);

            if (conta != null)
            {
                // Desfaz o efeito da movimentação
                if (movimentacao.Tipo.Equals(
                    "Entrada",
                    StringComparison.OrdinalIgnoreCase))
                {
                    conta.Saldo -= movimentacao.Valor;
                }
                else if (movimentacao.Tipo.Equals(
                    "Saída",
                    StringComparison.OrdinalIgnoreCase))
                {
                    conta.Saldo += movimentacao.Valor;
                }
            }

            _context.Movimentacoes.Remove(movimentacao);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}  