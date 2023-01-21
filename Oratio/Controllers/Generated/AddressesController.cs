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
    public class AddressesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AddressesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Addresses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Addresses.Include(a => a.Church);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Addresses/Create
        public IActionResult Create()
        {
            ViewData["ChurchId"] = new SelectList(_context.Churches, "Id", "Id");
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StreetName,StreetNumber,City,ZipCode,ChurchId,Id,OwnerId")] Address address)
        {
            if (ModelState.IsValid)
            {
                address.Id = Guid.NewGuid();
                _context.Add(address);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChurchId"] = new SelectList(_context.Churches, "Id", "Id", address.ChurchId);
            return View(address);
        }

        private bool AddressExists(Guid id)
        {
          return _context.Addresses.Any(e => e.Id == id);
        }
    }
}
