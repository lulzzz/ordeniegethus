using System.Security.Principal;
using Arkitektum.Orden.Models;

namespace Arkitektum.Orden.Services
{
    public interface ICurrentUser
    {
        bool IsInRole(string role);
        IIdentity Identity();
        string FullName();
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
    }
}