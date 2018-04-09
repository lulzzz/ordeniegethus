using System.Collections.Generic;
using System.Threading.Tasks;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Services;
using Microsoft.AspNetCore.Mvc;

namespace Arkitektum.Orden.Controllers
{
    public class SuperUsersController : BaseController
    {
        private readonly ISuperUsersService _superUsersService;

        public SuperUsersController(ISuperUsersService superUsersService, ISecurityService securityService) : base(securityService)
        {
            _superUsersService = superUsersService;
        }

        [HttpGet]
        [Route("/SuperUsers/organization/{organizationId}")]
        public async Task<IActionResult> GetSuperUsersForOrganization(int organizationId)
        {
            if (organizationId == 0)
                return NotFound();

            if (CurrentOrganizationId() != organizationId)
                return Forbid();

            List<SuperUser> superUsers = await _superUsersService.GetSuperUsersForOrganization(organizationId);

            return Json(superUsers);
        }

        [HttpPost]
        [Route("/SuperUsers")]
        public async Task<IActionResult> Create(SuperUser superUser)
        {
            if (superUser.OrganizationId == 0)
                return BadRequest();

            if (CurrentOrganizationId() != superUser.OrganizationId)
                return Forbid();

            SuperUser createdSuperUser = await _superUsersService.AddSuperUserToOrganization(superUser);
            return Json(createdSuperUser);
        }

        [HttpPut]
        [Route("/SuperUsers")]
        public async Task<IActionResult>  Edit(SuperUser superUser)
        {
            if (superUser.OrganizationId == 0)
                return BadRequest();

            if (CurrentOrganizationId() != superUser.OrganizationId)
                return Forbid();

            SuperUser updatedUser = await _superUsersService.UpdateSuperUser(superUser);
            return Json(updatedUser);
        }

        [HttpPost]
        [Route("/SuperUsers/Delete")]
        public async Task<IActionResult> Delete(int superUserId)
        {
            if (superUserId == 0)
                return BadRequest();

            SuperUser superUser = await _superUsersService.Get(superUserId);
            
            if (CurrentOrganizationId() != superUser.OrganizationId)
                return Forbid();

            await _superUsersService.DeleteSuperUser(superUserId);

            return Ok();
        }
    }
}