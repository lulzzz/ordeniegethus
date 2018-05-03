using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.ViewModels;
using Arkitektum.Orden.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nest;

namespace Arkitektum.Orden.Services
{
    public interface ISecurityService
    {
        CurrentUser GetCurrentUser();
        List<string> GetDelegateableRoles();
        Task<List<Organization>> GetDelegateableOrganizationsAsync();

        SimpleOrganization GetCurrentOrganization(int? organizationId);
        SimpleOrganization GetCurrentOrganization(HttpContext httpContext);
        bool CurrrentUserHasAccessToApplication(Application application, AccessLevel accessLevel);
        bool CurrrentUserHasAccessToOrganization(int organizationId, AccessLevel accessLevel);
        bool CurrrentUserHasAccessToDataset(int datasetId, AccessLevel read, int currentOrganizationId);
    }
    
    public class SecurityService : ISecurityService
    {
        private readonly IPrincipal _principal;
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICookieHelper _cookieHelper;

        public SecurityService(IPrincipal principal, ApplicationDbContext context, IUserService userService, UserManager<ApplicationUser> userManager, ICookieHelper cookieHelper)
        {
            _principal = principal;
            _context = context;
            _userService = userService;
            _userManager = userManager;
            _cookieHelper = cookieHelper;
        }

        /// <summary>
        /// Returns the currently logged in user
        /// </summary>
        /// <returns></returns>
        public CurrentUser GetCurrentUser()
        {
            ApplicationUser user = _userService.Get(_userManager.GetUserId(new ClaimsPrincipal(_principal))).Result;
            return new CurrentUser(_principal, user);
        }

        /// <summary>
        /// Returns a list of roles which the current logged in user has permission to give other users.
        /// </summary>
        /// <returns>list of role names</returns>
        public List<string> GetDelegateableRoles()
        {
            if (_principal.IsInRole(Roles.Admin))
                return new List<string>(Roles.All);

            // TODO: Use organization membership to determine delegatable roles, except for administrator

            return new List<string>();
        }

        public async Task<List<Organization>> GetDelegateableOrganizationsAsync()
        {
            return await _context.Organization.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Get the current organization from cookie, lookup all other available organizations from database.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public SimpleOrganization GetCurrentOrganization(HttpContext httpContext)
        {
            var currentOrganizationId = _cookieHelper.GetCurrentOrganizationId(httpContext);
            return GetCurrentOrganization(currentOrganizationId);
        }

        /// <summary>
        /// Returns the list of organizations available to the user.
        /// Returns null if user does not have access to any organizations
        /// </summary>
        /// <param name="organizationId">The id of the current organization - first item in organization list will be used if null</param>
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

        public bool CurrrentUserHasAccessToApplication(Application application, AccessLevel accessLevel)
        {
            CurrentUser currentUser = GetCurrentUser();
            return currentUser.HasAccessToOrganization(application.OrganizationId.Value, accessLevel);
        }

        public bool CurrrentUserHasAccessToOrganization(int organizationId, AccessLevel accessLevel)
        {
            CurrentUser currentUser = GetCurrentUser();
            return currentUser.HasAccessToOrganization(organizationId, accessLevel);
        }

        public bool CurrrentUserHasAccessToDataset(int datasetId, AccessLevel accessLevel, int currentOrganizationId)
        {
            var hasAccessToOrganization = GetCurrentUser().HasAccessToOrganization(currentOrganizationId, accessLevel);

            return hasAccessToOrganization && DatasetBelongsToOrganization(datasetId, currentOrganizationId);
        }

        private bool DatasetBelongsToOrganization(int datasetId, int organizationId)
        {
            return _context.Dataset.Any(d => d.Id == datasetId && d.OrganizationId == organizationId);
        }
    }

    
}