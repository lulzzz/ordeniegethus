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

        [HttpGet]
        [Route("/insights/nationalComponentsUsage")]
        public async Task<IActionResult> NationalComponentsUsage()
        {
            var nationalComponentsWithApplicationsView = await _applicationService.GetApplicationsGroupedByNationalComponents(CurrentOrganizationId());

            return View(nationalComponentsWithApplicationsView);
        }
    }
}