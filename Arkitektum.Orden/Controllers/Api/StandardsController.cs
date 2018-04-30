using System.Collections.Generic;
using System.Threading.Tasks;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.Api;
using Arkitektum.Orden.Services;
using Arkitektum.Orden.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Arkitektum.Orden.Controllers.Api
{
    [Authorize]
    [Route("api/standards")]
    public class StandardsController : BaseController
    {
        private readonly IApplicationService _applicationService;
        private readonly IStandardService _standardService;

        public StandardsController(ISecurityService securityService, IApplicationService applicationService, IStandardService standardService) : base(
            securityService)
        {
            _applicationService = applicationService;
            _standardService = standardService;
        }

        [HttpGet("")]
        public async Task<IActionResult> All()
        {
            IEnumerable<Standard> standards = await _standardService.All();
            return Json(new StandardViewModel().Map(standards));
        }

        [HttpPost("application")]
        public async Task<IActionResult> AddStandardToApplication([FromBody] ApplicationStandardViewModel model)
        {
            Application application = await _applicationService.GetAsync(model.ApplicationId);
            if (!_securityService.CurrrentUserHasAccessToApplication(application, AccessLevel.Write))
                return Forbid();
            
            await _standardService.AddStandardToApplication(new ApplicationStandard().Map(model));
            
            return NoContent();
        }
        
        [HttpDelete("{standardId}/application/{applicationId}")]
        public async Task<IActionResult> RemoveStandardFromApplication(int standardId, int applicationId)
        {
            Application application = await _applicationService.GetAsync(applicationId);
            if (!_securityService.CurrrentUserHasAccessToApplication(application, AccessLevel.Write))
                return Forbid();
            
            await _standardService.RemoveStandardFromApplication(
                new ApplicationStandard() {StandardId = standardId, ApplicationId = applicationId});
            return NoContent();
        }
    }
}