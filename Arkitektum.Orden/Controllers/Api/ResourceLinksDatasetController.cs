using System.Collections.Generic;
using System.Threading.Tasks;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.ViewModels;
using Arkitektum.Orden.Services;
using Arkitektum.Orden.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Arkitektum.Orden.Controllers.Api
{
    [Route("/api/resourcelinks/dataset")]
    public class ResourceLinksDatasetController : BaseController
    {
        private readonly IResourceLinkService _resourceLinkService;

        public ResourceLinksDatasetController(IResourceLinkService resourceLinkService, ISecurityService securityService) : base(securityService)
        {
            _resourceLinkService = resourceLinkService;
        }

        [HttpGet("{datasetId}")]
        public async Task<IActionResult> GetDatasetLinks(int datasetId)
        {
            if (!_securityService.CurrrentUserHasAccessToDataset(datasetId, AccessLevel.Read, CurrentOrganizationId()))
                return Forbid();
            
            IEnumerable<ResourceLink> resourceLinks = await _resourceLinkService.GetResourceLinksForDataset(datasetId);
            IEnumerable<ResourceLinkViewModel> viewModels = new ResourceLinkViewModel().MapToEnumerable(resourceLinks);
            return Json(viewModels);
        }

        [HttpPost("{datasetId}")]
        public async Task<IActionResult> CreateDatasetLink([FromBody] ResourceLinkViewModel resourceLink, int datasetId)
        {
            if (!_securityService.CurrrentUserHasAccessToDataset(datasetId, AccessLevel.Write, CurrentOrganizationId()))
                return Forbid();
            
            var model = new ResourceLinkViewModel().Map(resourceLink);
            model.DatasetResourceLinkId = datasetId;
            var createdResourceLink = await _resourceLinkService.Create(model);
            return Json(new ResourceLinkViewModel().Map(createdResourceLink));
        }

        [HttpPut("{datasetId}/{id}")]
        public async Task<ActionResult> EditDatasetLink(int datasetId, int id, [FromBody] ResourceLinkViewModel resourceLink)
        {
            if (!_securityService.CurrrentUserHasAccessToDataset(datasetId, AccessLevel.Write, CurrentOrganizationId()))
                return Forbid();
            
            var model = resourceLink.Map(resourceLink);
            model.Id = id;
            model.DatasetResourceLinkId = datasetId;

            var updatedResourceLink = await _resourceLinkService.UpdateAsync(model);

            return Json(new ResourceLinkViewModel().Map(updatedResourceLink));
        }
    }
}