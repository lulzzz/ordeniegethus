using System.Collections.Generic;
using System.Threading.Tasks;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.Api;
using Arkitektum.Orden.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Arkitektum.Orden.Controllers.Api
{
    [Authorize]
    [Route("api/standards")]
    public class StandardsController : BaseController
    {
        private readonly IStandardService _standardService;

        public StandardsController(ISecurityService securityService, IStandardService standardService) : base(
            securityService)
        {
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
            await _standardService.AddStandardToApplication(new ApplicationStandard().Map(model));
            
            return NoContent();
        }
        
        [HttpDelete("{standardId}/application/{applicationId}")]
        public async Task<IActionResult> RemoveStandardFromApplication(int standardId, int applicationId)
        {
            await _standardService.RemoveStandardFromApplication(
                new ApplicationStandard() {StandardId = standardId, ApplicationId = applicationId});
            return NoContent();
        }
    }
}