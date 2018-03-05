﻿using Microsoft.AspNetCore.Http;

namespace Arkitektum.Orden.Utils
{
    public class CookieHelper
    {
        public int? GetCurrentOrganizationId(HttpContext httpContext)
        {
            httpContext.Request.Cookies.TryGetValue(CookieNames.CurrentOrganizationId,
                out var organizationFromCookie);

            int? organizationId = null;

            if (!string.IsNullOrWhiteSpace(organizationFromCookie))
            {
                int.TryParse(organizationFromCookie, out var parsedOrganizationId);
                organizationId = parsedOrganizationId;
            }

            return organizationId;
        }
    }
}