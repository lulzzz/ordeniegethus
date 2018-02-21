using System.Security.Principal;

namespace Arkitektum.Orden.Services
{
    public interface ICurrentUser
    {
        bool IsInRole(string role);
    }

    public class CurrentUser : ICurrentUser
    {
        private readonly IPrincipal _principal;

        public CurrentUser(IPrincipal principal)
        {
            _principal = principal;
        }

        public bool IsInRole(string role)
        {
            return _principal.IsInRole(role);
        }
    }
}