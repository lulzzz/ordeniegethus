using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Arkitektum.Orden.Models
{
    /// <summary>
    ///     A user that can log in to the system
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        ///     Personal details
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        ///     The organizations this users is a part of.
        /// </summary>
        public List<OrganizationApplicationUser> Organizations { get; set; }

        /// <summary>
        ///     The organizations this user can administrate.
        /// </summary>
        public List<OrganizationAdministrators> OrganizationAdministrators { get; set; }
    }
}