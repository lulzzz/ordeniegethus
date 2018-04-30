using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models.ViewModels;
using Arkitektum.Orden.Services;
using Arkitektum.Orden.Utils;
using Microsoft.AspNetCore.Authorization;

namespace Arkitektum.Orden.Controllers
{
    [Authorize]
    [Route("/datasets")]
    public class DatasetsController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IDatasetService _datasetService;
        private readonly IApplicationService _applicationService;
        private ApplicationDbContext _context;


        public DatasetsController(ISecurityService securityService, IUserService userService, IDatasetService datasetService, IApplicationService applicationService, ApplicationDbContext context) : base(securityService)
        {
            _userService = userService;
            _datasetService = datasetService;
            _applicationService = applicationService;
            _context = context;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            if (!_securityService.CurrrentUserHasAccessToOrganization(CurrentOrganizationId(), AccessLevel.Read))
                return Forbid();
            
            SimpleOrganization currentOrganization = CurrentOrganization();
            var datasets = await _datasetService.GetAllDatasetsForOrganization(currentOrganization.Id);
            return View(new DatasetViewModel().MapToEnumerable(datasets));
        }

        [HttpGet("all")]
        public async Task<IActionResult> All()
        {
            if (!_securityService.CurrrentUserHasAccessToOrganization(CurrentOrganizationId(), AccessLevel.Read))
                return Forbid();

            var datasets = await _datasetService.GetAllDatasetsForOrganization(CurrentOrganizationId());
            return Json(new DatasetApiViewModel().Map(datasets));
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (!_securityService.CurrrentUserHasAccessToOrganization(CurrentOrganizationId(), AccessLevel.Read))
                return Forbid();
            
            if (id == null)
                return NotFound();

            var dataset = await _datasetService.GetAsync(id.Value);
               
            if (dataset == null)
                return NotFound();

            return View(new DatasetViewModel().Map(dataset));
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> DetailsJson(int? id)
        {
            if (!_securityService.CurrrentUserHasAccessToOrganization(CurrentOrganizationId(), AccessLevel.Read))
                return Forbid();

            if (id == null)
                return NotFound();

            var dataset = await _datasetService.GetAsync(id.Value);
            if (dataset == null)
                return NotFound();

            return Json(new DatasetViewModel().Map(dataset));
        }
        
        [HttpGet("create")]
        public IActionResult Create()
        {
            if (!_securityService.CurrrentUserHasAccessToOrganization(CurrentOrganizationId(), AccessLevel.Write))
                return Forbid();
            
            SimpleOrganization currentOrganization = CurrentOrganization();

            var model = new DatasetViewModel();
         
            model.OrganizationId = currentOrganization.Id;

            return View(model);

        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Purpose,AccessRight,HasPersonalData,HasSensitivePersonalData,HasMasterData,HostingLocation,PublishedToSharedDataCatalog," +
                                                      "Application,OrganizationId")] DatasetViewModel dataset)
        {
            if (!_securityService.CurrrentUserHasAccessToOrganization(CurrentOrganizationId(), AccessLevel.Write))
                return Forbid();
            
            if (ModelState.IsValid)
            {
                dataset.OrganizationId = CurrentOrganizationId();
                await _datasetService.Create(new DatasetViewModel().Map(dataset));
            
                return RedirectToAction(nameof(Index));
            }

            return View(dataset);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!_securityService.CurrrentUserHasAccessToOrganization(CurrentOrganizationId(), AccessLevel.Write))
                return Forbid();
            
            if (id == null)
            {
                return NotFound();
            }

            var datasets = await _datasetService.GetAsync(id.Value);
    
            SimpleOrganization currentOrganization = CurrentOrganization();

            if (datasets == null)
            {
                return NotFound();
            }
            return View(new DatasetViewModel().Map(datasets));
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Purpose,AccessRight,HasPersonalData,HasSensitivePersonalData,HasMasterData," +
                                                            "HostingLocation,PublishedToSharedDataCatalog,Application")] DatasetViewModel dataset)
        {
            if (!_securityService.CurrrentUserHasAccessToOrganization(CurrentOrganizationId(), AccessLevel.Write))
                return Forbid();
            
            if (id != dataset.Id)
            {
                return NotFound();
            }

            dataset.OrganizationId = CurrentOrganizationId();

            await _datasetService.UpdateAsync(id, dataset.Map(dataset));

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!_securityService.CurrrentUserHasAccessToOrganization(CurrentOrganizationId(), AccessLevel.Write))
                return Forbid();
            
            if (id == null) return NotFound();

            var dataset = await _datasetService.GetAsync(id.Value);

            if (dataset == null) return NotFound();
            
            return View(new DatasetViewModel().Map(dataset));
        }

        [HttpPost("delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!_securityService.CurrrentUserHasAccessToOrganization(CurrentOrganizationId(), AccessLevel.Write))
                return Forbid();
            
            await _datasetService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        

    }
}
