using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Microsoft.EntityFrameworkCore;

namespace Arkitektum.Orden.Services
{
    public interface ISecurityService
    {
        CurrentUser GetCurrentUser();
        List<string> GetDelegateableRoles();
        Task<List<Organization>> GetDelegateableOrganizationsAsync();
    }
    
    public class SecurityService : ISecurityService
    {
        private readonly IPrincipal _principal;
        private readonly ApplicationDbContext _context;

        public SecurityService(IPrincipal principal, ApplicationDbContext context)
        {
            _principal = principal;
            _context = context;
        }

        /// <summary>
        /// Returns the currently logged in user
        /// </summary>
        /// <returns></returns>
        public CurrentUser GetCurrentUser()
        {
            return new CurrentUser(_principal);
        }

        /// <summary>
        /// Returns a list of roles which the current logged in user has permission to give other users.
        /// </summary>
        /// <returns>list of role names</returns>
        public List<string> GetDelegateableRoles()
        {
            if (GetCurrentUser().IsInRole(Roles.Admin))
                return new List<string>(Roles.All);

            if (GetCurrentUser().IsInRole(Roles.OrganizationAdmin))
                return new[] {Roles.OrganizationAdmin, Roles.User, Roles.Reader}.ToList();

            return new List<string>();
        }

        public async Task<List<Organization>> GetDelegateableOrganizationsAsync()
        {
            return await _context.Organization.AsNoTracking().ToListAsync();
        }
    }
}