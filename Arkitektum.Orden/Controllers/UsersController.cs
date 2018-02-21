using System.Threading.Tasks;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.ViewModels;
using Arkitektum.Orden.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Arkitektum.Orden.Controllers
{
    [Authorize(Roles = Roles.Admin + "," + Roles.OrganizationAdmin)]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        [TempData]
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string StatusMessage { get; set; }

        public UsersController(IUserService userService, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _userService = userService;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAll();
            return View(new UserViewModel().MapToEnumerable(users));
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,FullName")] UserViewModel model)
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

/*
        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null) return NotFound();

            var user = await _userService.Get(id);
            if (user == null) return NotFound();

            return View(new UserViewModel().Map(user));
        }
*/
        
        
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