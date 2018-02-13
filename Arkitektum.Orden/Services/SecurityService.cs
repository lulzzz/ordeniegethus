using System.Security.Principal;
using System.Threading;
using Arkitektum.Orden.Data;
using Microsoft.AspNetCore.Http;

namespace Arkitektum.Orden.Services
{
    public interface ISecurityService
    {
        void GetCurrentUser();
    }

    public interface ICurrentUser
    {
        
    }

    public class CurrentUser : ICurrentUser
    {
        
    }

    public class SecurityService : ISecurityService
    {
        private readonly IPrincipal _principal;
        private readonly ApplicationDbContext _applicationDbContext;

        public SecurityService(IPrincipal principal, ApplicationDbContext applicationDbContext)
        {
            _principal = principal;
            _applicationDbContext = applicationDbContext;
        }


        public void GetCurrentUser()
        {

        }
    }
}