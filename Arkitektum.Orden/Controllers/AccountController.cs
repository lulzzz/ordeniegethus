using System;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.AccountViewModels;
using Arkitektum.Orden.Services;
using Arkitektum.Orden.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;

namespace Arkitektum.Orden.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private static readonly ILogger Log = Serilog.Log.ForContext(MethodBase.GetCurrentMethod().DeclaringType);
        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IUserService _userService;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _userService = userService;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    Log.Information("User {email} logged in.", model.Email);
                    return RedirectToLocal(returnUrl);
                }
                if (result.IsLockedOut)
                {
                    Log.Warning("User account {email} locked out.", model.Email);
                    return RedirectToAction(nameof(Lockout));
                }
                else
                {
                    Log.Debug("Invalid login attempt for {user}", model.Email);
                    ModelState.AddModelError(string.Empty, UIResource.InvalidLoginAttempt);
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangeCurrentOrganization(int organizationId)
        {
            ApplicationUser user = await _userService.Get(_userManager.GetUserId(User));
            
            if (User.IsInRole(Roles.Admin) || user.HasAccessToOrganization(organizationId))
            {
                HttpContext.Response.Cookies.Append(CookieNames.CurrentOrganizationId, organizationId.ToString());
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            Log.Debug("User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }
            Log.Debug("Loading confirm email page for {user}", userId);
            
            var result = await _userManager.ConfirmEmailAsync(user, code);

            string viewName;
            if (result.Succeeded)
            {
                Log.Debug("Confirmation of email succeded.");
                viewName = "ConfirmEmail";
            }
            else
            {
                Log.Debug("Confirmation of email failed: " + result.ToString());
                viewName = "Error";
            }
            
            return View(viewName);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                Log.Debug("Forgot password request for {user}", model.Email);
                
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
                await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                   $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
                
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            if (code == null)
            {
                throw new ApplicationException("A code must be supplied for password reset.");
            }
            var model = new ResetPasswordViewModel { Code = code };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            
            Log.Debug("Resetting password for {user}", model.Email);
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            AddErrors(result);
            return View();
        }

        [HttpPost]
        [Authorize(Roles=Roles.Admin)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImpersonateUser(string id)
        {
            var appUser = await _userManager.FindByIdAsync(id);
            var userPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
            userPrincipal.Identities.First().AddClaim(new Claim("OriginalUserId", User.FindFirst(x=>x.Type == ClaimTypes.NameIdentifier).Value));

            await _signInManager.SignOutAsync(); //sign out the current user

            await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, userPrincipal); //impersonate the new user

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize(Roles=Roles.User)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StopImpersonation()
        {
            var originalUserId = User.Claims.FirstOrDefault(x => x.Type == "OriginalUserId");
            if (originalUserId != null)
            {
                var appUser = await _userManager.FindByIdAsync(originalUserId.Value);
                await _signInManager.SignInAsync(appUser, false);
            }

            return RedirectToAction("Index", "Home");
        }
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }


        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
    }
}
