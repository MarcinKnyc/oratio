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
    public class ParishesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private CurrentUserRepository _currentUserRepository;

        public ParishesController(
            ApplicationDbContext context,
            CurrentUserRepository currentUserRepository
            )
        {
            _context = context;
            _currentUserRepository = currentUserRepository;
        }

        // GET: Parishes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Parishes.ToListAsync());
        }

        // GET: Parishes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Parishes == null)
            {
                return NotFound();
            }

            var parish = await _context.Parishes.FindAsync(id);
            if (parish == null || 
                parish.OwnerId.ToString() != _currentUserRepository.getCurrentUserId().ToString()
                )
            {
                return NotFound();
            }
            return View(parish);
        }

        // POST: Parishes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Dedicated,MinimumOffering,Id, OwnerId")] Parish parish)
        {
            if (id != parish.Id)
            {
                return NotFound();
            }
            // care: i have found an easier way to do this (<input hidden asp-for=OwnerId>),
            // but I don't wanna re-do this so i leave this here.

            Parish? parishDb = _context.Parishes.FirstOrDefault(p => p.Id == id);
            if (parishDb == null) { return NotFound("Parish with given Id not found in Db."); }

            parishDb.MinimumOffering = parish.MinimumOffering;
            parishDb.Name = parish.Name;
            parishDb.Dedicated = parish.Dedicated;


            Guid? currentUserId = _currentUserRepository.getCurrentUserId();
            if (currentUserId == null || parishDb.OwnerId == null)
            {
                return NotFound("Not logged in or parish doesn't have owner.");
            }
            if(currentUserId.Value.ToString() != parishDb.OwnerId.Value.ToString())
            {
                return base.Unauthorized($"The modified parish belongs to {parishDb.OwnerId}. It doesn't belong to the user who tried to modify it: {currentUserId}");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parishDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParishExists(parish.Id))
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
            return View(parish);
        }

        private bool ParishExists(Guid id)
        {
          return _context.Parishes.Any(e => e.Id == id);
        }
    }
}
