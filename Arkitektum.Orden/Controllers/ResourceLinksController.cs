using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.ViewModels;
using Arkitektum.Orden.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arkitektum.Orden.Controllers
{

    // TODO: add security checks

    public class ResourceLinksController : BaseController
    {

        private readonly IResourceLinkService _resourceLinkService;

        public ResourceLinksController(IResourceLinkService resourceLinkService, ISecurityService securityService) : base(securityService)
        {
            _resourceLinkService = resourceLinkService;
        }

        // GET: ResourceLinks for application
        [HttpGet]
        [Route("/ResourceLinks/Application/{applicationId}")]
        public async Task<IActionResult> GetApplicationLinks(int applicationId)
        {
            IEnumerable<ResourceLink> resourceLinks = await _resourceLinkService.GetResourceLinksForApplication(applicationId);

            IEnumerable<ResourceLinkViewModel> viewModels = new ResourceLinkViewModel().MapToEnumerable(resourceLinks);

            return Json(viewModels);
        }

        // POST: ResourceLinks for application
        [HttpPost]
        [Route("/ResourceLinks/Application/{applicationId}")]
        public async Task<IActionResult> CreateApplicationLink([FromBody] ResourceLinkViewModel resourceLink, int applicationId)
        {
            try
            {
                var model = new ResourceLinkViewModel().Map(resourceLink, applicationId);
                var createdResourceLink = await _resourceLinkService.Create(model);
                return Json(new ResourceLinkViewModel().Map(createdResourceLink));
            } 
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }

        // PUT: ResourceLinks/Application/5/10
        [HttpPut]
        [Route("/ResourceLinks/Application/{applicationId}/{id}")]
        public async Task<ActionResult> EditApplicationLink(int applicationId, int id, [FromBody] ResourceLinkViewModel resourceLink)
        {
            try
            {
                var model = resourceLink.Map(resourceLink, applicationId);
                model.Id = id;

                await _resourceLinkService.UpdateAsync(model);

                return Json(new ResourceLinkViewModel().Map(model));
            } 
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}