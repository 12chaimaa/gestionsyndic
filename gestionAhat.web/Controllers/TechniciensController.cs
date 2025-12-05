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
    public class TechniciensController : Controller
    {
        private readonly GestionsyndicContext _context;

        public TechniciensController(GestionsyndicContext context)
        {
            _context = context;
        }

        // GET: Techniciens
        public async Task<IActionResult> Index()
        {
            var gestionsyndicContext = _context.Techniciens.Include(t => t.IdUtilisateurNavigation);
            return View(await gestionsyndicContext.ToListAsync());
        }

        // GET: Techniciens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technicien = await _context.Techniciens
                .Include(t => t.IdUtilisateurNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (technicien == null)
            {
                return NotFound();
            }

            return View(technicien);
        }

        // GET: Techniciens/Create
        public IActionResult Create()
        {
            ViewData["IdUtilisateur"] = new SelectList(_context.Utilisateurs, "Id", "Id");
            return View();
        }

        // POST: Techniciens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Specialite,Entreprise,IdUtilisateur")] Technicien technicien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(technicien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUtilisateur"] = new SelectList(_context.Utilisateurs, "Id", "Id", technicien.IdUtilisateur);
            return View(technicien);
        }

        // GET: Techniciens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technicien = await _context.Techniciens.FindAsync(id);
            if (technicien == null)
            {
                return NotFound();
            }
            ViewData["IdUtilisateur"] = new SelectList(_context.Utilisateurs, "Id", "Id", technicien.IdUtilisateur);
            return View(technicien);
        }

        // POST: Techniciens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Specialite,Entreprise,IdUtilisateur")] Technicien technicien)
        {
            if (id != technicien.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(technicien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TechnicienExists(technicien.Id))
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
            ViewData["IdUtilisateur"] = new SelectList(_context.Utilisateurs, "Id", "Id", technicien.IdUtilisateur);
            return View(technicien);
        }

        // GET: Techniciens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technicien = await _context.Techniciens
                .Include(t => t.IdUtilisateurNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (technicien == null)
            {
                return NotFound();
            }

            return View(technicien);
        }

        // POST: Techniciens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var technicien = await _context.Techniciens.FindAsync(id);
            if (technicien != null)
            {
                _context.Techniciens.Remove(technicien);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TechnicienExists(int id)
        {
            return _context.Techniciens.Any(e => e.Id == id);
        }
    }
}
