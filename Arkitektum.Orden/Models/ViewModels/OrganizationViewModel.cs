using System.Collections.Generic;
using System.Linq;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class OrganizationViewModel : Mapper<Organization, OrganizationViewModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override IEnumerable<OrganizationViewModel> MapToEnumerable(IEnumerable<Organization> inputs)
        {
            var viewModels = new List<OrganizationViewModel>();
            foreach (var item in inputs)
            {
                viewModels.Add(Map(item));
            }
            return viewModels;
        }

        public override OrganizationViewModel Map(Organization input)
        {
            return new OrganizationViewModel
            {
                Id = input.Id,
                Name = input.Name
            };
        }
    }
}