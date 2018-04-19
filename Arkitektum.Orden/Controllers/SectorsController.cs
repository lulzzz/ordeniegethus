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
using System.Net;
using Arkitektum.Orden.Utils;

namespace Arkitektum.Orden.Controllers
{
    [Authorize]
    public class SectorsController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly ISectorService _sectorService;
        private readonly IApplicationSectorService _applicationSectorService;
        private readonly IApplicationService _applicationService;

        public SectorsController(ApplicationDbContext context, ISectorService sectorService, ISecurityService securityService, IApplicationSectorService applicationSectorService, IApplicationService applicationService) : base(securityService)
        {
            _context = context;
            _sectorService = sectorService;
            _applicationSectorService = applicationSectorService;
            _applicationService = applicationService;
        }

        // GET: Sectors
        public async Task<IActionResult> Index()
        {
            var sectors = await _sectorService.GetAll();
            return View(new SectorViewModel().MapToEnumerable(sectors));

        }

        [HttpGet("/sectors/all")]
        public async Task<IActionResult> All()
        {
            var sectors = await _sectorService.GetAll();
            return Json(new SectorViewModel().MapToEnumerable(sectors));
        }

        // GET: Sectors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var currentOrganization = CurrentOrganization();

            if (id == null)
            {
                return NotFound();
            }

            var sector = await _sectorService.GetSectorWithNoTracking(id.Value);

            if (sector == null)
            {
                return NotFound();
            }

           var applicationsForSector = await _sectorService.GetApplicationsForSector(id.Value, currentOrganization.Id);

           sector.PopulateSectorApplications(applicationsForSector);

   
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
            var data = new Sector()
            {
                Id = 2,
                Name = "Helse og omsorg",
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



            if (ModelState.IsValid)
            {
                await _sectorService.Create(sectorToCreate);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Sectors/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var sector = await _sectorService.GetSectorWithTracking(id.Value);

            if (sector == null)
            {
                return NotFound();
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


            if (ModelState.IsValid)
            {
                try
                {
                    await _sectorService.Update(id, sectorToEdit);
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

            var sector = await _context.Sector.SingleOrDefaultAsync(m => m.Id == id);

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

        [HttpGet("/sectors/application/{applicationId}")]
        public async Task<IActionResult> GetApplicationSectors(int applicationId)
        {
            if (applicationId == 0)
                return NotFound();

            var application = await _applicationService.GetAsync(applicationId);
            if (application == null)
                return NotFound();

            if (!_securityService.CurrrentUserHasAccessToApplication(application, AccessLevel.Read))
                return Forbid();

            var sectors = await _applicationSectorService.GetSectorsForApplication(applicationId);
            return Json(SectorApplicationViewModel.Map(sectors, applicationId));
        }

        [HttpPost("/sectors/application")]
        public async Task<IActionResult> CreateApplicationSector([FromBody] SectorApplicationViewModel model)
        {
            await _applicationSectorService.CreateApplicationSector(model.ApplicationId, model.SectorId);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete("/sectors/application/{sectorId}/{applicationId}")]
        public async Task<IActionResult> DeleteApplicationSector(int sectorId, int applicationId)
        {
            await _applicationSectorService.DeleteApplicationSector(applicationId, sectorId);
            return StatusCode(HttpStatusCode.NoContent);
        }

        private bool SectorExists(int id)
        {
            return _context.Sector.Any(e => e.Id == id);
        }


        private bool HasAccessTo(Sector sector)
        {
            return true;
        }
    }
}
