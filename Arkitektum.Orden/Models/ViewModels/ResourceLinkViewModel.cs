using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class ResourceLinkViewModel : Mapper<ResourceLink, ResourceLinkViewModel>
    {
        public override IEnumerable<ResourceLinkViewModel> MapToEnumerable(IEnumerable<ResourceLink> inputs)
        {
            return null;
        }

        public override ResourceLinkViewModel Map(ResourceLink input)
        {
            throw new NotImplementedException();
        }

       
    }
}
