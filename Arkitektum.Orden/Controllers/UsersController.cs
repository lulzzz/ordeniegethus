using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.ViewModels;
using Arkitektum.Orden.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Arkitektum.Orden.Controllers
{
    [Authorize(Roles = Roles.Admin + "," + Roles.OrganizationAdmin)]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            List<ApplicationUser> users = await _userService.GetUsers();
            var models = new UserViewModel().MapToEnumerable(users);
            return View(models);
        }
    }
}