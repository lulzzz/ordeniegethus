using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Arkitektum.Orden.Models.ViewModels;
using Arkitektum.Orden.Services;
using Microsoft.AspNetCore.Authorization;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Services.AppRegistry;
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
        public async Task<IActionResult> Index([FromQuery] int? sectorId)
        {
            if (!_securityService.CurrrentUserHasAccessToOrganization(CurrentOrganizationId(), AccessLevel.Read))
                return Forbid();

            SimpleOrganization currentOrganization = CurrentOrganization();
           
            IEnumerable<Application> applications = new List<Application>();

            var model = new ApplicationListViewModel();

            if (sectorId.HasValue)
            {
                applications =  await _applicationService.GetApplicationsWithFilter(currentOrganization.Id, sectorId.Value, 0, null);
            }
            else
            {
                applications = await _applicationService.GetAllApplicationsForOrganization(currentOrganization.Id);
            }

            model.Applications = new ApplicationListDetailViewModel().MapToEnumerable(applications);
            model.Sectors = await GetSectorViewModel(sectorId);

            return View(model);
        }

        private async Task<List<SelectListItem>> GetSectorViewModel(int? sectorId)
        {
            var sectors = await _sectorService.GetAll();
            return new SectorViewModel().MapToSelectListItems(sectors, sectorId);
        }

        private async Task<IEnumerable<ApplicationListDetailViewModel>> GetApplicationListViewModel(SimpleOrganization currentOrganization)
        {
            var applications = await _applicationService.GetAllApplicationsForOrganization(currentOrganization.Id);

            return new ApplicationListDetailViewModel().MapToEnumerable(applications);
        }


        [HttpGet("details/{id}")]
        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            int currentOrganizationId = CurrentOrganizationId();
            
            if (!_securityService.CurrrentUserHasAccessToOrganization(currentOrganizationId, AccessLevel.Read))
                return Forbid();

            bool hasWriteAccess =
                _securityService.CurrrentUserHasAccessToOrganization(currentOrganizationId, AccessLevel.Write);


            return View(new ApplicationDetailsViewModel() { ApplicationId = id.Value, OrganizationId = currentOrganizationId, HasWriteAccess = hasWriteAccess});
        }

        

        [HttpGet("create/{applicationQuery?}")]
        public async Task<IActionResult> Create([FromQuery] string applicationQuery)
        {
            if (!_securityService.CurrrentUserHasAccessToOrganization(CurrentOrganizationId(), AccessLevel.Write))
                return Forbid();

            var model = new ApplicationViewModel();
            model.OrganizationId = CurrentOrganizationId();
            model.AvailableSystemOwners = await GetAvailableSystemOwners();
            model.AvailableVendors = await GetAvailableVendors();

            if (applicationQuery != null)
            {
                model.Name = applicationQuery;
            }
            return View(model);
        }

        private async Task<List<SelectListItem>> GetAvailableSystemOwners(string systemOwnerId = null)
        {
            var availableSystemOwners = new List<SelectListItem>();
            availableSystemOwners.Add(new SelectListItem { Text = UIResource.SelectSystemOwnerFromList, Value = "" });

            foreach (var applicationUser in await _userService.GetSystemOwners(CurrentOrganizationId()))
            {
                availableSystemOwners.Add(new SelectListItem()
                {
                    Text = applicationUser.FullName,
                    Value = applicationUser.Id,
                    Selected = applicationUser.Id == systemOwnerId
                });
            }
            return availableSystemOwners;
        }

        private async Task<List<SelectListItem>> GetAvailableVendors(int currentVendorId = 0)
        {
            var vendors = new List<SelectListItem>();
            vendors.Add(new SelectListItem { Text = UIResource.SelectVendorFromList, Value = "0" });

            foreach (var vendor in await _vendorService.GetAll())
            {
                vendors.Add(new SelectListItem
                {
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
                    Id = nationalComponent.Id,
                    Name = nationalComponent.Name
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
        public async Task<IActionResult> Create([Bind("Name,Version,VendorId,VendorName,AnnualFee,InitialCost,PurchaseDate,HostingVendor,HostingLocation,NumberOfUsers,SystemOwnerId,Sectors,NationalComponents")] ApplicationViewModel model)
        {
            if (!_securityService.CurrrentUserHasAccessToOrganization(CurrentOrganizationId(), AccessLevel.Write))
                return Forbid();

            // always set organizationId to currentOrganization to prevent any security issues
            model.OrganizationId = CurrentOrganizationId();

            if (ModelState.IsValid)
            {
                Application createdApplication = await _applicationService.Create(new ApplicationViewModel().Map(model));
                return RedirectToAction(nameof(Details), new { createdApplication.Id });
            }

            model.AvailableSystemOwners = await GetAvailableSystemOwners(model.SystemOwnerId);
            model.AvailableVendors = await GetAvailableVendors();
            return View(model);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var application = await _applicationService.GetAsync(id.Value);
            if (application == null)
                return NotFound();

            if (!_securityService.CurrrentUserHasAccessToApplication(application, AccessLevel.Write))
                return Forbid();

            var model = new ApplicationViewModel().Map(application);

            model.AvailableSystemOwners = await GetAvailableSystemOwners(model.SystemOwnerId);
            model.AvailableVendors = await GetAvailableVendors(model.VendorId);
            return View(model);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
            [Bind("Id,Name,Version,VendorId,VendorName,AnnualFee,InitialCost,PurchaseDate,HostingVendor,HostingLocation,NumberOfUsers," +
                  "SystemOwnerId,Sectors,NationalComponents,AgreementDateStart,AgreementDescription,AgreementDocumentUrl," +
                  "AgreementResponsibleRole,AgreementTerminationClauses")] ApplicationViewModel model)
        {
            if (id == 0)
                return NotFound();

            if (id != model.Id)
            {
                return BadRequest();
            }

            Application application = await _applicationService.GetAsync(id);
            if (application == null)
                return NotFound();

            if (!_securityService.CurrrentUserHasAccessToApplication(application, AccessLevel.Write))
                return Forbid();

            model.OrganizationId = CurrentOrganizationId();

            if (ModelState.IsValid)
            {
                await _applicationService.UpdateAsync(id, new ApplicationViewModel().Map(model));
                FlashSuccess(UIResource.FlashApplicationUpdated);
                return RedirectToAction(nameof(Details), new { id });
            }

            model.AvailableSystemOwners = await GetAvailableSystemOwners(model.SystemOwnerId);
            model.AvailableVendors = await GetAvailableVendors(model.VendorId);
            return View(model);
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var applicationToDelete = await _applicationService.GetAsync(id.Value);
            if (applicationToDelete == null)
                return NotFound();

            if (!_securityService.CurrrentUserHasAccessToApplication(applicationToDelete, AccessLevel.Write))
                return Forbid();

            return View(applicationToDelete);
        }

        [HttpPost("delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == 0)
                return NotFound();

            var applicationToDelete = await _applicationService.GetAsync(id);
            if (applicationToDelete == null)
                return NotFound();

            if (!_securityService.CurrrentUserHasAccessToApplication(applicationToDelete, AccessLevel.Write))
                return Forbid();

            await _applicationService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("submit-app-registry/{id}")]
        public async Task<IActionResult> SubmitAppRegistry(int id)
        {
            if (id == 0)
                return NotFound();

            Application application = await _applicationService.GetAsync(id);
            if (application == null)
                return NotFound();

            if (!_securityService.CurrrentUserHasAccessToApplication(application, AccessLevel.Write))
                return Forbid();

            return View(new ApplicationViewModel().Map(application));
        }

        [HttpPost("submit-app-registry/{id}")]
        public async Task<IActionResult> SubmitAppRegistryConfirm(int id)
        {
            if (id == 0)
                return NotFound();

            Application application = await _applicationService.GetAsync(id);
            if (application == null)
                return NotFound();

            if (!_securityService.CurrrentUserHasAccessToApplication(application, AccessLevel.Write))
                return Forbid();

            await _appRegistry.SubmitApplication(id, CurrentOrganizationId(), CurrentUser().Id());

            FlashSuccess(UIResource.FlashApplicationSubmittedToAppRegistry);

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        [Route("/applications/sector/{sectorId}")]
        [Route("/applications/orderByPrice/{sortingOrder}")]
        [Route("/applications/nationalComponents/{nationalComponentId}")]
        public async Task<IActionResult> FilterApplications(int sectorId = 0, int nationalComponentId = 0, string sortingOrder = null)
        {
            var applications = await _applicationService.GetApplicationsWithFilter(CurrentOrganizationId(),
                sectorId, nationalComponentId, sortingOrder);

            return View(new ApplicationViewModel().MapToEnumerable(applications));
        }

     
    }
}
