using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class OrganizationViewModel : Mapper<Organization, OrganizationViewModel>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string OrganizationNumber { get; set; }

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
                Name = input.Name,
                OrganizationNumber = input.OrganizationNumber
            };
        }

        public Organization Map(OrganizationViewModel input)
        {
            return new Organization()
            {
                Id = input.Id,
                Name = input.Name,
                OrganizationNumber = input.OrganizationNumber
            };
        }
    }
}