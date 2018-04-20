using System.Collections.Generic;
using System.Linq;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class UserViewModel : Mapper<ApplicationUser, UserViewModel>
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public List<CheckboxOrganizationRole> OrganizationRoles { get; set; }
        public IEnumerable<OrganizationViewModel> Organizations { get; set; }
        public List<string> SystemRoles { get; set; }

        public UserViewModel()
        {
            OrganizationRoles = new List<CheckboxOrganizationRole>();
            Organizations = new List<OrganizationViewModel>();
            SystemRoles = new List<string>();
        }

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

    public class CheckboxOrganizationRole
    {
        public string OrganizationName { get; set; }
        public int OrganizationId { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Selected { get; set; }
    }
}