using System.Collections.Generic;

namespace Arkitektum.Orden.Models
{
    public class Organization
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string OrganizationNumber { get; set; }

        public List<Application> Applications { get; set; }

        public List<Sector> Sectors { get; set; }

        public List<ApplicationUser> Users { get; set; }
    }
}