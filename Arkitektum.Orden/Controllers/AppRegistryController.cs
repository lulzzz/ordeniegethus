using System.Collections.Generic;
using System.Threading.Tasks;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.AppRegistry;
using Arkitektum.Orden.Services;
using Arkitektum.Orden.Services.AppRegistry;
using Arkitektum.Orden.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal;

namespace Arkitektum.Orden.Controllers
{
    [Route("appregistry")]
    [Authorize]
    public class AppRegistryController : BaseController
    {
        private readonly IAppRegistry _appRegistry;

        public AppRegistryController(IAppRegistry appRegistry, ISecurityService securityService) : base(securityService)
        {
            _appRegistry = appRegistry;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            List<CommonApplication> applications = await _appRegistry.GetApplicationsAsync();

            return View(applications);
        }

        [HttpGet("all")]
        public async Task<IActionResult> All()
        {
            var applications = await _appRegistry.GetApplicationsAsync();

            return Json(applications);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateApplicationForOrganization([FromBody] CreateOrganizationApplicationViewModel model)
        {
            if (!_securityService.CurrrentUserHasAccessToOrganization(CurrentOrganizationId(), AccessLevel.Write))
                return Forbid();

            Application application = await _appRegistry.CreateApplicationForOrganization(model.CommonApplicationId, model.VersionNumber, CurrentOrganizationId());

            var applicationDetailsUrl = Url.Action("Details", "Applications", new {id = application.Id});
            
            return Json(new {location = applicationDetailsUrl });
        }
    }
}