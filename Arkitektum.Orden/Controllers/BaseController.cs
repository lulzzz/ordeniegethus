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

        protected BaseController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        protected ICurrentUser CurrentUser()
        {
            return _securityService.GetCurrentUser();
        }

        /// <summary>
        ///     Returns the current organization from cookie
        /// </summary>
        /// <returns></returns>
        protected SimpleOrganization CurrentOrganization()
        {
            return _securityService.GetCurrentOrganization(HttpContext);
        }

        protected int CurrentOrganizationId()
        {
            if (CurrentOrganization() == null)
                throw new Exception("Invalid system state - current organization is null");

            return CurrentOrganization().Id;
        }

        private void SetFlash(FlashMessageType type, string text)
        {
            TempData["FlashMessage.Type"] = type;
            TempData["FlashMessage.Text"] = text;
        }

        /// <summary>
        /// Sets a successful flash message available for the view through TempData
        /// </summary>
        /// <param name="text"></param>
        protected void FlashSuccess(string text)
        {
            SetFlash(FlashMessageType.Success, text);
        }

        /// <summary>
        /// Sets an error flash message available for the view through TempData
        /// </summary>
        /// <param name="text"></param>
        protected void FlashError(string text)
        {
            SetFlash(FlashMessageType.Error, text);
        }
    }
}