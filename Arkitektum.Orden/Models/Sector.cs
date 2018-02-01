using System.Collections.Generic;

namespace Arkitektum.Orden.Models
{
    public class Sector
    {
        public int Id { get; set; }

        /// <summary>
        ///     Navn
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Lovpålagt tjeneste, referanse til hjemmel/forskrift
        /// </summary>
        public List<LawReference> LawReferences { get; set; } 

        public int OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }

        public List<SectorApplication> SectorApplications { get; set; }
    }
}