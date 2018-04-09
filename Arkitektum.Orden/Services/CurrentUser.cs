using System.Security.Principal;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.ViewModels;

namespace Arkitektum.Orden.Services
{
    public interface ICurrentUser
    {
        bool IsInRole(string role);
        IIdentity Identity();
        string FullName();
        string Id();
        bool IsOrganizationAdminForOrganization(SimpleOrganization organization);
    }

    public class CurrentUser : ICurrentUser
    {
        private readonly IPrincipal _principal;
        private readonly ApplicationUser _applicationUser;

        public CurrentUser(IPrincipal principal, ApplicationUser applicationUser)
        {
            _principal = principal;
            _applicationUser = applicationUser;
        }

        public bool IsInRole(string role)
        {
            return _principal.IsInRole(role);
        }

        public IIdentity Identity() => _principal.Identity;

        public string FullName() => _applicationUser.FullName;

        public string Id() => _applicationUser.Id;

        public bool IsOrganizationAdminForOrganization(SimpleOrganization organization)
        {
            if (_applicationUser.HasAccessToOrganization(organization.Id) &&
                _applicationUser.HasRoleInOrganization(organization.Id, Roles.OrganizationAdmin))
                return true;

            return false;
        }
    }
}