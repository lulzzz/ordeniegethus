using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Arkitektum.Orden.Services
{
    public interface ISecurityService
    {
        CurrentUser GetCurrentUser();
        List<string> GetDelegateableRoles();
        Task<List<Organization>> GetDelegateableOrganizationsAsync();

        SimpleOrganization GetCurrentOrganization(int? organizationId);
    }
    
    public class SecurityService : ISecurityService
    {
        private readonly IPrincipal _principal;
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;

        public SecurityService(IPrincipal principal, ApplicationDbContext context, IUserService userService, UserManager<ApplicationUser> userManager)
        {
            _principal = principal;
            _context = context;
            _userService = userService;
            _userManager = userManager;
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

            // TODO: Use organization membership to determine delegatable roles, except for administrator

            return new List<string>();
        }

        public async Task<List<Organization>> GetDelegateableOrganizationsAsync()
        {
            return await _context.Organization.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Returns the users current organization along with other available organizations.
        /// Returns null if user does not have access to any organizations
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        public SimpleOrganization GetCurrentOrganization(int? organizationId)
        {
            ApplicationUser user = _userService.Get(_userManager.GetUserId(new ClaimsPrincipal(_principal))).Result;

            if (user != null)
            {
                if (GetCurrentUser().IsInRole(Roles.Admin))
                    return new SimpleOrganization(_context.Organization.AsNoTracking().ToList(), organizationId);

                if (user.Organizations != null && user.Organizations.Any())
                {
                    return new SimpleOrganization(user.Organizations, organizationId);
                }
            }

            return null;
        }
    }
}