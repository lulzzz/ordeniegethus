using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
        /// Beskrivelse (Modenhetsmodellen)
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Formål
        /// </summary>
        public string Purpose { get; set; }

        ///<summary>
        /// Inneholder emneord (eller tag) som beskriver datasettet
        /// </summary>
        public List<Keyword> Keywords { get; set; }

        /// <summary>
        /// Hovedidentifikator for datasettet, for eksempel en URI 
        /// eller annen identifikator som er stabil og globalt unik
        /// </summary>
        public List<Identifier> Identifiers { get; set; }

        /// <summary>
        /// Dette feltet angir i hvilken grad datasettet kan bli gjort
        /// tilgjengelig for allmennheten, uten hensyn til om det er
        /// publisert eller ikke. Et kontrollert vokabular med tre
        /// verdier (:public, :restricted og :non-public) vil bli 
        /// opprettet og forvaltet av EUs Publication Office. Ved bruk
        /// av verdiene ":restricted" og ":non-public" er egenskapen 
        /// skjermingshjemmel anbefalt.
        /// </summary>
        public AccessRight AccessRight { get; set; }

        /// <summary>
        /// Referanse til hjemmel (kilde for påstand) i offentlighetsloven,
        ///  sikkerhetsloven, beskyttelsesinstruksen eller annet lovverk
        ///  som ligger til grunn for vurdering av tilgangsnivå. Egenskapen
        ///  er anbefalt dersom «tilgangsnivå» har verdiene «restricted»
        ///  eller «non-public»
        /// </summary>
        public List<AccessRightComment> AccessRightComments { get; set; }

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
        public List<DcatConcept> Concepts { get; set; }

        ///<summary>
        /// Referanse til kontaktpunktsobjekt med kontaktopplysninger. 
        /// Disse kan brukes til å sende kommentarer om datasettet.
        /// </summary>
        public List<ResourceLink> ContactPoints { get; set; }


        /// <summary>
        /// Lenker til eksterne ressurser
        /// </summary>
        public List<ResourceLink> ResourceLinks { get; set; }


        /// <summary>
        ///     Lovhjemmel/forskrift for forvaltning
        /// </summary>
        public List<ResourceLink> LawReferences { get; set; }

        /// <summary>
        ///     Publisert til felles datakatalog
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PublishedToSharedDataCatalog { get; set; }


        /// <summary>
        /// Informasjonselementer i datasettet
        /// </summary>
        public List<Field> Fields { get; set; }


        /// <summary>
        /// Applikasjoner
        /// </summary>
        public List<ApplicationDataset> ApplicationDatasets { get; set; }


        /// <summary>
        /// En katalog eller repository som inneholder beskrivelsene 
        /// av datasettene som er beskrevet.
        /// </summary>

        public DcatCatalog DcatCatalog { get; set; }

        /// <summary>
        /// Koblingen mellom datasettet og en tilgjengelig distribusjon
        /// </summary>
        public List<Distribution> Distributions { get; set; }

        public void UpdateApplicationRelation(List<ApplicationDataset> updateDatasetApplicationDatasets)
        {
            var updatedApplicationIds = updateDatasetApplicationDatasets.Select(udad => udad.ApplicationId).ToList();

            List<ApplicationDataset> updatedListOfApplications = new List<ApplicationDataset>();

            foreach (var application in ApplicationDatasets)
            {
                if (updatedApplicationIds.Contains(application.ApplicationId))
                {
                    updatedListOfApplications.Add(application);
                    updateDatasetApplicationDatasets.RemoveAll(da => da.ApplicationId == application.ApplicationId);
                }

            }

            updatedListOfApplications.AddRange(updateDatasetApplicationDatasets);

            ApplicationDatasets = updatedListOfApplications;
        }


        public IEnumerable<Application> ApplicationsAsEnumerable()
        {
            return ApplicationDatasets?.Select(sa => sa.Application);
        }

        }


    /// <summary>
    /// 
    /// </summary>
    public enum AccessRight
    {
        [Display(Name = "Offentlige data")]
        PublicData,
        [Display(Name = "Data med  begrenset tilgang")]
        RestrictedData,
        [Display(Name = "Ikke offentlige data")]
        NonPublicData
    }






}
