using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.ViewModels;
using Arkitektum.Orden.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Arkitektum.Orden.Controllers
{
    [Authorize(Roles = Roles.Admin + "," + Roles.OrganizationAdmin)]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ISecurityService _securityService;

        [TempData]
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string StatusMessage { get; set; }

        public UsersController(IUserService userService, UserManager<ApplicationUser> userManager, IEmailSender emailSender, ISecurityService securityService)
        {
            _userService = userService;
            _userManager = userManager;
            _emailSender = emailSender;
            _securityService = securityService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAll();
            return View(new UserViewModel().MapToEnumerable(users));
        }

        // GET: Users/Create
        public async Task<IActionResult> Create()
        {
            var model = new UserViewModel();
            List<Organization> delegateableOrganizations = await _securityService.GetDelegateableOrganizationsAsync();
            List<string> delegateableRoles = _securityService.GetDelegateableRoles();
            model.OrganizationRoles = new List<CheckboxOrganizationRole>();
            foreach (var organization in delegateableOrganizations)
            {
                foreach (var role in delegateableRoles)
                {
                    model.OrganizationRoles.Add(new CheckboxOrganizationRole()
                    {
                        OrganizationId = organization.Id,
                        OrganizationName = organization.Name,
                        RoleId = role,
                        RoleName = role // TODO add localized names
                    });
                }
            }

            return View(model);
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,FullName,OrganizationRoles")] UserViewModel model)
        {
            if(ModelState.IsValid)
            {
                var hasPermission = await VerifyUserHasPermissionToCreateMemberships(model);
                if (!hasPermission)
                {
                    ModelState.AddModelError("", "User does not have permission to create memberships");
                }
                else
                {
                    ApplicationUser user = CreateApplicationUser(model);

                    var result = await _userManager.CreateAsync(user); // Create without password.
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, Roles.User);

                        List<OrganizationApplicationUser> memberships = CreateOrganizationMemberships(user, model.OrganizationRoles);
                        await _userService.AddOrganizationRolesAsync(memberships);

                        await SendActivationMail(user);

                        StatusMessage = UIResource.UserControllerStatusMessageCreatedUser;
                        
                        return RedirectToAction(nameof(Index));
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }
            }
            return View(model);
        }

        private static ApplicationUser CreateApplicationUser(UserViewModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true,
                FullName = model.FullName
            };
            return user;
        }

        private async Task<bool> VerifyUserHasPermissionToCreateMemberships(UserViewModel model)
        {
            List<Organization> delegateableOrganizations = await _securityService.GetDelegateableOrganizationsAsync();
            List<string> delegateableRoles = _securityService.GetDelegateableRoles();

            List<int> delegatableOrganizationIds = delegateableOrganizations.Select(o => o.Id).ToList();
            foreach (var role in model.OrganizationRoles)
            {
                if (role.Selected)
                {
                    if (!delegateableRoles.Contains(role.RoleId))
                    {
                        
                        return false;
                    }

                    if (!delegatableOrganizationIds.Contains(role.OrganizationId))
                    {
                        return false;
                    }
                }
            }

            

            return true;
        }

        private static List<OrganizationApplicationUser> CreateOrganizationMemberships(ApplicationUser user, List<CheckboxOrganizationRole> model)
        {
            List<OrganizationApplicationUser> organizationMembership = new List<OrganizationApplicationUser>();

            foreach (var item in model)
            {
                if (item.Selected)
                {
                    organizationMembership.Add(new OrganizationApplicationUser()
                    {
                        OrganizationId = item.OrganizationId,
                        ApplicationUser = user,
                        Role = item.RoleId
                    });
                }
            }

            return organizationMembership;
        }


        private async Task SendActivationMail(ApplicationUser user)
        {
            string code = await _userManager.GeneratePasswordResetTokenAsync(user);
 
            // Using protocol param will force creation of an absolute url.
            var callbackUrl = Url.Action("ResetPassword", "Account", 
                new { userId = user.Id, code}, protocol: Request.Scheme);

            await _emailSender.SendEmailConfirmationAsync(user.Email, callbackUrl);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null) return NotFound();

            var user = await _userService.Get(id);
            if (user == null) return NotFound();

            IList<string> userSystemRoles = await _userManager.GetRolesAsync(user);

            var userViewModel = new UserViewModel().Map(user);
            userViewModel.SystemRoles.AddRange(userSystemRoles);
            userViewModel.OrganizationRoles = new List<CheckboxOrganizationRole>();
            foreach (var organizationMembership in user.Organizations)
            {
                userViewModel.OrganizationRoles.Add(new CheckboxOrganizationRole()
                {
                    RoleName = organizationMembership.Role, 
                    OrganizationName = organizationMembership.Organization.Name,
                });
            }
            return View(userViewModel);
        }
        
        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();

            ApplicationUser user = await _userService.Get(id);
            if (user == null) return NotFound();

            return View(new UserViewModel().Map(user));
        }

        // POST: Users/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (id == null) return NotFound();

            ApplicationUser user = await _userService.Get(id);
            if (user == null) return NotFound();

            await _userManager.DeleteAsync(user);

            return RedirectToAction(nameof(Index));
        }
    }
}