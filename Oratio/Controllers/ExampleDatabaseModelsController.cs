using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Oratio.Data;
using Oratio.Models;

namespace Oratio.Controllers
{
    public class ExampleDatabaseModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExampleDatabaseModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ExampleDatabaseModels
        public async Task<IActionResult> Index()
        {
              return View(await _context.Models.ToListAsync());
        }

        // GET: ExampleDatabaseModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Models == null)
            {
                return NotFound();
            }

            var exampleDatabaseModel = await _context.Models
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exampleDatabaseModel == null)
            {
                return NotFound();
            }

            return View(exampleDatabaseModel);
        }

        // GET: ExampleDatabaseModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExampleDatabaseModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,State")] ExampleDatabaseModel exampleDatabaseModel)
        {
            if (ModelState.IsValid)
            {
                exampleDatabaseModel.Id = Guid.NewGuid();
                _context.Add(exampleDatabaseModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(exampleDatabaseModel);
        }

        // GET: ExampleDatabaseModels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Models == null)
            {
                return NotFound();
            }

            var exampleDatabaseModel = await _context.Models.FindAsync(id);
            if (exampleDatabaseModel == null)
            {
                return NotFound();
            }
            return View(exampleDatabaseModel);
        }

        // POST: ExampleDatabaseModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,State")] ExampleDatabaseModel exampleDatabaseModel)
        {
            if (id != exampleDatabaseModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exampleDatabaseModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExampleDatabaseModelExists(exampleDatabaseModel.Id))
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
            return View(exampleDatabaseModel);
        }

        // GET: ExampleDatabaseModels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Models == null)
            {
                return NotFound();
            }

            var exampleDatabaseModel = await _context.Models
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exampleDatabaseModel == null)
            {
                return NotFound();
            }

            return View(exampleDatabaseModel);
        }

        // POST: ExampleDatabaseModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Models == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Models'  is null.");
            }
            var exampleDatabaseModel = await _context.Models.FindAsync(id);
            if (exampleDatabaseModel != null)
            {
                _context.Models.Remove(exampleDatabaseModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExampleDatabaseModelExists(Guid id)
        {
          return _context.Models.Any(e => e.Id == id);
        }
    }
}
