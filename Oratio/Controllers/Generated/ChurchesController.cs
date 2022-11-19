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
    public class ChurchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChurchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Churches
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Churches.Include(c => c.Parish);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Churches/Details/5
        public async Task<IActionResult> Details(Guid? id)
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

        // GET: Churches/Create
        public IActionResult Create()
        {
            ViewData["ParishId"] = new SelectList(_context.Parishes, "Id", "Id");
            return View();
        }

        // POST: Churches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ParishId")] Church church)
        {
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
            return View(church);
        }

        // POST: Churches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,ParishId")] Church church)
        {
            if (id != church.Id)
            {
                return NotFound();
            }

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
