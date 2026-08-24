using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaGestao.Data;
using SistemaGestao.Models;

namespace SistemaGestao.Controllers
{
    [Authorize]
    public class MovimentacoesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public MovimentacoesController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Movimentacoes
        public async Task<IActionResult> Index()
        {
            var usuarioId = _userManager.GetUserId(User);

            var movimentacoes = await _context.Movimentacoes
                .Include(m => m.Conta)
                .Where(m => m.Conta != null &&
                            m.Conta.UsuarioId == usuarioId)
                .OrderByDescending(m => m.Data)
                .ToListAsync();

            return View(movimentacoes);
        }

        // GET: Movimentacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var usuarioId = _userManager.GetUserId(User);

            var movimentacao = await _context.Movimentacoes
                .Include(m => m.Conta)
                .FirstOrDefaultAsync(m =>
                    m.Id == id &&
                    m.Conta != null &&
                    m.Conta.UsuarioId == usuarioId);

            if (movimentacao == null)
                return NotFound();

            return View(movimentacao);
        }

        // GET: Movimentacoes/Create
        public async Task<IActionResult> Create()
        {
            var usuarioId = _userManager.GetUserId(User);

            ViewData["ContaId"] = new SelectList(
                await _context.Contas
                    .Where(c => c.UsuarioId == usuarioId && c.Ativa)
                    .ToListAsync(),
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
            var usuarioId = _userManager.GetUserId(User);

            // Validação do tipo
            if (movimentacao.Tipo != "Entrada" &&
                movimentacao.Tipo != "Saída")
            {
                ModelState.AddModelError(
                    "Tipo",
                    "O tipo deve ser Entrada ou Saída."
                );
            }

            // Validação do valor
            if (movimentacao.Valor <= 0)
            {
                ModelState.AddModelError(
                    "Valor",
                    "O valor deve ser maior que zero."
                );
            }

            // Validação da data
            if (movimentacao.Data == default)
            {
                ModelState.AddModelError(
                    "Data",
                    "Informe uma data válida."
                );
            }

            // Busca somente uma conta pertencente ao usuário
            var conta = await _context.Contas
                .FirstOrDefaultAsync(c =>
                    c.Id == movimentacao.ContaId &&
                    c.UsuarioId == usuarioId);

            if (conta == null)
            {
                ModelState.AddModelError(
                    "ContaId",
                    "A conta selecionada não existe ou não pertence ao usuário."
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
                movimentacao.Tipo.Equals(
                    "Saída",
                    StringComparison.OrdinalIgnoreCase) &&
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

            // Recarrega as contas do usuário
            ViewData["ContaId"] = new SelectList(
                await _context.Contas
                    .Where(c => c.UsuarioId == usuarioId && c.Ativa)
                    .ToListAsync(),
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

            var usuarioId = _userManager.GetUserId(User);

            var movimentacao = await _context.Movimentacoes
                .Include(m => m.Conta)
                .FirstOrDefaultAsync(m =>
                    m.Id == id &&
                    m.Conta != null &&
                    m.Conta.UsuarioId == usuarioId);

            if (movimentacao == null)
                return NotFound();

            ViewData["ContaId"] = new SelectList(
                await _context.Contas
                    .Where(c => c.UsuarioId == usuarioId && c.Ativa)
                    .ToListAsync(),
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

            var usuarioId = _userManager.GetUserId(User);

            // Busca a movimentação original pertencente ao usuário
            var movimentacaoOriginal = await _context.Movimentacoes
                .Include(m => m.Conta)
                .FirstOrDefaultAsync(m =>
                    m.Id == id &&
                    m.Conta != null &&
                    m.Conta.UsuarioId == usuarioId);

            if (movimentacaoOriginal == null)
                return NotFound();

            // Validação do tipo
            if (movimentacao.Tipo != "Entrada" &&
                movimentacao.Tipo != "Saída")
            {
                ModelState.AddModelError(
                    "Tipo",
                    "O tipo deve ser Entrada ou Saída."
                );
            }

            // Validação do valor
            if (movimentacao.Valor <= 0)
            {
                ModelState.AddModelError(
                    "Valor",
                    "O valor deve ser maior que zero."
                );
            }

            // Validação da data
            if (movimentacao.Data == default)
            {
                ModelState.AddModelError(
                    "Data",
                    "Informe uma data válida."
                );
            }

            // Conta antiga
            var contaAntiga = await _context.Contas
                .FirstOrDefaultAsync(c =>
                    c.Id == movimentacaoOriginal.ContaId &&
                    c.UsuarioId == usuarioId);

            // Conta nova
            var contaNova = await _context.Contas
                .FirstOrDefaultAsync(c =>
                    c.Id == movimentacao.ContaId &&
                    c.UsuarioId == usuarioId);

            if (contaNova == null)
            {
                ModelState.AddModelError(
                    "ContaId",
                    "A conta selecionada não existe ou não pertence ao usuário."
                );
            }
            else if (!contaNova.Ativa)
            {
                ModelState.AddModelError(
                    "ContaId",
                    "A conta selecionada está inativa."
                );
            }

            // 1. Desfaz temporariamente a movimentação antiga
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

            // 2. Verifica saldo da nova movimentação
            if (contaNova != null &&
                movimentacao.Tipo.Equals(
                    "Saída",
                    StringComparison.OrdinalIgnoreCase))
            {
                if (movimentacao.Valor > contaNova.Saldo)
                {
                    ModelState.AddModelError(
                        "Valor",
                        $"Saldo insuficiente. Saldo disponível: {contaNova.Saldo:C}"
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

                // Atualiza somente os dados permitidos
                movimentacaoOriginal.Tipo = movimentacao.Tipo;
                movimentacaoOriginal.Valor = movimentacao.Valor;
                movimentacaoOriginal.Data = movimentacao.Data;
                movimentacaoOriginal.Descricao = movimentacao.Descricao;
                movimentacaoOriginal.ContaId = movimentacao.ContaId;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // Se houver erro, desfaz a alteração temporária
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

            // Recarrega as contas do usuário
            ViewData["ContaId"] = new SelectList(
                await _context.Contas
                    .Where(c => c.UsuarioId == usuarioId && c.Ativa)
                    .ToListAsync(),
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

            var usuarioId = _userManager.GetUserId(User);

            var movimentacao = await _context.Movimentacoes
                .Include(m => m.Conta)
                .FirstOrDefaultAsync(m =>
                    m.Id == id &&
                    m.Conta != null &&
                    m.Conta.UsuarioId == usuarioId);

            if (movimentacao == null)
                return NotFound();

            return View(movimentacao);
        }

        // POST: Movimentacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuarioId = _userManager.GetUserId(User);

            var movimentacao = await _context.Movimentacoes
                .Include(m => m.Conta)
                .FirstOrDefaultAsync(m =>
                    m.Id == id &&
                    m.Conta != null &&
                    m.Conta.UsuarioId == usuarioId);

            if (movimentacao == null)
                return NotFound();

            var conta = movimentacao.Conta;

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