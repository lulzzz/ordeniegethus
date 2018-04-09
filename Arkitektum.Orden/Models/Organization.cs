using System.Collections.Generic;

namespace Arkitektum.Orden.Models
{
    public class Organization
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string OrganizationNumber { get; set; }

        public List<Application> Applications { get; set; }

        public List<OrganizationApplicationUser> Users { get; set; }

        public List<OrganizationAdministrators> OrganizationAdministrators { get; set; }

        /// <summary>
        /// List of users available to be added as super users in an application entry
        /// </summary>
        public List<SuperUser> SuperUsers { get; set; }

        public virtual DcatCatalog DcatCatalog { get; set; }
    }
}