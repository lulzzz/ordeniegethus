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
        [Route("/SuperUsers/organization/{organizationId}")]
        public async Task<IActionResult> Create([FromBody] SuperUser superUser, int organizationId)
        {
            if (organizationId == 0)
                return BadRequest();

            if (CurrentOrganizationId() != organizationId)
                return Forbid();

            superUser.OrganizationId = organizationId;

            SuperUser createdSuperUser = await _superUsersService.AddSuperUserToOrganization(superUser);
            return Json(createdSuperUser);
        }

        [HttpPut]
        [Route("/SuperUsers/organization/{organizationId}/{id}")]
        public async Task<IActionResult> Edit(int organizationId, int id, [FromBody] SuperUser superUser)
        {
            if (organizationId == 0)
                return BadRequest();

            if (id == 0)
                return BadRequest();

            if (CurrentOrganizationId() != organizationId)
                return Forbid();

            superUser.Id = id;
            superUser.OrganizationId = organizationId;

            SuperUser updatedUser = await _superUsersService.UpdateSuperUser(superUser);
            return Json(updatedUser);
        }

        [HttpPost]
        [Route("/SuperUsers/Delete")]
        public async Task<IActionResult> Delete([FromBody] SuperUser superUser)
        {
            if (superUser.Id == 0)
                return BadRequest();

            SuperUser originalSuperUser = await _superUsersService.Get(superUser.Id);
            
            if (CurrentOrganizationId() != originalSuperUser.OrganizationId)
                return Forbid();

            await _superUsersService.DeleteSuperUser(superUser.Id);

            return Ok();
        }
    }
}