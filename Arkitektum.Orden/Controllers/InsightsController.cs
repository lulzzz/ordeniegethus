using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arkitektum.Orden.Models.ViewModels;
using Arkitektum.Orden.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arkitektum.Orden.Controllers
{
    public class InsightsController : BaseController
    {
        private readonly IApplicationService _applicationService;
        private readonly ISectorService _sectorService;

        public InsightsController(IApplicationService applicationService, ISectorService sectorService, ISecurityService securityService) : base(securityService)
        {
            _applicationService = applicationService;
            _sectorService = sectorService;
        }

        // GET: Insights
        public async Task<IActionResult> Index()
        {
            var model = new InsightsViewModel();
           
            model.Applications = await _applicationService.GetAllApplicationsForOrganization(CurrentOrganizationId());
            model.Sectors = await _sectorService.GetAll();

            return View(model);

        }

        // GET: Insights/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Insights/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Insights/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Insights/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Insights/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Insights/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Insights/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}