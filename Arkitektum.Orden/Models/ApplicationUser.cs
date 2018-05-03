using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Arkitektum.Orden.Models
{
    /// <summary>
    ///     A user that can log in to the system
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        public const string AdministratorFullName = "Administrator";
        
        /// <summary>
        /// Full name of the user
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        ///     The organizations this users is a part of.
        /// </summary>
        public virtual List<OrganizationApplicationUser> Organizations { get; set; }

        /// <summary>
        ///     The organizations this user can administrate.
        /// </summary>
        public virtual List<OrganizationAdministrators> OrganizationAdministrators { get; set; }

        public bool HasAccessToOrganization(int organizationId)
        {
            if (Organizations != null)
            {
                foreach (var org in Organizations)
                {
                    if (org.OrganizationId == organizationId)
                        return true;
                }
            }
            return false;
        }

        public bool HasRoleInOrganization(int organizationId, string role)
        {
            foreach (var membership in GetOrganizationMembership(organizationId))
            {
                if (membership.Role == role)
                    return true;
            }
            return false;
        }

        private List<OrganizationApplicationUser> GetOrganizationMembership(int organizationId)
        {
            var memberships = new List<OrganizationApplicationUser>();
            if (Organizations != null)
            {
                foreach (var org in Organizations)
                {
                    if (org.OrganizationId == organizationId)
                        memberships.Add(org);
                }
            }
            return memberships;
        }

        internal bool HasAnyRoleInOrganization(int organizationId, IEnumerable<string> roles)
        {
            foreach (var membership in GetOrganizationMembership(organizationId))
            {
                foreach(var role in roles)
                {
                    if (membership.Role == role)
                        return true;
                }
            }
            return false;
        }
    }
}