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
using Microsoft.AspNetCore.Hosting.Internal;
using Arkitektum.Orden.Utils;

namespace Arkitektum.Orden.Controllers
{
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

        // GET: Datasets
        public async Task<IActionResult> Index()
        {
            SimpleOrganization currentOrganization = CurrentOrganization();
            var datasets = await _datasetService.GetAllDatasetsForOrganization(currentOrganization.Id);
            return View(new DatasetViewModel().MapToEnumerable(datasets));
        }

        [HttpGet("/datasets/all")]
        public async Task<IActionResult> All()
        {
            if (!_securityService.CurrrentUserHasAccessToOrganization(CurrentOrganizationId(), AccessLevel.Read))
                return Forbid();

            var datasets = await _datasetService.GetAllDatasetsForOrganization(CurrentOrganizationId());
            return Json(new DatasetApiViewModel().Map(datasets));
        }

        // GET: Datasets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataset = await _datasetService.GetAsync(id.Value);
               
            if (dataset == null)
            {
                return NotFound();
            }

            return View(new DatasetViewModel().Map(dataset));
        }

        // GET: Datasets/Create
        public IActionResult Create()
        {
            SimpleOrganization currentOrganization = CurrentOrganization();

            var model = new DatasetViewModel();
         
            model.OrganizationId = currentOrganization.Id;

            return View(model);

        }

        // POST: Datasets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Purpose,AccessRight,HasPersonalData,HasSensitivePersonalData,HasMasterData,HostingLocation,PublishedToSharedDataCatalog," +
                                                      "Application,OrganizationId")] DatasetViewModel dataset)
        {
            if (ModelState.IsValid)
            {
                dataset.OrganizationId = CurrentOrganizationId();
                await _datasetService.Create(new DatasetViewModel().Map(dataset));
            
                return RedirectToAction(nameof(Index));
            }

            return View(dataset);
        }

        // GET: Datasets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
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

        // POST: Datasets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Purpose,AccessRight,HasPersonalData,HasSensitivePersonalData,HasMasterData," +
                                                            "HostingLocation,PublishedToSharedDataCatalog,Application")] DatasetViewModel dataset)
        {
            if (id != dataset.Id)
            {
                return NotFound();
            }

            dataset.OrganizationId = CurrentOrganizationId();

            await _datasetService.UpdateAsync(id, dataset.Map(dataset));

            return RedirectToAction(nameof(Index));
        }

        // GET: Datasets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var dataset = await _datasetService.GetAsync(id.Value);

            if (dataset == null) return NotFound();
            
            return View(new DatasetViewModel().Map(dataset));
        }

        // POST: Datasets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _datasetService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
