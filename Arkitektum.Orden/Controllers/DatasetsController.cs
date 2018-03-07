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
    public class DatasetsController : BaseController
    {
        private readonly IApplicationService _applicationService;
        private readonly IDatasetService _dataService;
        private ApplicationDbContext _context;


        public DatasetsController(ISecurityService securityService, IApplicationService applicationService, IDatasetService dataService) : base(securityService)
        {
            _applicationService = applicationService;
            _dataService = dataService;
        }

        // GET: Datasets
        public async Task<IActionResult> Index()
        {
            SimpleOrganization currentOrganization = CurrentOrganization();
            var datasets = await _dataService.GetAllDatasetsForOrganization(currentOrganization.Id);
            return View(new DatasetViewModel().MapToEnumerable(datasets));
        }

        // GET: Datasets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataset = await _context.Dataset
                .SingleOrDefaultAsync(m => m.Id == id);
            if (dataset == null)
            {
                return NotFound();
            }

            return View(dataset);
        }

        // GET: Datasets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Datasets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Purpose,AccessRight,HasPersonalData,HasSensitivePersonalData,HasMasterData,DataLocation,PublishedToSharedDataCatalog")] Dataset dataset)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dataset);
                await _context.SaveChangesAsync();
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

            var dataset = await _context.Dataset.SingleOrDefaultAsync(m => m.Id == id);
            if (dataset == null)
            {
                return NotFound();
            }
            return View(dataset);
        }

        // POST: Datasets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Purpose,AccessRight,HasPersonalData,HasSensitivePersonalData,HasMasterData,DataLocation,PublishedToSharedDataCatalog")] Dataset dataset)
        {
            if (id != dataset.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dataset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DatasetExists(dataset.Id))
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
            return View(dataset);
        }

        // GET: Datasets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataset = await _context.Dataset
                .SingleOrDefaultAsync(m => m.Id == id);
            if (dataset == null)
            {
                return NotFound();
            }

            return View(dataset);
        }

        // POST: Datasets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dataset = await _context.Dataset.SingleOrDefaultAsync(m => m.Id == id);
            _context.Dataset.Remove(dataset);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DatasetExists(int id)
        {
            return _context.Dataset.Any(e => e.Id == id);
        }
    }
}
