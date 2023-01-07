using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oratio.Areas.Identity.Data;

namespace Oratio.Controllers
{
    public class UserActivationsController : Controller
    {
        private readonly UserContext _context;

        public UserActivationsController(UserContext context)
        {
            _context = context;
        }
        // GET: UserActivations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.Where(user => user.IsAdministrator).ToListAsync());
        }

        // GET: UserActivations/Activate/{Id}
        public IActionResult Activate(Guid? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }
            var user = _context.Users.Find(id.ToString());
            if (user == null) return NotFound();
            user.IsActive = true;
            _context.Update(user);
            _context.SaveChanges();

            return View("Index", _context.Users.Where(user => user.IsAdministrator).ToList());
        }

        // GET: UserActivations/Activate/{Id}
        public IActionResult Deactivate(Guid? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }
            var user = _context.Users.Find(id.ToString());
            if (user == null) return NotFound();
            user.IsActive = false;
            _context.Update(user);
            _context.SaveChanges();

            return View("Index", _context.Users.Where(user => user.IsAdministrator).ToList());
        }

        /*// POST: Intentions/Confirm/5
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
        }*/
    }
}
