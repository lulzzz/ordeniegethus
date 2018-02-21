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
        public IActionResult Create()
        {
            var model = new UserViewModel();
            List<string> delegateableRoles = _securityService.GetDelegateableRoles();
            model.Roles = new List<CheckboxRole>();
            foreach (var role in delegateableRoles)
            {
                model.Roles.Add(new CheckboxRole() { Name = role }); // TODO add localized names
            }
            return View(model);
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,FullName,Roles")] UserViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email, 
                    Email = model.Email, 
                    EmailConfirmed = true,
                    FullName = model.FullName
                };
                
                var result = await _userManager.CreateAsync(user); // Create without password.
                if(result.Succeeded)
                {
                    List<string> delegateableRoles = _securityService.GetDelegateableRoles();

                    foreach (var role in model.Roles)
                    {
                        if (role.Selected && delegateableRoles.Contains(role.Name))
                            await _userManager.AddToRoleAsync(user, role.Name);
                    }
                    await SendActivationMail(user);
                    StatusMessage = UIResource.UserControllerStatusMessageCreatedUser;
                    return RedirectToAction(nameof(Index));
                }
                
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }
            return View(model);
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

            var userViewModel = new UserViewModel().Map(user);
            IList<string> roles = await _userManager.GetRolesAsync(user);
            userViewModel.Roles = new List<CheckboxRole>();
            foreach (var role in roles)
            {
                userViewModel.Roles.Add(new CheckboxRole() {Name = role});
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