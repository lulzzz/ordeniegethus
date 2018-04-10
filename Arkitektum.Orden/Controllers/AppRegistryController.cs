using System.Collections.Generic;
using System.Threading.Tasks;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Services;
using Arkitektum.Orden.Services.AppRegistry;
using Microsoft.AspNetCore.Mvc;

namespace Arkitektum.Orden.Controllers
{
    public class AppRegistryController : Controller
    {
        private IAppRegistry _appRegistry;
        private IApplicationService _applicationService;

        public AppRegistryController(IAppRegistry appRegistry, IApplicationService applicationService)
        {
            _appRegistry = appRegistry;
            _applicationService = applicationService;
        }

        public async Task<IActionResult> Index()
        {
            List<CommonApplication> applications = await _appRegistry.GetApplicationsAsync();

            return View(applications);
        }

        
    }
}