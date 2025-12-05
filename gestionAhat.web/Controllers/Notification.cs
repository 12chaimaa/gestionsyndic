using gestionsyndic.web.Models;
using gestionsyndic.web.newfolder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace gestionsyndic.web.Controllers
{
    public class NotificationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotificationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ------------------------- INDEX -------------------------
        public async Task<IActionResult> Index()
        {
            var notifications = await _context.Notifications
                .Include(n => n.Destinataire)
                .ToListAsync();

            return View(notifications);
        }

        // ------------------------- DETAILS -------------------------
        public async Task<IActionResult> Details(int id)
        {
            var notification = await _context.Notifications
                .Include(n => n.Destinataire)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (notification == null)
                return NotFound();

            return View(notification);
        }

        // ------------------------- CREATE GET -------------------------
        public IActionResult Create()
        {
            ViewData["DestinataireId"] = new SelectList(_context.Utilisateurs, "Id", "Nom");
            return View();
        }

        // ------------------------- CREATE POST -------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Message,DateEnvoi,Lu,DestinataireId")] Notification notification)
        {
            if (ModelState.IsValid)
            {
                notification.DateEnvoi = DateTime.Now;
                notification.Lu = false;

                _context.Add(notification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["DestinataireId"] = new SelectList(_context.Utilisateurs, "Id", "Nom", notification.DestinataireId);
            return View(notification);
        }

        // ------------------------- EDIT GET -------------------------
        public async Task<IActionResult> Edit(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);

            if (notification == null)
                return NotFound();

            ViewData["DestinataireId"] = new SelectList(_context.Utilisateurs, "Id", "Nom", notification.DestinataireId);
            return View(notification);
        }

        // ------------------------- EDIT POST -------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Message,DateEnvoi,Lu,DestinataireId")] Notification notification)
        {
            if (id != notification.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Notifications.Any(n => n.Id == notification.Id))
                        return NotFound();

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["DestinataireId"] = new SelectList(_context.Utilisateurs, "Id", "Nom", notification.DestinataireId);
            return View(notification);
        }

        // ------------------------- DELETE GET -------------------------
        public async Task<IActionResult> Delete(int id)
        {
            var notification = await _context.Notifications
                .Include(n => n.Destinataire)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (notification == null)
                return NotFound();

            return View(notification);
        }

        // ------------------------- DELETE POST -------------------------
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification != null)
            {
                _context.Notifications.Remove(notification);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
