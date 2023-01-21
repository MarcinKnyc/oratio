using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Oratio.Data;
using Oratio.Models;

namespace Oratio.Controllers.Generated
{
    public class MassesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Masses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Mass
                .Include(m => m.Church)
                .Include(m => m.Intentions);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Masses/Details/5
        public async Task<IActionResult> Details(Guid? id)
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

        // GET: Masses/Create
        public IActionResult Create()
        {
            ViewData["ChurchId"] = new SelectList(_context.Churches, "Id", "Id");
            return View();
        }

        // POST: Masses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DateTime,ChurchId,Id,OwnerId")] Mass mass)
        {
            if (ModelState.IsValid)
            {
                mass.Id = Guid.NewGuid();
                _context.Add(mass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChurchId"] = new SelectList(_context.Churches, "Id", "Id", mass.ChurchId);
            return View(mass);
        }

        // GET: Masses/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Mass == null)
            {
                return NotFound();
            }

            var mass = await _context.Mass.FindAsync(id);
            if (mass == null)
            {
                return NotFound();
            }
            ViewData["ChurchId"] = new SelectList(_context.Churches, "Id", "Id", mass.ChurchId);
            return View(mass);
        }

        // POST: Masses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DateTime,ChurchId,Id,OwnerId")] Mass mass)
        {
            if (id != mass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
            ViewData["ChurchId"] = new SelectList(_context.Churches, "Id", "Id", mass.ChurchId);
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

        private bool MassExists(Guid id)
        {
          return _context.Mass.Any(e => e.Id == id);
        }
    }
}
