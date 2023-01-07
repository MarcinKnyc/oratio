using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oratio.Areas.Identity.Data;

namespace Oratio.Controllers
{
    public class UserActivationsController : Controller
    {
        private readonly UserContext _context;
        private readonly CurrentUserRepository _currentUserRepository;
        private readonly UserManager<OratioUser> _userManager;

        public UserActivationsController(
            UserContext context,
            CurrentUserRepository currentUserRepository,
            UserManager<OratioUser> userManager
            )
        {
            _context = context;
            _currentUserRepository = currentUserRepository;
            _userManager = userManager;
        }
        // GET: UserActivations
        public async Task<IActionResult> Index()
        {
            //if (!_currentUserRepository.isLoggedInAsModerator()) return Unauthorized();
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

        // GET: UserActivations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        // POST: Intentions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Email,EmailConfirmed,IsAdministrator,IsFaithful,IsModerator,IsActive,Id")] OratioUser user)
        {
            if (id.ToString() != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //var ss = _context.Users.FirstOrDefault(userTemp => userTemp.Id == id.ToString());
                //if (ss is null) return NotFound();
                //_context.Update(user);
                //await _context.SaveChangesAsync();
                OratioUser userFromDb = await _userManager.FindByIdAsync(user.Id);
                userFromDb.Email = user.Email;
                userFromDb.EmailConfirmed = user.EmailConfirmed;
                userFromDb.IsAdministrator = user.IsAdministrator;
                userFromDb.IsFaithful = user.IsFaithful;
                userFromDb.IsModerator = user.IsModerator;
                await _userManager.UpdateAsync(userFromDb);

                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
    }
}
