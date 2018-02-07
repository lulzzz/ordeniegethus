﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arkitektum.Orden.Models
{
    public class Organization
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string OrganizationNumber { get; set; }

        public List<Application> Applications { get; set; }

        public List<Sector> Sectors { get; set; }

        public List<OrganizationApplicationUser> Users { get; set; }

        public List<OrganizationAdministrators> OrganizationAdministrators { get; set; }

        public DcatCatalog DcatCatalog { get; set; }
    }
}