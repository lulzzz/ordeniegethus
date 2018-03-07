using System;
using Arkitektum.Orden.Models.ViewModels;
using Arkitektum.Orden.Services;
using Arkitektum.Orden.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Arkitektum.Orden.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly ISecurityService _securityService;

        protected BaseController(ISecurityService securityService )
        {
            _securityService = securityService;
        }

        protected ICurrentUser CurrentUser()
        {
            return _securityService.GetCurrentUser();
        }

        /// <summary>
        ///     Returns the current organization from the session
        /// </summary>
        /// <returns></returns>
        protected SimpleOrganization CurrentOrganization()
        {
            var currentOrganizationId = new CookieHelper().GetCurrentOrganizationId(HttpContext);
            return _securityService.GetCurrentOrganization(currentOrganizationId);
        }

        protected int CurrentOrganizationId()
        {
            if (CurrentOrganization() == null)
                throw new Exception("Invalid system state - current organization is null");

            return CurrentOrganization().Id;
        }

    }
}