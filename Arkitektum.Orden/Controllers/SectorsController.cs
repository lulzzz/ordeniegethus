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
using Microsoft.AspNetCore.Authorization;

namespace Arkitektum.Orden.Controllers
{
    [Authorize]
    public class SectorsController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly ISectorService _sectorService;

        public SectorsController(ApplicationDbContext context, ISectorService sectorService, ISecurityService securityService) : base(securityService)
        {
            _context = context;
            _sectorService = sectorService;
        }

        // GET: Sectors
        public async Task<IActionResult> Index()
        {
            SimpleOrganization currentOrganization = CurrentOrganization();
            var sectors = await _sectorService.GetSectorsForOrganization(currentOrganization.Id);
            return View(new SectorViewModel().MapToEnumerable(sectors));
        }

        // GET: Sectors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var sector = await _sectorService.GetAsync(id.Value);
            if (sector == null)
            {
                return NotFound();
            }

            if (!HasAccessTo(sector))
                return Forbid();

            return View(new SectorViewModel().Map(sector));
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
        public async Task<IActionResult> Create([Bind("Id,Name,OrganizationId")] SectorViewModel sector)
        {
            var sectorToCreate = new SectorViewModel().Map(sector);

            sectorToCreate.OrganizationId = CurrentOrganizationId();

            if (ModelState.IsValid)
            {
                await _sectorService.Create(sectorToCreate);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Sectors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
        
            var sector = await _sectorService.GetAsync(id.Value);

            if (sector == null)
            {
                return NotFound();
            }

            if (!HasAccessTo(sector))
            {
                return Forbid();
            }
            
            return View(new SectorViewModel().Map(sector));
        }

        // POST: Sectors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,OrganizationId")] SectorViewModel sector)
        {
            if (id != sector.Id)
            {
                return NotFound();
            }

            var sectorToEdit = new SectorViewModel().Map(sector);
            sectorToEdit.OrganizationId = CurrentOrganizationId();

            if (ModelState.IsValid)
            {
                try
                {
                    await _sectorService.UpdateAsync(id, sectorToEdit);
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
            return View(nameof(Index));
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

            if (!HasAccessTo(sector))
            {
                return Forbid();
            }

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
            if (HasAccessTo(sector))
            {
                _context.Sector.Remove(sector);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return Forbid();
            }
            
        }

        private bool SectorExists(int id)
        {
            return _context.Sector.Any(e => e.Id == id);
        }


        private bool HasAccessTo(Sector sector)
        {
            return sector?.OrganizationId == CurrentOrganizationId();
        }
    }
}
