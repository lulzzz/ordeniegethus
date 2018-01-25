using System.Collections.Generic;

namespace Arkitektum.Orden.Models
{
    public class Application
    {
        public int Id { get; set; }

        /// <summary>
        ///     Navn
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Versjon
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        ///     Leverandør
        /// </summary>
        public Vendor Vendor { get; set; }

        /// <summary>
        ///     Støtter disse standardene
        /// </summary>
        public List<Standard> Standards { get; set; }

        /// <summary>
        ///     Støtter disse felleskomponentene (svarUt, Id-port)
        /// </summary>
        public List<SharedService> SuppportedSharedServices { get; set; }

        /// <summary>
        ///     Tilgjengelig integrasjonsmuligheter (API)
        /// </summary>
        public List<Integration> SupportedIntegrations { get; set; }

        /// <summary>
        ///     Årlig kostnad
        /// </summary>
        public decimal AnnnualFee { get; set; }

        /// <summary>
        ///     Innkjøpskostnad
        /// </summary>
        public decimal InitialCost { get; set; }

        /// <summary>
        ///     Systemeier
        /// </summary>
        public ApplicationUser SystemOwner { get; set; }

        /// <summary>
        ///     Superbrukere
        /// </summary>
        public List<ApplicationUser> SuperUsers { get; set; }


        /// <summary>
        ///     Lenker til eksterne ressurser
        /// </summary>
        public List<ResourceLink> ResourceLinks { get; set; }

        /// <summary>
        ///     Driftsplassering
        /// </summary>
        public string HostingLocation { get; set; }

        /// <summary>
        ///     Driftsleverandør
        /// </summary>
        public Vendor HostingVendor { get; set; }

        /// <summary>
        ///     Antall brukere
        /// </summary>
        public int NumberOfUsers { get; set; }


        /// <summary>
        ///     Tjenesteområder
        /// </summary>
        public List<Sector> Sectors { get; set; }

        /// <summary>
        ///     Datasett
        /// </summary>
        public List<Dataset> Datasets { get; set; }

        /// <summary>
        ///     Felleskomponenter
        /// </summary>
        public List<SharedService> SharedServices { get; set; }


        public int OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }
    }
}