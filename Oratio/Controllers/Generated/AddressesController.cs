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
    public class AddressesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CurrentUserRepository _currentUserRepository;

        public AddressesController(ApplicationDbContext context, CurrentUserRepository parishLinkRepository)
        {
            _context = context;
            _currentUserRepository = parishLinkRepository;
        }

        // GET: Addresses
        public async Task<IActionResult> Index()
        {
            // var applicationDbContext = _context.Addresses.Include(a => a.Church);
            if (!_currentUserRepository.isLoggedIn() || !_currentUserRepository.isLoggedInAsParish())
                return Unauthorized("Only available for parishes.");
            var parishId = _currentUserRepository.getParishIdForLoggedUser();         

            var applicationDbContext = _context.Addresses
                .Include(address => address.Church)
                .Include(address => address.Church.Parish)
                .Where(address => address.Church.ParishId.ToString() == parishId);
           
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Addresses/Create
        public IActionResult Create()
        {
            if (!_currentUserRepository.isLoggedIn() || !_currentUserRepository.isLoggedInAsParish())
                return Unauthorized("Only available for parishes.");

            ViewData["ChurchId"] = getChurchIdSelectItems();
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StreetName,StreetNumber,City,ZipCode,ChurchId,Id")] Address address)
        {
            if (!_currentUserRepository.isLoggedIn() || !_currentUserRepository.isLoggedInAsParish())
                return Unauthorized("Only available for parishes.");

            address.OwnerId = _currentUserRepository.getCurrentUserId();

            if (ModelState.IsValid)
            {
                address.Id = Guid.NewGuid();
                _context.Add(address);
                await _context.SaveChangesAsync();           

                return RedirectToAction(nameof(Index));
            }

            ViewData["ChurchId"] = getChurchIdSelectItems();
            return View(address);
        }
        // GET: Addresses/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (!_currentUserRepository.isLoggedIn() || !_currentUserRepository.isLoggedInAsParish())
                return Unauthorized("Only available for parishes.");

            if (id == null || _context.Addresses == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            ViewData["ChurchId"] = getChurchIdSelectItems();
            return View(address);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("StreetName,StreetNumber,City,ZipCode,ChurchId,Id,OwnerId")] Address address)
        {
            if (!_currentUserRepository.isLoggedIn() || !_currentUserRepository.isLoggedInAsParish())
                return Unauthorized("Only available for parishes.");

            if (id != address.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(address);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.Id))
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
            return View(address);
        }

        // GET: Addresses/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Addresses == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses
                .Include(a => a.Church)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Addresses == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Addresses'  is null.");
            }
            var address = await _context.Addresses.FindAsync(id);
            if (address != null)
            {
                _context.Addresses.Remove(address);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private List<SelectListItem> getChurchIdSelectItems()
        {
            return _context.Churches
                .Include(c => c.Address)
                .Where(c => c.ParishId.ToString() == _currentUserRepository.getParishIdForLoggedUser())
                .Select(c => new SelectListItem(
                $"{c.Address.City}, {c.Name}",
                c.Id.ToString()
            ))
                .ToList();
        }

        private bool AddressExists(Guid id)
        {
          return _context.Addresses.Any(e => e.Id == id);
        }
    }
}
