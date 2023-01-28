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
    public class MassGenerationRulesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private CurrentUserRepository _currentUserRepository;

        public MassGenerationRulesController(ApplicationDbContext context, CurrentUserRepository currentUserRepository)
        {
            _context = context;
            _currentUserRepository = currentUserRepository;
        }

        // GET: MassGenerationRules
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MassGenerationRules.Include(m => m.Parish);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MassGenerationRules/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.MassGenerationRules == null)
            {
                return NotFound();
            }

            var massGenerationRule = await _context.MassGenerationRules
                .Include(m => m.Parish)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (massGenerationRule == null)
            {
                return NotFound();
            }

            return View(massGenerationRule);
        }

        // GET: MassGenerationRules/Create
        public IActionResult Create()
        {
            ViewData["ParishId"] = new SelectList(_context.Parishes, "Id", "Id");
            return View();
        }

        // POST: MassGenerationRules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TimesToRepeat,TimespanToRepeat,RuleTerminationTime,DayOfWeek,WeekNumber,IsActive,RuleStartTime,Id")] MassGenerationRule massGenerationRule)
        {

            massGenerationRule.OwnerId = _currentUserRepository.getCurrentUserId();
            massGenerationRule.ParishId = new Guid(_currentUserRepository.getParishIdForLoggedUser());

            if (ModelState.IsValid)
            {
                massGenerationRule.Id = Guid.NewGuid();
                _context.Add(massGenerationRule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParishId"] = new SelectList(_context.Parishes, "Id", "Id", massGenerationRule.ParishId);
            return View(massGenerationRule);
        }

        // GET: MassGenerationRules/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.MassGenerationRules == null)
            {
                return NotFound();
            }

            var massGenerationRule = await _context.MassGenerationRules.FindAsync(id);
            if (massGenerationRule == null)
            {
                return NotFound();
            }
            ViewData["ParishId"] = new SelectList(_context.Parishes, "Id", "Id", massGenerationRule.ParishId);
            return View(massGenerationRule);
        }

        // POST: MassGenerationRules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ParishId,TimesToRepeat,TimespanToRepeat,RuleTerminationTime,DayOfWeek,WeekNumber,IsActive,RuleStartTime,Id,OwnerId")] MassGenerationRule massGenerationRule)
        {
            if (id != massGenerationRule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(massGenerationRule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MassGenerationRuleExists(massGenerationRule.Id))
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
            ViewData["ParishId"] = new SelectList(_context.Parishes, "Id", "Id", massGenerationRule.ParishId);
            return View(massGenerationRule);
        }

        // GET: MassGenerationRules/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.MassGenerationRules == null)
            {
                return NotFound();
            }

            var massGenerationRule = await _context.MassGenerationRules
                .Include(m => m.Parish)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (massGenerationRule == null)
            {
                return NotFound();
            }

            return View(massGenerationRule);
        }

        // POST: MassGenerationRules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.MassGenerationRules == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MassGenerationRules'  is null.");
            }
            var massGenerationRule = await _context.MassGenerationRules.FindAsync(id);
            if (massGenerationRule != null)
            {
                _context.MassGenerationRules.Remove(massGenerationRule);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MassGenerationRuleExists(Guid id)
        {
          return _context.MassGenerationRules.Any(e => e.Id == id);
        }
    }
}
