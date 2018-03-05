using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.ViewModels;
using Arkitektum.Orden.Services;

namespace Arkitektum.Orden.Controllers
{
    public class SectorsController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public SectorsController(ApplicationDbContext context, ISecurityService securityService) : base(securityService)
        {
            _context = context;
        }

        // GET: Sectors
        public async Task<IActionResult> Index()
        {
            SimpleOrganization currentOrganization = CurrentOrganization();
            var applicationDbContext = _context.Sector.Where(s => s.OrganizationId == currentOrganization.Id);
            var sectors = await applicationDbContext.ToListAsync();
            return View(sectors);
        }

        // GET: Sectors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sector = await _context.Sector
                .Include(s => s.Organization)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (sector == null)
            {
                return NotFound();
            }

            return View(sector);
        }

        // GET: Sectors/Create
        public IActionResult Create()
        {
            ViewData["CurrentOrganizationId"] = CurrentOrganizationId();
            ViewData["OrganizationId"] = new SelectList(_context.Organization, "Id", "Id");
            return View();
        }

        public IActionResult EditJson()
        {
            var data = new Sector() {
                Id = 2, Name = "Helse og omsorg",
                LawReferences = new List<ResourceLink>() {
                    new ResourceLink() {
                        Id = 0,
                        Description = "Beskrivelse av første lovreferanse",
                        Url = "www.lenketilfoerstelovreferanse.no"
                    },
                    new ResourceLink() {
                        Id = 0,
                        Description = "Beskrivelse av andre lovreferanse",
                        Url = "www.lenketilandrelovreferanse.no"
                    },
                }
            };
            return Json(data);
        }

        // POST: Sectors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,OrganizationId")] Sector sector)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sector);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrganizationId"] = new SelectList(_context.Organization, "Id", "Id", sector.OrganizationId);
            return View(sector);
        }

        // GET: Sectors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sector = await _context.Sector.SingleOrDefaultAsync(m => m.Id == id);
            if (sector == null)
            {
                return NotFound();
            }
            ViewData["OrganizationId"] = new SelectList(_context.Organization, "Id", "Id", sector.OrganizationId);
            return View(sector);
        }

        // POST: Sectors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,OrganizationId")] Sector sector)
        {
            if (id != sector.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sector);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectorExists(sector.Id))
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
            ViewData["OrganizationId"] = new SelectList(_context.Organization, "Id", "Id", sector.OrganizationId);
            return View(sector);
        }

        // GET: Sectors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sector = await _context.Sector
                .Include(s => s.Organization)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (sector == null)
            {
                return NotFound();
            }

            return View(sector);
        }

        // POST: Sectors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sector = await _context.Sector.SingleOrDefaultAsync(m => m.Id == id);
            _context.Sector.Remove(sector);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SectorExists(int id)
        {
            return _context.Sector.Any(e => e.Id == id);
        }
    }
}
