using System.Collections.Generic;
using System.Threading.Tasks;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.ViewModels;
using Arkitektum.Orden.Services;
using Arkitektum.Orden.Services.Insights;
using Arkitektum.Orden.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Arkitektum.Orden.Controllers
{
    [Route("insights")]
    [Authorize]
    public class InsightsController : BaseController
    {
        private readonly IApplicationService _applicationService;
        private readonly ISectorService _sectorService;
        private readonly IDatasetInsightsService _datasetInsightsService;

        public InsightsController(IApplicationService applicationService, ISectorService sectorService, IDatasetInsightsService datasetInsightsService, ISecurityService securityService) : base(securityService)
        {
            _applicationService = applicationService;
            _sectorService = sectorService;
            _datasetInsightsService = datasetInsightsService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            if (!_securityService.CurrrentUserHasAccessToOrganization(CurrentOrganizationId(), AccessLevel.Read))
                return Forbid();

            var model = new InsightsViewModel();
           
            model.Applications = await _applicationService.GetAllApplicationsForOrganization(CurrentOrganizationId());
            model.Sectors = await _sectorService.GetAll();

            return View(model);

        }

        [HttpGet("dataset-privacy")]
        public async Task<IActionResult> DatasetPrivacy()
        {
            if (!_securityService.CurrrentUserHasAccessToOrganization(CurrentOrganizationId(), AccessLevel.Read))
                return Forbid();

            List<Dataset> datasets = await _datasetInsightsService.DatasetsWithPrivacyConcerns(CurrentOrganizationId());

            return View(new DatasetViewModel().Map(datasets));
        }

        [HttpGet]
        [Route("national-components-usage")]
        public IActionResult NationalComponentsUsage()
        {
            if (!_securityService.CurrrentUserHasAccessToOrganization(CurrentOrganizationId(), AccessLevel.Read))
                return Forbid();

            var nationalComponentsWithApplicationsView = _applicationService.GetApplicationsGroupedByNationalComponents(CurrentOrganizationId());

            return View(nationalComponentsWithApplicationsView);
        }

        [HttpGet]
        [Route("application-costs")]
        public async Task<IActionResult> ApplicationCosts()
        {
            if (!_securityService.CurrrentUserHasAccessToOrganization(CurrentOrganizationId(), AccessLevel.Read))
                return Forbid();

            var applications = await _applicationService.GetAllApplicationsForOrganization(CurrentOrganizationId());

            return View(new ApplicationCostViewModel().MapToEnumerable(applications));
        }

        [HttpGet]
        [Route("datasets-published-overview")]
        public async Task<IActionResult> DatasetsPublishedStatusOverview()
        {
            if (!_securityService.CurrrentUserHasAccessToOrganization(CurrentOrganizationId(), AccessLevel.Read))
                return Forbid();

            var model = await _datasetInsightsService.GetDatasetWithPublishingStatus(CurrentOrganizationId());

            return View(model);
        }


    }
}