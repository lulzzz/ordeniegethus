using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using Nest;

namespace Arkitektum.Orden.Models
{
    public class Dataset : ChangeTrackingEntity
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
        public string Keywords { get; set; }



        /// <summary>
        /// Hovedidentifikator for datasettet, for eksempel en URI 
        /// eller annen identifikator som er stabil og globalt unik
        /// </summary>
        public string Identifiers { get; set; }

        /// <summary>
        /// Dette feltet angir i hvilken grad datasettet kan bli gjort
        /// tilgjengelig for allmennheten, uten hensyn til om det er
        /// publisert eller ikke. Et kontrollert vokabular med tre
        /// verdier (:public, :restricted og :non-public) vil bli 
        /// opprettet og forvaltet av EUs Publication Office. Ved bruk
        /// av verdiene ":restricted" og ":non-public" er egenskapen 
        /// skjermingshjemmel anbefalt.
        /// </summary>
        public AccessRight? AccessRight { get; set; }

        internal CommonDataset CopyToCommonDataset()
        {
            return new CommonDataset
            {
                Name = this.Name,
                Description = this.Description,
                Purpose = this.Purpose,
                HasPersonalData = this.HasPersonalData,
                HasSensitivePersonalData = this.HasSensitivePersonalData,
            };
        }

        /// <summary>
        /// Referanse til hjemmel (kilde for påstand) i offentlighetsloven,
        ///  sikkerhetsloven, beskyttelsesinstruksen eller annet lovverk
        ///  som ligger til grunn for vurdering av tilgangsnivå. Egenskapen
        ///  er anbefalt dersom «tilgangsnivå» har verdiene «restricted»
        ///  eller «non-public»
        /// </summary>
        public string AccessRightComments { get; set; }

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
        public HostingLocation? HostingLocation { get; set; }

        public int OrganizationId { get; set; }

        /// <summary>
        /// Referanse til et hovedtema for datasettet. Et datasett 
        /// kan assosieres med flere tema. Bruk av vokabularet Data 
        /// theme som er publisert av EUs Publication Offices er påkrevd. 
        /// Bruk av Difis Los-vokabular er anbefalt. Også andre nasjonale
        /// og internasjonale vokabular kan brukes om de oppfyller denne
        /// standardens krav til kontrollerte vokabular.
        /// </summary>
        public string Concepts { get; set; }

        ///<summary>
        /// Referanse til kontaktpunktsobjekt med kontaktopplysninger. 
        /// Disse kan brukes til å sende kommentarer om datasettet.
        /// </summary>
        public string ContactPoints { get; set; }

        ///<summary>
        /// Referanse til sentrale begrep som er viktige for å forstå og tolke
        ///  datasettet. Representeres som URI-er
        /// </summary>
        public string Subjects { get; set; }

        /// <summary>
        /// Lenker til eksterne ressurser
        /// </summary>
        public virtual List<ResourceLink> ResourceLinks { get; set; }

        /// <summary>
        ///     Lovhjemmel/forskrift for forvaltning
        /// </summary>
        public virtual List<ResourceLink> LawReferences { get; set; }

        /// <summary>
        ///     Publisert til felles datakatalog
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PublishedToSharedDataCatalog { get; set; }


        /// <summary>
        /// Informasjonselementer i datasettet
        /// </summary>
        public virtual List<Field> Fields { get; set; }


        /// <summary>
        /// Applikasjoner
        /// </summary>
        public virtual List<ApplicationDataset> ApplicationDatasets { get; set; }


        /// <summary>
        /// En katalog eller repository som inneholder beskrivelsene 
        /// av datasettene som er beskrevet.
        /// </summary>

        public virtual DcatCatalog DcatCatalog { get; set; }

        /// <summary>
        /// Koblingen mellom datasettet og en tilgjengelig distribusjon
        /// </summary>
        public string Distributions { get; set; }

        public Dataset()
        {
            ResourceLinks = new List<ResourceLink>();
            LawReferences = new List<ResourceLink>();
            Fields = new List<Field>();
            KeywordsAsList = new List<string>();
            ConceptsAsList = new List<string>();
            IdentifiersAsList = new List<string>();
            ContactPointsAsList = new List<string>();
            DistributionsAsList = new List<string>();
            AccessRightCommentsAsList = new List<string>();
            SubjectsAsList = new List<string>();
            DescriptionsAsList = new List<string>();
        }


        private List<string> SplitByPipe(string input)
        {
            if (input == null)
                return new List<string>();

            return input.Split('|').ToList();
        }

        private string JoinByPipe(List<string> input)
        {
            if (input == null || input.Count == 0)
                return null;

            return string.Join('|', input);
        }

       [NotMapped]
        public  List<string> KeywordsAsList
        {
            get => SplitByPipe(Keywords);
            set => Keywords = JoinByPipe(value);
        }

        [NotMapped]
        public List<string> DescriptionsAsList
        {
            get => SplitByPipe(Description);
            set => Description = JoinByPipe(value);
        }

        [NotMapped]
        public List<string> ConceptsAsList
        {
            get => SplitByPipe(Concepts);
            set => Concepts = JoinByPipe(value);
        }

        [NotMapped]
        public List<string> IdentifiersAsList
        {
            get => SplitByPipe(Identifiers);
            set => Identifiers = JoinByPipe(value);
        }

        [NotMapped]
        public List<string> ContactPointsAsList
        {
            get => SplitByPipe(ContactPoints);
            set => ContactPoints = JoinByPipe(value);
        }

        [NotMapped]
        public List<string> DistributionsAsList
        {
            get => SplitByPipe(Distributions);
            set => Distributions = JoinByPipe(value);
        }

        [NotMapped]
        public List<string> AccessRightCommentsAsList
        {
            get => SplitByPipe(AccessRightComments);
            set => AccessRightComments = JoinByPipe(value);
        }

        [NotMapped]
        public List<string> SubjectsAsList
        {
            get => SplitByPipe(Subjects);
            set => Subjects = JoinByPipe(value);
        }

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
