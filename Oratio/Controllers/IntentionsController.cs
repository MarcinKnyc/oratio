using Microsoft.AspNetCore.Mvc;
using Oratio.Data;

namespace Oratio.Controllers
{
    public class IntentionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IntentionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Intentions/Confirm
        public IActionResult Confirm()
        {
            return View("/Views/Intentions/ConfirmManual.cshtml");
        }

        // POST: Intentions/Confirm/5
        [HttpPost, ActionName("Confirm")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirm(Guid id)
        {
            var intention = await _context.Intentions.FindAsync(id);

            if (intention != null)
            {
                intention.isPaid = true;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
