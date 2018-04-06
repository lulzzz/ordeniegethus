using System.Collections.Generic;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class ResourceLinkViewModel : Mapper<ResourceLink, ResourceLinkViewModel>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }

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
                Id = input.Id,
                Description = input.Description,
                Url = input.Url
            };
        }


        public ResourceLink Map(ResourceLinkViewModel resourceLink, int applicationId)
        {
            var model = new ResourceLink
            {
                Id = resourceLink.Id,
                Description = resourceLink.Description,
                Url = resourceLink.Url,
                ApplicationId = applicationId
            };
            return model;
        }

    }
}
