using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.ViewModels;
using Arkitektum.Orden.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Arkitektum.Orden.Controllers
{
    [Authorize]
    [Route("/nationalcomponents/application")]
    public class NationalComponentApplicationController : BaseController
    {
        private readonly INationalComponentService _nationalComponentService;

        public NationalComponentApplicationController(INationalComponentService nationalComponentService, ISecurityService securityService) : base(securityService) 
        {
            _nationalComponentService = nationalComponentService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComponentsForApplication(int id)
        {
            List<NationalComponent> components = await _nationalComponentService.GetComponentsForApplication(id);
            return Json(new NationalComponentApiViewModel().Map(components));
        }

        [HttpPost("")]
        public async Task<IActionResult> AddComponentToApplication([FromBody] ApplicationNationalComponentViewModel model)
        {
            await _nationalComponentService.AddComponentToApplication(model.NationalComponentId, model.ApplicationId);
            return StatusCode(HttpStatusCode.Created);
        }

        [HttpDelete("{nationalComponentId}/{applicationId}")]
        public async Task<IActionResult> RemoveComponentFromApplication(int nationalComponentId, int applicationId)
        {
            await _nationalComponentService.RemoveComponentFromApplication(nationalComponentId, applicationId);
            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}