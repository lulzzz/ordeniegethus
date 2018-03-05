using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arkitektum.Orden.Models.ViewModels;
using Arkitektum.Orden.Utils;
using Microsoft.AspNetCore.Http;

namespace Arkitektum.Orden.Services
{
    public interface ISecurityHelper
    {
        SimpleOrganization GetCurrentOrganization();
    }

    public class SecurityHelper : ISecurityHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISecurityService _securityService;

        public SecurityHelper(IHttpContextAccessor httpContextAccessor, ISecurityService securityService)
        {
            _httpContextAccessor = httpContextAccessor;
            _securityService = securityService;
        }

        public SimpleOrganization GetCurrentOrganization()
        {
            var currentOrganizationId = new CookieHelper().GetCurrentOrganizationId(_httpContextAccessor.HttpContext);
            return _securityService.GetCurrentOrganization(currentOrganizationId);
        }
    }
}
