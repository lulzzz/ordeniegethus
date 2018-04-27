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
        public StandardsController(ISecurityService securityService, ApplicationDbContext context) : base(
            securityService)
        {
        }

        [HttpGet("")]
        public async Task<IActionResult> All()
        {
            List<Standard> standards = await _context.Standards.ToListAsync();
            return Json(new StandardViewModel().Map(standards));
        }

        [HttpPost("application")]
        public async Task<IActionResult> AddStandardToApplication([FromBody] ApplicationStandardViewModel model)
        {
            return NoContent();
        }
        
        [HttpDelete("{standardId}/application/{applicationId}")]
        public async Task<IActionResult> RemoveStandardFromApplication(int standardId, int applicationId)
        {
            return NoContent();
        }
    }
}