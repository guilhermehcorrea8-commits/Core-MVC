using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaAgendamento.Data;
using SistemaAgendamento.Models;

namespace SistemaAgendamento.Controllers
{
    public class AgendamentosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AgendamentosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Agendamentos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Agendamentos.Include(a => a.Cliente).Include(a => a.Profissional).Include(a => a.Servico);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Agendamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendamento = await _context.Agendamentos
                .Include(a => a.Cliente)
                .Include(a => a.Profissional)
                .Include(a => a.Servico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agendamento == null)
            {
                return NotFound();
            }

            return View(agendamento);
        }

        // GET: Agendamentos/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "CPF");
            ViewData["ProfissionalId"] = new SelectList(_context.Profissionais, "Id", "Especialidade");
            ViewData["ServicoId"] = new SelectList(_context.Servicos, "Id", "Nome");
            return View();
        }

        // POST: Agendamentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataHora,ClienteId,ServicoId,ProfissionalId,Observacao")] Agendamento agendamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agendamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "CPF", agendamento.ClienteId);
            ViewData["ProfissionalId"] = new SelectList(_context.Profissionais, "Id", "Especialidade", agendamento.ProfissionalId);
            ViewData["ServicoId"] = new SelectList(_context.Servicos, "Id", "Nome", agendamento.ServicoId);
            return View(agendamento);
        }

        // GET: Agendamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendamento = await _context.Agendamentos.FindAsync(id);
            if (agendamento == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "CPF", agendamento.ClienteId);
            ViewData["ProfissionalId"] = new SelectList(_context.Profissionais, "Id", "Especialidade", agendamento.ProfissionalId);
            ViewData["ServicoId"] = new SelectList(_context.Servicos, "Id", "Nome", agendamento.ServicoId);
            return View(agendamento);
        }

        // POST: Agendamentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataHora,ClienteId,ServicoId,ProfissionalId,Observacao")] Agendamento agendamento)
        {
            if (id != agendamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agendamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgendamentoExists(agendamento.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "CPF", agendamento.ClienteId);
            ViewData["ProfissionalId"] = new SelectList(_context.Profissionais, "Id", "Especialidade", agendamento.ProfissionalId);
            ViewData["ServicoId"] = new SelectList(_context.Servicos, "Id", "Nome", agendamento.ServicoId);
            return View(agendamento);
        }

        // GET: Agendamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendamento = await _context.Agendamentos
                .Include(a => a.Cliente)
                .Include(a => a.Profissional)
                .Include(a => a.Servico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agendamento == null)
            {
                return NotFound();
            }

            return View(agendamento);
        }

        // POST: Agendamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agendamento = await _context.Agendamentos.FindAsync(id);
            if (agendamento != null)
            {
                _context.Agendamentos.Remove(agendamento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgendamentoExists(int id)
        {
            return _context.Agendamentos.Any(e => e.Id == id);
        }
    }
}
