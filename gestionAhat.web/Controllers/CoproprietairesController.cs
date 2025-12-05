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
    public class CoproprietairesController : Controller
    {
        private readonly GestionsyndicContext _context;

        public CoproprietairesController(GestionsyndicContext context)
        {
            _context = context;
        }

        // GET: Coproprietaires
        public async Task<IActionResult> Index()
        {
            var gestionsyndicContext = _context.Coproprietaires.Include(c => c.IdUtilisateurNavigation).Include(c => c.Immeuble);
            return View(await gestionsyndicContext.ToListAsync());
        }

        // GET: Coproprietaires/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coproprietaire = await _context.Coproprietaires
                .Include(c => c.IdUtilisateurNavigation)
                .Include(c => c.Immeuble)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coproprietaire == null)
            {
                return NotFound();
            }

            return View(coproprietaire);
        }

        // GET: Coproprietaires/Create
        public IActionResult Create()
        {
            ViewData["IdUtilisateur"] = new SelectList(_context.Utilisateurs, "Id", "Id");
            ViewData["ImmeubleId"] = new SelectList(_context.Immeubles, "Id", "Id");
            return View();
        }

        // POST: Coproprietaires/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cin,Adresse,ImmeubleId,IdUtilisateur")] Coproprietaire coproprietaire)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coproprietaire);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUtilisateur"] = new SelectList(_context.Utilisateurs, "Id", "Id", coproprietaire.IdUtilisateur);
            ViewData["ImmeubleId"] = new SelectList(_context.Immeubles, "Id", "Id", coproprietaire.ImmeubleId);
            return View(coproprietaire);
        }

        // GET: Coproprietaires/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coproprietaire = await _context.Coproprietaires.FindAsync(id);
            if (coproprietaire == null)
            {
                return NotFound();
            }
            ViewData["IdUtilisateur"] = new SelectList(_context.Utilisateurs, "Id", "Id", coproprietaire.IdUtilisateur);
            ViewData["ImmeubleId"] = new SelectList(_context.Immeubles, "Id", "Id", coproprietaire.ImmeubleId);
            return View(coproprietaire);
        }

        // POST: Coproprietaires/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cin,Adresse,ImmeubleId,IdUtilisateur")] Coproprietaire coproprietaire)
        {
            if (id != coproprietaire.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coproprietaire);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoproprietaireExists(coproprietaire.Id))
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
            ViewData["IdUtilisateur"] = new SelectList(_context.Utilisateurs, "Id", "Id", coproprietaire.IdUtilisateur);
            ViewData["ImmeubleId"] = new SelectList(_context.Immeubles, "Id", "Id", coproprietaire.ImmeubleId);
            return View(coproprietaire);
        }

        // GET: Coproprietaires/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coproprietaire = await _context.Coproprietaires
                .Include(c => c.IdUtilisateurNavigation)
                .Include(c => c.Immeuble)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coproprietaire == null)
            {
                return NotFound();
            }

            return View(coproprietaire);
        }

        // POST: Coproprietaires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coproprietaire = await _context.Coproprietaires.FindAsync(id);
            if (coproprietaire != null)
            {
                _context.Coproprietaires.Remove(coproprietaire);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoproprietaireExists(int id)
        {
            return _context.Coproprietaires.Any(e => e.Id == id);
        }
    }
}
