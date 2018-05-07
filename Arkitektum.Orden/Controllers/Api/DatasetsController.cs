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
    [Route("/api/datasets")]
    [Area("Api")]
    public class DatasetsController : BaseController
    {
        private readonly IDatasetService _datasetService;

        public DatasetsController(IDatasetService datasetService, ISecurityService securityService) : base(
            securityService)
        {
            _datasetService = datasetService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> All()
        {
            if (!_securityService.CurrrentUserHasAccessToOrganization(CurrentOrganizationId(), AccessLevel.Read))
                return Forbid();

            var datasets = await _datasetService.GetAllDatasetsForOrganization(CurrentOrganizationId());
            return Json(new DatasetApiViewModel().Map(datasets));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int? id)
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

        [HttpGet("{id}/applications")]
        public async Task<IActionResult> GetApplicationsForDataset(int id)
        {
            if (!_securityService.CurrrentUserHasAccessToOrganization(CurrentOrganizationId(), AccessLevel.Read))
                return Forbid();

            IEnumerable<Application> applicationsForDataset = await _datasetService.GetApplicationsForDataset(id);
            
            return Json(new ApplicationApiViewModel().Map(applicationsForDataset));
        }
    }
}