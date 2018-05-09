using System.Collections.Generic;
using System.Threading.Tasks;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.ViewModels;
using Arkitektum.Orden.Services;
using Arkitektum.Orden.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Arkitektum.Orden.Controllers.Api
{
    [Authorize]
    [Route("/api/applications")]
    [Area("Api")]
    public class ApplicationsController : BaseController
    {
        private readonly IApplicationService _applicationService;
        private readonly IApplicationDatasetService _applicationDatasetService;

        public ApplicationsController(IApplicationService applicationService,
            IApplicationDatasetService applicationDatasetService, ISecurityService securityService) : base(
            securityService)
        {
            _applicationService = applicationService;
            _applicationDatasetService = applicationDatasetService;
        }
        
        [HttpGet("all")]
        public async Task<IActionResult> All()
        {
            if (!_securityService.CurrrentUserHasAccessToOrganization(CurrentOrganizationId(), AccessLevel.Read))
                return Forbid();

            var applications = await _applicationService.GetAllApplicationsForOrganization(CurrentOrganizationId());
            return Json(new ApplicationApiViewModel().Map(applications));
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (!_securityService.CurrrentUserHasAccessToOrganization(CurrentOrganizationId(), AccessLevel.Read))
                return Forbid();

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

        [HttpGet("{id}/datasets")]
        public async Task<IActionResult> GetApplicationDatasets(int id)
        {
            if (id == 0)
                return NotFound();

            var application = await _applicationService.GetAsync(id);
            if (application == null)
                return NotFound();

            if (!_securityService.CurrrentUserHasAccessToApplication(application, AccessLevel.Read))
                return Forbid();

            IEnumerable<ApplicationDataset> datasetsForApplication = await _applicationDatasetService.GetDatasetsForApplication(id);
            
            return Json(new DatasetApiViewModel().Map(datasetsForApplication));
        }
    }
}