﻿using System;
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
    public class IntentionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IntentionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Intentions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Intentions.Include(i => i.Mass);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Intentions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Intentions == null)
            {
                return NotFound();
            }

            var intention = await _context.Intentions
                .Include(i => i.Mass)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (intention == null)
            {
                return NotFound();
            }

            return View(intention);
        }

        // GET: Intentions/Create
        public IActionResult Create()
        {
            ViewData["MassId"] = new SelectList(_context.Mass, "Id", "Id");
            return View();
        }

        // POST: Intentions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AskedIntention,Offering,isPaid,isApproved,MassId,Id,OwnerId")] Intention intention)
        {
            if (ModelState.IsValid)
            {
                intention.Id = Guid.NewGuid();
                _context.Add(intention);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MassId"] = new SelectList(_context.Mass, "Id", "Id", intention.MassId);
            return View(intention);
        }

        // GET: Intentions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Intentions == null)
            {
                return NotFound();
            }

            var intention = await _context.Intentions.FindAsync(id);
            if (intention == null)
            {
                return NotFound();
            }
            ViewData["MassId"] = new SelectList(_context.Mass, "Id", "Id", intention.MassId);
            return View(intention);
        }

        // POST: Intentions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AskedIntention,Offering,isPaid,isApproved,MassId,Id,OwnerId")] Intention intention)
        {
            if (id != intention.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(intention);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IntentionExists(intention.Id))
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
            ViewData["MassId"] = new SelectList(_context.Mass, "Id", "Id", intention.MassId);
            return View(intention);
        }

        // GET: Intentions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Intentions == null)
            {
                return NotFound();
            }

            var intention = await _context.Intentions
                .Include(i => i.Mass)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (intention == null)
            {
                return NotFound();
            }

            return View(intention);
        }

        // POST: Intentions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Intentions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Intentions'  is null.");
            }
            var intention = await _context.Intentions.FindAsync(id);
            if (intention != null)
            {
                _context.Intentions.Remove(intention);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IntentionExists(Guid id)
        {
          return _context.Intentions.Any(e => e.Id == id);
        }
    }
}
