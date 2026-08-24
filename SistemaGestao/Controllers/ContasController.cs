using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestao.Data;
using SistemaGestao.Models;

namespace SistemaGestao.Controllers
{
    [Authorize]
    public class ContasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ContasController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Contas
        public async Task<IActionResult> Index()
        {
            var usuarioId = _userManager.GetUserId(User);

            var contas = await _context.Contas
                .Where(c => c.UsuarioId == usuarioId)
                .ToListAsync();

            return View(contas);
        }

        // GET: Contas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var usuarioId = _userManager.GetUserId(User);

            var conta = await _context.Contas
                .FirstOrDefaultAsync(c =>
                    c.Id == id &&
                    c.UsuarioId == usuarioId);

            if (conta == null)
                return NotFound();

            return View(conta);
        }

        // GET: Contas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Nome,Saldo,Instituicao,Ativa")] Conta conta)
        {
            if (ModelState.IsValid)
            {
                conta.UsuarioId = _userManager.GetUserId(User) ?? string.Empty;

                _context.Add(conta);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(conta);
        }

        // GET: Contas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var usuarioId = _userManager.GetUserId(User);

            var conta = await _context.Contas
                .FirstOrDefaultAsync(c =>
                    c.Id == id &&
                    c.UsuarioId == usuarioId);

            if (conta == null)
                return NotFound();

            return View(conta);
        }

        // POST: Contas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,Nome,Saldo,Instituicao,Ativa")] Conta conta)
        {
            if (id != conta.Id)
                return NotFound();

            var usuarioId = _userManager.GetUserId(User);

            var contaExistente = await _context.Contas
                .FirstOrDefaultAsync(c =>
                    c.Id == id &&
                    c.UsuarioId == usuarioId);

            if (contaExistente == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                contaExistente.Nome = conta.Nome;
                contaExistente.Saldo = conta.Saldo;
                contaExistente.Instituicao = conta.Instituicao;
                contaExistente.Ativa = conta.Ativa;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(conta);
        }

        // GET: Contas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var usuarioId = _userManager.GetUserId(User);

            var conta = await _context.Contas
                .FirstOrDefaultAsync(c =>
                    c.Id == id &&
                    c.UsuarioId == usuarioId);

            if (conta == null)
                return NotFound();

            return View(conta);
        }

        // POST: Contas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuarioId = _userManager.GetUserId(User);

            var conta = await _context.Contas
                .FirstOrDefaultAsync(c =>
                    c.Id == id &&
                    c.UsuarioId == usuarioId);

            if (conta != null)
            {
                _context.Contas.Remove(conta);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
