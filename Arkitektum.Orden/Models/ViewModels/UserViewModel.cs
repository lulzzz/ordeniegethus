using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class UserViewModel : Mapper<ApplicationUser, UserViewModel>
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        
        public List<CheckboxRole> Roles { get; set; }

        public List<SelectListItem> AllRoles { get; set; }


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
            IEnumerable<OrganizationViewModel> organizations = new List<OrganizationViewModel>();
            if (applicationUser.Organizations != null)
            {
                organizations = new OrganizationViewModel().MapToEnumerable(
                    applicationUser.Organizations.Select(o => o.Organization));
            }

            return new UserViewModel
            {
                Id = applicationUser.Id,
                Email = applicationUser.Email,
                FullName = applicationUser.FullName,
                Organizations = organizations
                    
            };
        }

        public ApplicationUser Map(UserViewModel viewModel)
        {
            return new ApplicationUser()
            {
                Id = viewModel.Id,
                Email = viewModel.Email,
                FullName = viewModel.FullName
            };
        }
    }

    public class CheckboxRole
    {
        public string Name { get; set; }
        public bool Selected { get;set; }
    }
}