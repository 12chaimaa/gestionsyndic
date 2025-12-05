using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gestionsyndic.web.Models;

namespace gestionsyndic.web.Controllers
{
    public class TravauxesController : Controller
    {
        private readonly GestionsyndicContext _context;

        public TravauxesController(GestionsyndicContext context)
        {
            _context = context;
        }

        // GET: Travauxes
        public async Task<IActionResult> Index()
        {
            var gestionsyndicContext = _context.Travauxes.Include(t => t.IdImmeubleNavigation).Include(t => t.IdPaiementNavigation);
            return View(await gestionsyndicContext.ToListAsync());
        }

        // GET: Travauxes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travaux = await _context.Travauxes
                .Include(t => t.IdImmeubleNavigation)
                .Include(t => t.IdPaiementNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (travaux == null)
            {
                return NotFound();
            }

            return View(travaux);
        }

        // GET: Travauxes/Create
        public IActionResult Create()
        {
            ViewData["IdImmeuble"] = new SelectList(_context.Immeubles, "Id", "Id");
            ViewData["IdPaiement"] = new SelectList(_context.Paiements, "Id", "Id");
            return View();
        }

        // POST: Travauxes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,DateDebut,DateFin,Statut,Cout,IdImmeuble,IdPaiement")] Travaux travaux)
        {
            if (ModelState.IsValid)
            {
                _context.Add(travaux);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdImmeuble"] = new SelectList(_context.Immeubles, "Id", "Id", travaux.IdImmeuble);
            ViewData["IdPaiement"] = new SelectList(_context.Paiements, "Id", "Id", travaux.IdPaiement);
            return View(travaux);
        }

        // GET: Travauxes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travaux = await _context.Travauxes.FindAsync(id);
            if (travaux == null)
            {
                return NotFound();
            }
            ViewData["IdImmeuble"] = new SelectList(_context.Immeubles, "Id", "Id", travaux.IdImmeuble);
            ViewData["IdPaiement"] = new SelectList(_context.Paiements, "Id", "Id", travaux.IdPaiement);
            return View(travaux);
        }

        // POST: Travauxes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,DateDebut,DateFin,Statut,Cout,IdImmeuble,IdPaiement")] Travaux travaux)
        {
            if (id != travaux.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(travaux);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TravauxExists(travaux.Id))
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
            ViewData["IdImmeuble"] = new SelectList(_context.Immeubles, "Id", "Id", travaux.IdImmeuble);
            ViewData["IdPaiement"] = new SelectList(_context.Paiements, "Id", "Id", travaux.IdPaiement);
            return View(travaux);
        }

        // GET: Travauxes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travaux = await _context.Travauxes
                .Include(t => t.IdImmeubleNavigation)
                .Include(t => t.IdPaiementNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (travaux == null)
            {
                return NotFound();
            }

            return View(travaux);
        }

        // POST: Travauxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var travaux = await _context.Travauxes.FindAsync(id);
            if (travaux != null)
            {
                _context.Travauxes.Remove(travaux);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TravauxExists(int id)
        {
            return _context.Travauxes.Any(e => e.Id == id);
        }
    }
}
