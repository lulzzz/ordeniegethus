using System.Collections.Generic;
using System.Linq;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class UserViewModel : Mapper<ApplicationUser, UserViewModel>
    {
        public string Id { get; set; }
        public string Email { get; set; }

        public IEnumerable<OrganizationViewModel> Organizations { get; set; }

        public override IEnumerable<UserViewModel> MapToEnumerable(IEnumerable<ApplicationUser> inputs)
        {
            var viewModels = new List<UserViewModel>();
            foreach (var appUser in inputs)
            {
                viewModels.Add(Map(appUser));
            }
            return viewModels;
        }

        public override UserViewModel Map(ApplicationUser applicationUser)
        {
            IEnumerable<OrganizationViewModel> organizations = null;
            if (applicationUser.Organizations != null)
            {
                organizations = new OrganizationViewModel().MapToEnumerable(
                    applicationUser.Organizations.Select(o => o.Organization));
            }

            return new UserViewModel
            {
                Id = applicationUser.Id,
                Email = applicationUser.Email,
                Organizations = organizations
                    
            };
        }
    }
}