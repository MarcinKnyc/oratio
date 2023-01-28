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
    public class MassesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private CurrentUserRepository _currentUserRepository;

        public MassesController(ApplicationDbContext context, CurrentUserRepository currentUserRepository)
        {
            _context = context;
            _currentUserRepository = currentUserRepository;
        }

        // GET: Masses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Mass
                .Include(m => m.Church)
                .Include(m => m.Intentions);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Masses/Create
        public IActionResult Create()
        {
            if (! _currentUserRepository.isLoggedIn() || ! _currentUserRepository.isLoggedInAsParish())
            {
                return Unauthorized("Only accessible if you're logged in as parish");
            }
            ViewData["ChurchId"] = getChurchIdSelectItems();
            return View();
        }

        // POST: Masses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DateTime,ChurchId,Id")] Mass mass)
        {
            if (!_currentUserRepository.isLoggedIn() || !_currentUserRepository.isLoggedInAsParish())
            {
                return Unauthorized("Only accessible if you're logged in as parish");
            }
            mass.OwnerId = _currentUserRepository.getCurrentUserId();
            if (ModelState.IsValid)
            {
                mass.Id = Guid.NewGuid();
                _context.Add(mass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChurchId"] = getChurchIdSelectItems();
            return View(mass);
        }

        // GET: Masses/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (!_currentUserRepository.isLoggedIn() || !_currentUserRepository.isLoggedInAsParish())
            {
                return Unauthorized("Only accessible if you're logged in as parish");
            }
            if (id == null || _context.Mass == null)
            {
                return NotFound();
            }

            var mass = await _context.Mass.FindAsync(id);
            if (mass == null)
            {
                return NotFound();
            }
            ViewData["ChurchId"] = getChurchIdSelectItems();
            return View(mass);
        }

        // POST: Masses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DateTime,ChurchId,Id,OwnerId")] Mass mass)
        {
            if (!_currentUserRepository.isLoggedIn() || !_currentUserRepository.isLoggedInAsParish())
            {
                return Unauthorized("Only accessible if you're logged in as parish");
            }
            if (id != mass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //todo: add validation, if church used actually belongs to my parish.
                try
                {
                    _context.Update(mass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MassExists(mass.Id))
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
            ViewData["ChurchId"] = getChurchIdSelectItems();
            return View(mass);
        }

        // GET: Masses/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Mass == null)
            {
                return NotFound();
            }

            var mass = await _context.Mass
                .Include(m => m.Church)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mass == null)
            {
                return NotFound();
            }

            return View(mass);
        }

        // POST: Masses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Mass == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Mass'  is null.");
            }
            var mass = await _context.Mass.FindAsync(id);
            if (mass != null)
            {
                _context.Mass.Remove(mass);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private List<SelectListItem> getChurchIdSelectItems()
        {
            return _context.Churches
                .Include(c=> c.Address)
                .Where(c => c.ParishId.ToString() == _currentUserRepository.getParishIdForLoggedUser())
                .Select(c => new SelectListItem(
                $"{c.Address.City}, {c.Name}",
                c.Id.ToString()
            ))
                .ToList();
        }

        private bool MassExists(Guid id)
        {
          return _context.Mass.Any(e => e.Id == id);
        }
    }
}
