using Arkitektum.Orden.Models.ViewModels;
using Arkitektum.Orden.Services;
using Microsoft.AspNetCore.Mvc;

namespace Arkitektum.Orden.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly ISecurityService _securityService;

        protected BaseController(ISecurityService securityService)
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
            return new SessionHelper().GetCurrentOrganization(HttpContext.Session);
        }

        protected int? CurrentOrganizationId()
        {
            return CurrentOrganization()?.Id;
        }

        /// <summary>
        ///     Set current organization in the session
        /// </summary>
        /// <param name="simpleOrganization"></param>
        protected void SetCurrentOrganization(SimpleOrganization simpleOrganization)
        {
            new SessionHelper().SetCurrentOrganization(HttpContext.Session, simpleOrganization);
        }
    }
}