using System.Collections.Generic;
using System.Linq;

namespace Arkitektum.Orden.Models
{
    /// <summary>
    /// Tjenesteområde, e.g. Arealplanlegging
    /// </summary>
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
        public virtual List<ResourceLink> LawReferences { get; set; } 

        public virtual List<SectorApplication> SectorApplications { get; set; }

        public IEnumerable<Application> ApplicationsAsEnumerable()
        {
            return SectorApplications.Select(sa => sa.Application);
        }

        public Sector()
        {
            LawReferences = new List<ResourceLink>();
            SectorApplications = new List<SectorApplication>();
        }
    }
}