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
    public class ParishesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParishesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Parishes
        public async Task<IActionResult> Index()
        {
              return View(await _context.Parishes.ToListAsync());
        }

        // GET: Parishes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Parishes == null)
            {
                return NotFound();
            }

            var parish = await _context.Parishes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parish == null)
            {
                return NotFound();
            }

            return View(parish);
        }

        // GET: Parishes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parishes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Dedicated,MinimumOffering,Id,OwnerId")] Parish parish)
        {
            if (ModelState.IsValid)
            {
                parish.Id = Guid.NewGuid();
                _context.Add(parish);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parish);
        }

        // GET: Parishes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Parishes == null)
            {
                return NotFound();
            }

            var parish = await _context.Parishes.FindAsync(id);
            if (parish == null)
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
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Dedicated,MinimumOffering,Id,OwnerId")] Parish parish)
        {
            if (id != parish.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parish);
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

        // GET: Parishes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Parishes == null)
            {
                return NotFound();
            }

            var parish = await _context.Parishes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parish == null)
            {
                return NotFound();
            }

            return View(parish);
        }

        // POST: Parishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Parishes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Parishes'  is null.");
            }
            var parish = await _context.Parishes.FindAsync(id);
            if (parish != null)
            {
                _context.Parishes.Remove(parish);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParishExists(Guid id)
        {
          return _context.Parishes.Any(e => e.Id == id);
        }
    }
}
