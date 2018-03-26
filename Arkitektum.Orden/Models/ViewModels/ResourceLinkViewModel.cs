using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class ResourceLinkViewModel : Mapper<ResourceLink, ResourceLinkViewModel>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public int ApplicationId { get; set; }



        public override IEnumerable<ResourceLinkViewModel> MapToEnumerable(IEnumerable<ResourceLink> inputs)
        {
            var viewModels = new List<ResourceLinkViewModel>();
            if (inputs != null)
            {
                foreach (var item in inputs)
                {
                    viewModels.Add(Map(item));
                }
            }

            return viewModels;
        }

        public override ResourceLinkViewModel Map(ResourceLink input)
        {
            return new ResourceLinkViewModel
            {
                Description = input.Description,
                Url = input.Url
            };
        }


        public ResourceLink Map(ResourceLink resourceLink, int applicationId)
        {
            return new ResourceLink
            {
                Description = resourceLink.Description,
                Url = resourceLink.Url,
                ApplicationId = applicationId
            };
        }

        public ResourceLink Map(ResourceLinkViewModel resourceLink)
        {
            return new ResourceLink
            {
                Id = resourceLink.Id,
                Description = resourceLink.Description,
                Url = resourceLink.Url
            };
        }
    }
}
