using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Microsoft.AspNetCore.Authorization;

namespace Arkitektum.Orden.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class NationalComponentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NationalComponentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NationalComponents
        public async Task<IActionResult> Index()
        {
            return View(await _context.NationalComponent.ToListAsync());
        }

        // GET: NationalComponents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nationalComponent = await _context.NationalComponent
                .SingleOrDefaultAsync(m => m.Id == id);
            if (nationalComponent == null)
            {
                return NotFound();
            }

            return View(nationalComponent);
        }

        // GET: NationalComponents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NationalComponents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] NationalComponent nationalComponent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nationalComponent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nationalComponent);
        }

        // GET: NationalComponents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nationalComponent = await _context.NationalComponent.SingleOrDefaultAsync(m => m.Id == id);
            if (nationalComponent == null)
            {
                return NotFound();
            }
            return View(nationalComponent);
        }

        // POST: NationalComponents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] NationalComponent nationalComponent)
        {
            if (id != nationalComponent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nationalComponent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NationalComponentExists(nationalComponent.Id))
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
            return View(nationalComponent);
        }

        // GET: NationalComponents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nationalComponent = await _context.NationalComponent
                .SingleOrDefaultAsync(m => m.Id == id);
            if (nationalComponent == null)
            {
                return NotFound();
            }

            return View(nationalComponent);
        }

        // POST: NationalComponents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nationalComponent = await _context.NationalComponent.SingleOrDefaultAsync(m => m.Id == id);
            _context.NationalComponent.Remove(nationalComponent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NationalComponentExists(int id)
        {
            return _context.NationalComponent.Any(e => e.Id == id);
        }
    }
}
