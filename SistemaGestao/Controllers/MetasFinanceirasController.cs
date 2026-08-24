using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using SistemaGestao.Data;
using SistemaGestao.Models;

namespace SistemaGestao.Controllers
{
    [Authorize]
    public class MetasFinanceirasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MetasFinanceirasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MetasFinanceiras
        public async Task<IActionResult> Index()
        {
            return View(await _context.MetasFinanceiras.ToListAsync());
        }

        // GET: MetasFinanceiras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metaFinanceira = await _context.MetasFinanceiras
                .FirstOrDefaultAsync(m => m.Id == id);
            if (metaFinanceira == null)
            {
                return NotFound();
            }

            return View(metaFinanceira);
        }

        // GET: MetasFinanceiras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MetasFinanceiras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,ValorObjetivo,ValorAtual,Prazo,Descricao,Concluida")] MetaFinanceira metaFinanceira)
        {
            if (ModelState.IsValid)
            {
                _context.Add(metaFinanceira);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(metaFinanceira);
        }

        // GET: MetasFinanceiras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metaFinanceira = await _context.MetasFinanceiras.FindAsync(id);
            if (metaFinanceira == null)
            {
                return NotFound();
            }
            return View(metaFinanceira);
        }

        // POST: MetasFinanceiras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,ValorObjetivo,ValorAtual,Prazo,Descricao,Concluida")] MetaFinanceira metaFinanceira)
        {
            if (id != metaFinanceira.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(metaFinanceira);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MetaFinanceiraExists(metaFinanceira.Id))
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
            return View(metaFinanceira);
        }

        // GET: MetasFinanceiras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metaFinanceira = await _context.MetasFinanceiras
                .FirstOrDefaultAsync(m => m.Id == id);
            if (metaFinanceira == null)
            {
                return NotFound();
            }

            return View(metaFinanceira);
        }

        // POST: MetasFinanceiras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var metaFinanceira = await _context.MetasFinanceiras.FindAsync(id);
            if (metaFinanceira != null)
            {
                _context.MetasFinanceiras.Remove(metaFinanceira);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MetaFinanceiraExists(int id)
        {
            return _context.MetasFinanceiras.Any(e => e.Id == id);
        }
    }
}
