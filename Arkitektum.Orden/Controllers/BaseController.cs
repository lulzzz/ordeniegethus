using System.Threading;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Services;
using Microsoft.AspNetCore.Mvc;

namespace Arkitektum.Orden.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly ISecurityService _securityService;

        public BaseController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        protected ApplicationUser CurrentUser()
        {
            _securityService.GetCurrentUser();

            return null;
        }

    }

}