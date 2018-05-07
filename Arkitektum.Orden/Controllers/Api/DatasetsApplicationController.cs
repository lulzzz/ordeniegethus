using Arkitektum.Orden.Models.ViewModels;
using Arkitektum.Orden.Services;
using Arkitektum.Orden.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Arkitektum.Orden.Controllers
{
    [Authorize]
    [Route("/api/datasets/application")]
    public class DatasetsApplicationController : BaseController
    {
        private readonly IApplicationService _applicationService;
        private readonly IApplicationDatasetService _applicationDatasetService;

        public DatasetsApplicationController(IApplicationService applicationService, IApplicationDatasetService applicationDatasetService, ISecurityService securityService) : base(securityService)
        {
            _applicationService = applicationService;
            _applicationDatasetService = applicationDatasetService;
        }

        [HttpGet("{applicationId}")]
        public async Task<IActionResult> GetApplicationDatasets(int applicationId)
        {
            if (applicationId == 0)
                return NotFound();

            var application = await _applicationService.GetAsync(applicationId);
            if (application == null)
                return NotFound();

            if (!_securityService.CurrrentUserHasAccessToApplication(application, AccessLevel.Read))
                return Forbid();

            var datasets = await _applicationDatasetService.GetDatasetsForApplication(applicationId);
            return Json(new DatasetApiViewModel().Map(datasets));
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateApplicationDataset([FromBody] ApplicationDatasetViewModel model)
        {
            if (model.ApplicationId == 0)
                return NotFound();

            var application = await _applicationService.GetAsync(model.ApplicationId);
            if (application == null)
                return NotFound();

            if (!_securityService.CurrrentUserHasAccessToApplication(application, AccessLevel.Write))
                return Forbid();

            await _applicationDatasetService.CreateApplicationDataset(model.ApplicationId, model.DatasetId);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete("{datasetId}/{applicationId}")]
        public async Task<IActionResult> DeleteApplicationDataset(int datasetId, int applicationId)
        {   
            if (applicationId == 0)
                return NotFound();

            var application = await _applicationService.GetAsync(applicationId);
            if (application == null)
                return NotFound();

            if (!_securityService.CurrrentUserHasAccessToApplication(application, AccessLevel.Write))
                return Forbid();

            await _applicationDatasetService.DeleteApplicationDataset(applicationId, datasetId);
            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}
