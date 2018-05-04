using System.Collections.Generic;
using System.Threading.Tasks;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.Api;
using Arkitektum.Orden.Services;
using Arkitektum.Orden.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Arkitektum.Orden.Controllers.Api
{
    [Authorize]
    [Route("/api/applications")]
    public class ApplicationSuperUsersController : BaseController
    {
        private readonly ISuperUsersService _superUsersService;

        public ApplicationSuperUsersController(ISuperUsersService superUsersService, ISecurityService securityService) : base(securityService)
        {
            _superUsersService = superUsersService;
        }

        [HttpGet("{applicationId}/superusers")]
        public async Task<IActionResult> GetSuperUsersForApplication(int applicationId)
        {
            if (!_securityService.CurrrentUserHasAccessToApplication(applicationId, AccessLevel.Read, CurrentOrganizationId()))
                return Forbid();
            
            List<SuperUser> superUsers = await _superUsersService.GetSuperUsersForApplication(applicationId);
            return Json(new SuperUserViewModel().MapToViewModel(superUsers));
        }

        
        [HttpPost("{applicationId}/superusers")]
        public async Task<IActionResult> AddSuperUserToApplication(int applicationId, [FromBody] SuperUserViewModel superUser)
        {
            if (!_securityService.CurrrentUserHasAccessToApplication(applicationId, AccessLevel.Write, CurrentOrganizationId()))
                return Forbid();

            await _superUsersService.AddSuperUserToApplication(applicationId, superUser.Id);
            
            return NoContent();
        }
        
        [HttpDelete("{applicationId}/superusers/{superUserId}")]
        public async Task<IActionResult> RemoveSuperUserFromApplication(int applicationId, int superUserId)
        {
            if (!_securityService.CurrrentUserHasAccessToApplication(applicationId, AccessLevel.Write, CurrentOrganizationId()))
                return Forbid();
            
            await _superUsersService.RemoveSuperUserFromApplication(applicationId, superUserId);
            
            return NoContent();
        }

    }
}