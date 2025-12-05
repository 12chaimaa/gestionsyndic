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
    public class SyndicsController : Controller
    {
        private readonly GestionsyndicContext _context;

        public SyndicsController(GestionsyndicContext context)
        {
            _context = context;
        }

        // GET: Syndics
        public async Task<IActionResult> Index()
        {
            var gestionsyndicContext = _context.Syndics.Include(s => s.IdUtilisateurNavigation);
            return View(await gestionsyndicContext.ToListAsync());
        }

        // GET: Syndics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var syndic = await _context.Syndics
                .Include(s => s.IdUtilisateurNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (syndic == null)
            {
                return NotFound();
            }

            return View(syndic);
        }

        // GET: Syndics/Create
        public IActionResult Create()
        {
            ViewData["IdUtilisateur"] = new SelectList(_context.Utilisateurs, "Id", "Id");
            return View();
        }

        // POST: Syndics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AdresseBureau,Cin,IdUtilisateur")] Syndic syndic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(syndic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUtilisateur"] = new SelectList(_context.Utilisateurs, "Id", "Id", syndic.IdUtilisateur);
            return View(syndic);
        }

        // GET: Syndics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var syndic = await _context.Syndics.FindAsync(id);
            if (syndic == null)
            {
                return NotFound();
            }
            ViewData["IdUtilisateur"] = new SelectList(_context.Utilisateurs, "Id", "Id", syndic.IdUtilisateur);
            return View(syndic);
        }

        // POST: Syndics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AdresseBureau,Cin,IdUtilisateur")] Syndic syndic)
        {
            if (id != syndic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(syndic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SyndicExists(syndic.Id))
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
            ViewData["IdUtilisateur"] = new SelectList(_context.Utilisateurs, "Id", "Id", syndic.IdUtilisateur);
            return View(syndic);
        }

        // GET: Syndics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var syndic = await _context.Syndics
                .Include(s => s.IdUtilisateurNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (syndic == null)
            {
                return NotFound();
            }

            return View(syndic);
        }

        // POST: Syndics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var syndic = await _context.Syndics.FindAsync(id);
            if (syndic != null)
            {
                _context.Syndics.Remove(syndic);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SyndicExists(int id)
        {
            return _context.Syndics.Any(e => e.Id == id);
        }
    }
}
