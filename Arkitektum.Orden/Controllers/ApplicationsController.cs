using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Arkitektum.Orden.Models.ViewModels;
using Arkitektum.Orden.Services;
using Microsoft.AspNetCore.Authorization;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Services.AppRegistry;
using System;
using Arkitektum.Orden.Utils;

namespace Arkitektum.Orden.Controllers
{
    
    [Authorize]
    [Route("/applications")]
    public class ApplicationsController : BaseController
    {
        private readonly IApplicationService _applicationService;
        private readonly IUserService _userService;
        private readonly ISectorService _sectorService;
        private readonly INationalComponentService _nationalComponentsService;
        private readonly IAppRegistry _appRegistry;
        private readonly IVendorService _vendorService;

        public ApplicationsController(ISecurityService securityService, IApplicationService applicationService, IUserService userService, 
            ISectorService sectorService, INationalComponentService nationalComponentsService, IAppRegistry appRegistry, IVendorService vendorService) : base(securityService)
        {
            _applicationService = applicationService;
            _userService = userService;
            _sectorService = sectorService;
            _nationalComponentsService = nationalComponentsService;
            _appRegistry = appRegistry;
            _vendorService = vendorService;
        }
       
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {            
            SimpleOrganization currentOrganization = CurrentOrganization();
            var applications = await _applicationService.GetAllApplicationsForOrganisation(currentOrganization.Id);
            return View(new ApplicationViewModel().MapToEnumerable(applications));
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            return View(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> DetailsJson(int? id)
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

            return Json(new ApplicationViewModel().Map(application));
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            var model = new ApplicationViewModel();
            model.OrganizationId = CurrentOrganizationId();
            model.AvailableSystemOwners = await GetAvailableSystemOwners(model);
            model.AvailableVendors = await GetAvailableVendors();
            model.Sectors = await GetAvailableSectors();
            model.NationalComponents = await GetNationalComponents();
            model.AvailableHostingLocations = GetAvailableHostingLocations();
            return View(model);
        }

        private List<SelectListItem> GetAvailableHostingLocations(string currentValue = null)
        {
            var items = new List<SelectListItem>()
            {
                new SelectListItem{ Text = UIResource.SelectHostingLocationFromList, Value = null },
                new SelectListItem{ Text = UIResource.HostingLocationCloud, Value = HostingLocation.Cloud.ToString()},
                new SelectListItem{ Text = UIResource.HostingLocationLocalServer, Value = HostingLocation.LocalServer.ToString()},
                new SelectListItem{ Text = UIResource.HostingLocationExternalServer, Value = HostingLocation.ExternalServer.ToString()},
            };

            if (currentValue != null)
            {
                foreach(var item in items)
                {
                    if (item.Value == currentValue)
                        item.Selected = true;
                }
            }

            return items;
        }

        private async Task<List<SelectListItem>> GetAvailableSystemOwners(ApplicationViewModel model)
        {
            var availableSystemOwners = new List<SelectListItem>();
            foreach (var applicationUser in await _userService.GetAll())
            {
                availableSystemOwners.Add(new SelectListItem()
                {
                    Text = applicationUser.FullName,
                    Value = applicationUser.Id
                });
            }
            return availableSystemOwners;
        }

        private async Task<List<SelectListItem>> GetAvailableVendors(int currentVendorId = 0)
        {
            var vendors = new List<SelectListItem>();
            vendors.Add(new SelectListItem{ Text = UIResource.SelectVendorFromList, Value = "0"});

            foreach (var vendor in await _vendorService.GetAll())
            {
                vendors.Add(new SelectListItem {
                    Text = vendor.Name,
                    Value = vendor.Id.ToString(),
                    Selected = vendor.Id == currentVendorId
                    });
            }
            return vendors;
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
            
            foreach (var sector in await _sectorService.GetAll())
            {
                availableSectors.Add(new CheckboxApplicationSector()
                {
                    SectorId = sector.Id,
                    SectorName = sector.Name,
                });
            }
            
            return availableSectors;
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Version,VendorId,VendorName,AnnualFee,InitialCost,HostingVendor,HostingLocation,NumberOfUsers,SystemOwner,Sectors,NationalComponents")] ApplicationViewModel model)
        {
            // always set organizationId to currentOrganization to prevent any security issues
            model.OrganizationId = CurrentOrganizationId();
            
            if (ModelState.IsValid)
            {
                var createApplication = await _applicationService.Create(new ApplicationViewModel().Map(model));
                return RedirectToAction(nameof(Index));
            }

            model.AvailableSystemOwners = await GetAvailableSystemOwners(model);
            model.AvailableVendors = await GetAvailableVendors();
            model.Sectors = await GetAvailableSectors();
            model.NationalComponents = await GetNationalComponents();
            model.AvailableHostingLocations = GetAvailableHostingLocations(model.HostingLocation);

            return View(model);
        }

        [HttpGet("edit/{id}")]
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

            model.AvailableSystemOwners = await GetAvailableSystemOwners(model);
            model.AvailableVendors = await GetAvailableVendors(model.VendorId);
            model.NationalComponents = await GetNationalComponents();
            model.AvailableHostingLocations = GetAvailableHostingLocations(model.HostingLocation);
            model.MergeSectors(await GetAvailableSectors());

            return View(model);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Version,VendorId,VendorName,AnnualFee,InitialCost,HostingVendor,HostingLocation,NumberOfUsers,SystemOwner,Sectors,NationalComponents")] ApplicationViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            model.OrganizationId = CurrentOrganizationId();

            if (ModelState.IsValid)
            {
                await _applicationService.UpdateAsync(id, new ApplicationViewModel().Map(model));
                FlashSuccess(UIResource.FlashApplicationUpdated);
                return RedirectToAction(nameof(Details), new { id });
            }

            model.AvailableSystemOwners = await GetAvailableSystemOwners(model);
            model.AvailableVendors = await GetAvailableVendors(model.VendorId);
            model.NationalComponents = await GetNationalComponents();
            model.AvailableHostingLocations = GetAvailableHostingLocations(model.HostingLocation);
            model.MergeSectors(await GetAvailableSectors());

            return View(model);
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            
            var applicationToDelete = await _applicationService.GetAsync(id.Value);
            if (applicationToDelete == null) return NotFound();
            
            return View(applicationToDelete);
        }

        [HttpPost("delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _applicationService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("submit-app-registry/{id}")]
        public async Task<IActionResult> SubmitAppRegistry(int id)
        {
            Application application = await _applicationService.GetAsync(id);
            return View(new ApplicationViewModel().Map(application));
        }

        [HttpPost("submit-app-registry/{id}")]
        public async Task<IActionResult> SubmitAppRegistryConfirm(int id)
        {
            await _appRegistry.SubmitApplication(id);

            return RedirectToAction(nameof(Details), new {id});
        }
    }
}
