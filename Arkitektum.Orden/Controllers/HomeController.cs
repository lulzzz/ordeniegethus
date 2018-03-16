using System.Diagnostics;
using System.Threading.Tasks;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.ViewModels;
using Arkitektum.Orden.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Arkitektum.Orden.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly ISectorService _sectorService;
        private readonly IApplicationService _applicationService;
        private readonly IDatasetService _datasetService;

        public HomeController(ISectorService sectorService, IApplicationService applicationService,
            ISecurityService securityService, IDatasetService datasetService) : base(securityService)
        {
            _sectorService = sectorService;
            _applicationService = applicationService;
            _datasetService = datasetService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel();
            model.NumberOfApplications =
                await _applicationService.GetApplicationCountForOrganization(CurrentOrganizationId());
            model.NumberOfDataset = await _datasetService.GetDatasetsCountForOrganization(CurrentOrganizationId());
            model.NumberOfPublishedDataset = 0; // TODO implement numberOfPublishedDataset

            model.Sectors =
                SectorViewModel.MapEnumerable(await _sectorService.GetSectorsForOrganization(CurrentOrganizationId()));
            return View(model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}