using Microsoft.AspNetCore.Mvc;
using Oratio.Areas.Identity.Data;
using Oratio.Data;

namespace Oratio.Controllers
{
    public class IntentionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private CurrentUserRepository _currentUserRepository;

        public IntentionsController(ApplicationDbContext context, CurrentUserRepository currentUserRepository)
        {
            _context = context;
            _currentUserRepository = currentUserRepository;
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

        //Path: Masses/Approve/askjd-ajshdakjjjjjjjjjjjjjjjjj-asd
        public async Task<IActionResult> Approve(Guid id)
        {
            if (! _currentUserRepository.isLoggedIn() || !_currentUserRepository.isLoggedInAsParish())
            {
                return Unauthorized("Only accessible for parishes.");
            }
            var intention = await _context.Intentions.FindAsync(id);
            if (intention == null)
            {
                return NotFound("Intention with given id not found");
            }

            intention.isApproved = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
