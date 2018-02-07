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
        /// Referanse til et hovedtema for datasettet. Et datasett 
        /// kan assosieres med flere tema. Bruk av vokabularet Data 
        /// theme som er publisert av EUs Publication Offices er påkrevd. 
        /// Bruk av Difis Los-vokabular er anbefalt. Også andre nasjonale
        /// og internasjonale vokabular kan brukes om de oppfyller denne
        /// standardens krav til kontrollerte vokabular.
        /// </summary>
        public static List<String> Concepts { get; set; }

        ///<summary>
        /// Referanse til kontaktpunktsobjekt med kontaktopplysninger. 
        /// Disse kan brukes til å sende kommentarer om datasettet.
        /// </summary>
        public static List<ResourceLink> ContactPoints { get; set; }


        /// <summary>
        /// Lenker til eksterne ressurser
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

        ///<summary>
        /// Referanse til en enhet (organisasjon) som er ansvarlig for
        ///  å gjøre datasettet tilgjengelig. Bør være autoritativ URI 
        /// for enhet, sekundært organisasjonsnummer
        /// </summary>
        public Agent Publisher { get; set; }

        /// <summary>
        /// Informasjonselementer i datasettet
        /// </summary>
        public List<Field> Fields { get; set; }


        /// <summary>
        /// Applikasjoner
        /// </summary>
        public List<ApplicationDataset> ApplicationDatasets { get; set; }

        public Category Category { get; set; }

        public List<Distribution> Distributions {get; set; }
        }

    }
}