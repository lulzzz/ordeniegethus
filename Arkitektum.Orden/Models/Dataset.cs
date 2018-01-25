using System;
using System.Collections.Generic;

namespace Arkitektum.Orden.Models
{
    public class Dataset
    {
        public int Id { get; set; }

        /// <summary>
        ///     Navn
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Beskrivelse (Modenhetsmodellen)
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Formål
        /// </summary>
        public string Purpose { get; set; }

        /// <summary>
        ///     Personopplysninger
        /// </summary>
        public bool HasPersonalData { get; set; }

        /// <summary>
        ///     Sensitive personopplysninger
        /// </summary>
        public bool HasSensitivePersonalData { get; set; }

        /// <summary>
        ///     Masterdata
        /// </summary>
        public bool HasMasterData { get; set; }

        /// <summary>
        ///     Dataplassering
        /// </summary>
        public string DataLocation { get; set; }


        /// <summary>
        ///     Lenker til eksterne ressurser
        /// </summary>
        public List<ResourceLink> ResourceLinks { get; set; }

        /// <summary>
        ///     Lovhjemmel/forskrift for forvaltning
        /// </summary>
        public List<LawReference> LawReferences { get; set; }

        /// <summary>
        ///     Publisert til felles datakatalog
        /// </summary>
        public DateTime? PublishedToSharedDataCatalog { get; set; }

        /// <summary>
        ///     Informasjonselementer i datasettet
        /// </summary>
        public List<Field> Fields { get; set; }


        /// <summary>
        ///     Applikasjoner
        /// </summary>
        public List<Application> Applications { get; set; }
    }
}