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
    public class ImmeublesController : Controller
    {
        private readonly GestionsyndicContext _context;

        public ImmeublesController(GestionsyndicContext context)
        {
            _context = context;
        }

        // GET: Immeubles
        public async Task<IActionResult> Index()
        {
            var gestionsyndicContext = _context.Immeubles.Include(i => i.IdSyndicNavigation);
            return View(await gestionsyndicContext.ToListAsync());
        }

        // GET: Immeubles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var immeuble = await _context.Immeubles
                .Include(i => i.IdSyndicNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (immeuble == null)
            {
                return NotFound();
            }

            return View(immeuble);
        }

        // GET: Immeubles/Create
        public IActionResult Create()
        {
            ViewData["IdSyndic"] = new SelectList(_context.Syndics, "Id", "Id");
            return View();
        }

        // POST: Immeubles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Adresse,NombreEtages,IdSyndic")] Immeuble immeuble)
        {
            if (ModelState.IsValid)
            {
                _context.Add(immeuble);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdSyndic"] = new SelectList(_context.Syndics, "Id", "Id", immeuble.IdSyndic);
            return View(immeuble);
        }

        // GET: Immeubles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var immeuble = await _context.Immeubles.FindAsync(id);
            if (immeuble == null)
            {
                return NotFound();
            }
            ViewData["IdSyndic"] = new SelectList(_context.Syndics, "Id", "Id", immeuble.IdSyndic);
            return View(immeuble);
        }

        // POST: Immeubles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Adresse,NombreEtages,IdSyndic")] Immeuble immeuble)
        {
            if (id != immeuble.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(immeuble);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImmeubleExists(immeuble.Id))
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
            ViewData["IdSyndic"] = new SelectList(_context.Syndics, "Id", "Id", immeuble.IdSyndic);
            return View(immeuble);
        }

        // GET: Immeubles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var immeuble = await _context.Immeubles
                .Include(i => i.IdSyndicNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (immeuble == null)
            {
                return NotFound();
            }

            return View(immeuble);
        }

        // POST: Immeubles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var immeuble = await _context.Immeubles.FindAsync(id);
            if (immeuble != null)
            {
                _context.Immeubles.Remove(immeuble);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImmeubleExists(int id)
        {
            return _context.Immeubles.Any(e => e.Id == id);
        }
    }
}
