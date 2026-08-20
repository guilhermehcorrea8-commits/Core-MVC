using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaEstoque.Data;
using SistemaEstoque.Models;

namespace SistemaEstoque.Controllers
{
    public class MovimentacoesEstoqueController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovimentacoesEstoqueController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MovimentacoesEstoque
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MovimentacoesEstoque.Include(m => m.Produto);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MovimentacoesEstoque/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentacaoEstoque = await _context.MovimentacoesEstoque
                .Include(m => m.Produto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movimentacaoEstoque == null)
            {
                return NotFound();
            }

            return View(movimentacaoEstoque);
        }

        // GET: MovimentacoesEstoque/Create
        public IActionResult Create()
        {
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "Codigo");
            return View();
        }

        // POST: MovimentacoesEstoque/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProdutoId,Tipo,Quantidade,Data,Observacao")] MovimentacaoEstoque movimentacaoEstoque)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movimentacaoEstoque);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "Codigo", movimentacaoEstoque.ProdutoId);
            return View(movimentacaoEstoque);
        }

        // GET: MovimentacoesEstoque/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentacaoEstoque = await _context.MovimentacoesEstoque.FindAsync(id);
            if (movimentacaoEstoque == null)
            {
                return NotFound();
            }
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "Codigo", movimentacaoEstoque.ProdutoId);
            return View(movimentacaoEstoque);
        }

        // POST: MovimentacoesEstoque/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProdutoId,Tipo,Quantidade,Data,Observacao")] MovimentacaoEstoque movimentacaoEstoque)
        {
            if (id != movimentacaoEstoque.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movimentacaoEstoque);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovimentacaoEstoqueExists(movimentacaoEstoque.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "Codigo", movimentacaoEstoque.ProdutoId);
            return View(movimentacaoEstoque);
        }

        // GET: MovimentacoesEstoque/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentacaoEstoque = await _context.MovimentacoesEstoque
                .Include(m => m.Produto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movimentacaoEstoque == null)
            {
                return NotFound();
            }

            return View(movimentacaoEstoque);
        }

        // POST: MovimentacoesEstoque/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movimentacaoEstoque = await _context.MovimentacoesEstoque.FindAsync(id);
            if (movimentacaoEstoque != null)
            {
                _context.MovimentacoesEstoque.Remove(movimentacaoEstoque);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovimentacaoEstoqueExists(int id)
        {
            return _context.MovimentacoesEstoque.Any(e => e.Id == id);
        }
    }
}
