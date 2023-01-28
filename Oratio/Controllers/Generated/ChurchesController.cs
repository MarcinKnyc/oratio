using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Oratio.Areas.Identity.Data;
using Oratio.Areas.Identity.Pages.Account.Manage;
using Oratio.Data;
using Oratio.Models;

namespace Oratio.Controllers.Generated
{
    public class ChurchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly CurrentUserRepository _currentUserRepository;

        public ChurchesController(ApplicationDbContext context, CurrentUserRepository currentUserRepository)
        {
            _context = context;
            _currentUserRepository = currentUserRepository;

        }

        // GET: Churches
        public async Task<IActionResult> Index()
        {
           
            if (_currentUserRepository.isLoggedInAsParish() == true)
            {
                var parishId = _currentUserRepository.getParishIdForLoggedUser();
                var applicationDbContext = _context.Churches
                    .Where(church => church.ParishId.ToString() == parishId);

                applicationDbContext
                    .Include(church => church.Parish)
                    .Include(church => church.Address);
                return View(await applicationDbContext.ToListAsync());
            }

            else
            {
                var applicationDbContext = _context.Churches.Include(church => church.Parish);
                return View(await applicationDbContext.ToListAsync());
            }
 
        }

        // GET: Churches/Create
        public IActionResult Create()
        {
            if (!_currentUserRepository.isLoggedInAsParish()) return Unauthorized("Only parish administrator can perform this action.");
            ViewData["ParishId"] = new SelectList(_context.Parishes, "Id", "Id");
            return View("/Views/Churches/CreateManual.cshtml"); 
        }

        // POST: Churches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ParishId,Id,OwnerId,Address.StreetName")] Church church)
        {
            if (!_currentUserRepository.isLoggedInAsParish()) return Unauthorized("Only parish administrator can perform this action.");


            church.ParishId = new Guid(_currentUserRepository.getParishIdForLoggedUser());
        
            church.OwnerId = (Guid)_currentUserRepository.getCurrentUserId();

            if (ModelState.IsValid)
            {
               
                church.Id = Guid.NewGuid();
                _context.Add(church);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParishId"] = new SelectList(_context.Parishes, "Id", "Id", church.ParishId);
            return View(church);
        }

        // GET: Churches/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (!_currentUserRepository.isLoggedInAsParish()) return Unauthorized("Only parish administrator can perform this action.");
            if (id == null || _context.Churches == null)
            {
                return NotFound();
            }

            var church = await _context.Churches.FindAsync(id);
            if (church == null)
            {
                return NotFound();
            }
            ViewData["ParishId"] = new SelectList(_context.Parishes, "Id", "Id", church.ParishId);
            return View("/Views/Churches/EditManual.cshtml"); 
        }

        // POST: Churches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,ParishId,Id,OwnerId")] Church church)
        {
            if (!_currentUserRepository.isLoggedInAsParish()) return Unauthorized("Only parish administrator can perform this action.");

            if (id != church.Id)
            {
                return NotFound();
            }
            church.ParishId = new Guid(_currentUserRepository.getParishIdForLoggedUser());



            church.OwnerId = (Guid)_currentUserRepository.getCurrentUserId();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(church);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChurchExists(church.Id))
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
            ViewData["ParishId"] = new SelectList(_context.Parishes, "Id", "Id", church.ParishId);
            return View(church);
        }

        // GET: Churches/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Churches == null)
            {
                return NotFound();
            }

            var church = await _context.Churches
                .Include(c => c.Parish)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (church == null)
            {
                return NotFound();
            }

            return View(church);
        }

        // POST: Churches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Churches == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Churches'  is null.");
            }
            var church = await _context.Churches.FindAsync(id);
            if (church != null)
            {
                _context.Churches.Remove(church);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChurchExists(Guid id)
        {
          return _context.Churches.Any(e => e.Id == id);
        }
    }
}
