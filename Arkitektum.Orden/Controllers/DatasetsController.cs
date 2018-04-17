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
        public async Task<IActionResult> Create()
        {
            SimpleOrganization currentOrganization = CurrentOrganization();

            var model = new DatasetViewModel();
         
            model.OrganizationId = currentOrganization.Id;

            model.AvailableApplications = await GetAvailableApplications(currentOrganization.Id);
            

            return View(model);

        }

        private async Task<List<SelectListItem>> GetAvailableApplications(int currentOrganizationId)
        {
            var applicationsForOrganisation = await _applicationService.GetAllApplicationsForOrganization(currentOrganizationId);

            List<SelectListItem> selectListItems = new List<SelectListItem>();

            foreach (var application in applicationsForOrganisation)
            {
                selectListItems.Add(new SelectListItem
                {
                    Text = application.Name,
                    Value = application.Id.ToString()
                });
            }
 
            return selectListItems;
        }


        // POST: Datasets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Purpose,AccessRight,HasPersonalData,HasSensitivePersonalData,HasMasterData,DataLocation,PublishedToSharedDataCatalog," +
                                                      "Application,OrganizationId")] DatasetViewModel dataset)
        {
            if (ModelState.IsValid)
            {
                var createDataset = await _datasetService.Create(new DatasetViewModel().Map(dataset));
                return RedirectToAction(nameof(Index));
            }

            return View();
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
            var applications = await _applicationService.GetAllApplicationsForOrganization(currentOrganization.Id);
            

            if (datasets == null)
            {
                return NotFound();
            }
            return View(new DatasetViewModel().Map(datasets, applications));
        }

        // POST: Datasets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Purpose,AccessRight,HasPersonalData,HasSensitivePersonalData,HasMasterData," +
                                                            "DataLocation,PublishedToSharedDataCatalog,Application")] DatasetViewModel dataset)
        {
            if (id != dataset.Id)
            {
                return NotFound();
            }

            await _datasetService.UpdateAsync(id, dataset.Map(dataset));

            return View(dataset);
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
