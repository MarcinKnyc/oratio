using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Oratio.Areas.Identity.Data;
using Oratio.Data;
using Oratio.Models;

namespace Oratio.Controllers.Generated
{
    public class IntentionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CurrentUserRepository _currentUserRepository;

        public IntentionsController(ApplicationDbContext context, CurrentUserRepository currentUserRepository)
        {
            _context = context;
            _currentUserRepository = currentUserRepository;
        }

        // GET: Intentions
        public async Task<IActionResult> Index()
        {
            ViewData["Date"] = "asdasdasda";
            if (!_currentUserRepository.isLoggedIn()) return NotFound("Only accessible for users logged in");
            if (_currentUserRepository.isLoggedInAsParish())
            {
                var parishId = _currentUserRepository.getParishIdForLoggedUser();
                var applicationDbContext = _context.Intentions
                    .Where(intention => intention.Mass.Church.ParishId.ToString() == parishId)
                    .Include(intention => intention.Mass);
                return View(await applicationDbContext.ToListAsync());

            }
            else if (_currentUserRepository.isLoggedInAsFaithful())
            {
                var userId = _currentUserRepository.getCurrentUserId();
                if (userId == null) return NotFound("Only accessible for users logged in");
                var applicationDbContext = _context.Intentions
                    .Where(i => i.OwnerId == userId)
                    .Include(i => i.Mass);
                return View(await applicationDbContext.ToListAsync());
            }
            else //loggedInAsModerator
            {
                var applicationDbContext = _context.Intentions.Include(i => i.Mass);
                return View(await applicationDbContext.ToListAsync());
            }
        }


        // GET: Intentions/Create
        public IActionResult Create()
        {
            if (!_currentUserRepository.isLoggedIn()) return NotFound("Niedostępne dla niezalogowanych.");
            if (_currentUserRepository.isLoggedInAsParish()) return NotFound("Niedostępne dla administratorów parafii.");
            ViewData["MassId"] = getMassIdSelectItems();
            return View("/Views/Intentions/CreateManual.cshtml");
        }

        // POST: Intentions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AskedIntention,Offering,isPaid,isApproved,MassId,Id,OwnerId")] Intention intention)
        {
            var userId = _currentUserRepository.getCurrentUserId();
            Mass? mass = _context.Mass.Include(massses => massses.Church).FirstOrDefault(massses => massses.Id == intention.MassId);
            if (userId == null || _currentUserRepository.isLoggedInAsParish() || mass == null || mass.Church == null) 
            {
                ViewData["MassId"] = getMassIdSelectItems();
                return View("/Views/Intentions/CreateManual.cshtml", intention);
            }
            Parish? parishInDb = _context.Parishes.FirstOrDefault(parish => parish.Id == mass.Church.ParishId);
            float? minimalOffering = parishInDb != null ? parishInDb.MinimumOffering : null;
            if (!intention.ValidateOfferingAmount((int?)minimalOffering) || !ModelState.IsValid)
            {
                ViewData["MassId"] = getMassIdSelectItems();
                return View("/Views/Intentions/CreateManual.cshtml", intention);
            }
            intention.OwnerId = userId;        
            intention.Id = Guid.NewGuid();
            _context.Add(intention);
            await _context.SaveChangesAsync();
            return RedirectToAction("Confirm", new { intention.Id });
        }

        private List<SelectListItem> getMassIdSelectItems()
        {
            return _context.Mass.Select(m => new SelectListItem(
                $"{m.Church.Address.City}, {m.Church.Name}, {m.DateTime.ToShortDateString()} {m.DateTime.ToShortTimeString()}",
                m.Id.ToString()
            ))
                .ToList();
        }

        private bool IntentionExists(Guid id)
        {
          return _context.Intentions.Any(e => e.Id == id);
        }
    }
}
