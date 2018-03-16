using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Arkitektum.Orden.Models.ViewModels;
using Arkitektum.Orden.Services;
using Microsoft.AspNetCore.Authorization;

namespace Arkitektum.Orden.Controllers
{
    [Authorize]
    public class ApplicationsController : BaseController
    {
        private readonly IApplicationService _applicationService;
        private readonly IUserService _userService;
        private readonly ISectorService _sectorService;
        private readonly INationalComponentService _nationalComponentsService;

        public ApplicationsController(ISecurityService securityService, IApplicationService applicationService, IUserService userService, 
            ISectorService sectorService, INationalComponentService nationalComponentsService) : base(securityService)
        {
            _applicationService = applicationService;
            _userService = userService;
            _sectorService = sectorService;
            _nationalComponentsService = nationalComponentsService;
        }


        // GET: Applications
        public async Task<IActionResult> Index()
        {            
            SimpleOrganization currentOrganization = CurrentOrganization();
            var applications = await _applicationService.GetAllApplicationsForOrganisation(currentOrganization.Id);
            return View(new ApplicationViewModel().MapToEnumerable(applications));
        }

        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _applicationService.GetAsync(id.Value);
            if (application == null)
            {
                return NotFound();
            }

            return View(new ApplicationViewModel().Map(application));
        }

        // GET: Applications/Create
        public async Task<IActionResult> Create()
        {
            var model = new ApplicationViewModel();
            model.OrganizationId = CurrentOrganizationId();

            model.AvailableSuperUsers = new List<SelectListItem>();

            foreach (var applicationUser in await _userService.GetAll())
            {
                model.AvailableSuperUsers.Add(new SelectListItem()
                {
                    Text = applicationUser.FullName,
                    Value = applicationUser.Id
                });
            }

            model.Sectors = await GetAvailableSectors();

            model.NationalComponents = await GetNationalComponents();

            return View(model);

        }

        private async Task<List<CheckboxApplicationNationalComponents>> GetNationalComponents()
        {
            var nationalComponents = new List<CheckboxApplicationNationalComponents>();

            foreach (var nationalComponent in await _nationalComponentsService.GetAll())
            {
                nationalComponents.Add(new CheckboxApplicationNationalComponents()
                {
                    NationalComponentId = nationalComponent.Id,
                    NationalComponentName = nationalComponent.Name
                });
            }

            return nationalComponents;
        }

        private async Task<List<CheckboxApplicationSector>> GetAvailableSectors()
        {
            var availableSectors = new List<CheckboxApplicationSector>();

            foreach (var sector in await _sectorService.GetSectorsForOrganization(CurrentOrganizationId()))
            {
                availableSectors.Add(new CheckboxApplicationSector()
                {
                    SectorId = sector.Id,
                    SectorName = sector.Name,
                });
            }

            return availableSectors;
        }

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,AnnualFee,Vendor,SystemOwner,Version,OrganizationId,Sectors,NationalComponents")] ApplicationViewModel application)
        {
            if (ModelState.IsValid)
            {
                var createApplication = await _applicationService.Create(new ApplicationViewModel().Map(application));
                return RedirectToAction(nameof(Index));
            }

            return View(application);
        }

        // GET: Applications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _applicationService.GetAsync(id.Value);
            if (application == null)
            {
                return NotFound();
            }

            var model = new ApplicationViewModel().Map(application);

            model.AvailableSuperUsers = new List<SelectListItem>();

            foreach (var applicationUser in await _userService.GetAll())
            {
                model.AvailableSuperUsers.Add(new SelectListItem()
                {
                    Text = applicationUser.FullName,
                    Value = applicationUser.Id
                });
            }

            model.MergeSectors(await GetAvailableSectors());

            model.NationalComponents = await GetNationalComponents();

            return View(model);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Version,AnnualFee,InitialCost,HostingLocation,NumberOfUsers," +
                                                            "Vendor,OrganizationId,Sectors,NationalComponents")] ApplicationViewModel application)
        {
            if (id != application.Id)
            {
                return NotFound();
            }

            await _applicationService.UpdateAsync(id, application.Map(application));
            
            return View(application);
        }

        // GET: Applications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            
            var applicationToDelete = await _applicationService.GetAsync(id.Value);
            if (applicationToDelete == null) return NotFound();
            
            return View(applicationToDelete);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _applicationService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

       }
}
