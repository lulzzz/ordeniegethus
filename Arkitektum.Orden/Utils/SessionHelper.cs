using System.Reflection;
using Arkitektum.Orden.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Arkitektum.Orden.Controllers
{
    public class SessionHelper
    {
        private const string CurrentOrganizationKey = "CurrentOrganization";
        private static readonly ILogger Log = Serilog.Log.ForContext(MethodBase.GetCurrentMethod().DeclaringType);

        public SimpleOrganization GetCurrentOrganization(ISession session)
        {
            var currentOrganization = session.Get<SimpleOrganization>(CurrentOrganizationKey);
            Log.Debug("Current organization in session: {organizationName}", currentOrganization?.Name);
            return currentOrganization;
        }

        public void SetCurrentOrganization(ISession session, SimpleOrganization organization)
        {
            Log.Debug("Setting current organization in session: {organizationName}", organization.Name);
            session.Set(CurrentOrganizationKey, organization);
        }
    }
}