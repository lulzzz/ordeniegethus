using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Arkitektum.Orden.Models
{
    public class Application : ChangeTrackingEntity
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
        public string Vendor { get; set; }

        /// <summary>
        ///     Støtter disse standardene
        /// </summary>
        public virtual List<ApplicationStandard> ApplicationStandards { get; set; }

        /// <summary>
        ///     Støtter disse felleskomponentene (svarUt, Id-port)
        /// </summary>
        //public List<SharedService> SuppportedSharedServices { get; set; }

        /// <summary>
        ///     Tilgjengelig integrasjonsmuligheter (API)
        /// </summary>
        public virtual List<ApplicationSupportedIntegration> ApplicationSupportedIntegrations { get; set; }

        public CommonApplication CopyToCommonApplication()
        {
            var commonApp = new CommonApplication() {
                Name = this.Name,
                Versions = new List<CommonApplicationVersion>()
                {
                    new CommonApplicationVersion
                    {
                        VersionNumber = this.Version,
                    }
                },
                OriginalApplicationId = this.Id
            };

            foreach(var appDataset in ApplicationDatasets)
            {
                commonApp.CommonDatasets.Add(appDataset.Dataset.CopyToCommonDataset());
            }

            return commonApp;
        }

        /// <summary>
        ///     Årlig kostnad
        /// </summary>

        public decimal AnnualFee { get; set; }

        /// <summary>
        ///     Innkjøpskostnad
        /// </summary>

        public decimal InitialCost { get; set; }

        /// <summary>
        ///     Systemeier
        /// </summary>
        public virtual ApplicationUser SystemOwner { get; set; }

        public string SystemOwnerId { get; set; }

        /// <summary>
        ///     Superbrukere
        /// </summary>
        public virtual List<ApplicationSuperUser> SuperUsers { get; set; }


        /// <summary>
        ///     Lenker til eksterne ressurser
        /// </summary>
        public virtual List<ResourceLink> ResourceLinks { get; set; }

        /// <summary>
        ///     Driftsplassering
        /// </summary>
        public string HostingLocation { get; set; }

        /// <summary>
        ///     Driftsleverandør
        /// </summary>
        public string HostingVendor { get; set; }

        /// <summary>
        ///     Antall brukere
        /// </summary>
        public int NumberOfUsers { get; set; }


        /// <summary>
        ///     Tjenesteområder
        /// </summary>
        public virtual List<SectorApplication> SectorApplications { get; set; }

        /// <summary>
        ///     Datasett
        /// </summary>
        public virtual List<ApplicationDataset> ApplicationDatasets { get; set; }

        /// <summary>
        ///     Felleskomponenter
        /// </summary>
        public virtual List<ApplicationNationalComponent> ApplicationNationalComponent { get; set; }
       
        public int? OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }


        public string DecimalToString(decimal inputDecimal)
        {
            string priceAsString = decimal.Round(inputDecimal, 0, MidpointRounding.AwayFromZero).ToString();
            return priceAsString;
        }

        /// <summary>
        /// Updates the list of sectors with the incoming list. 
        /// Preserves existing sectors that are tracked by entity framework which still should be in the list. 
        /// </summary>
        /// <param name="updatedSectorApplications"></param>
        public void UpdateSectorRelations(List<SectorApplication> updatedSectorApplications)
        {
            var updatedSectorIds = updatedSectorApplications.Select(sa => sa.SectorId).ToList();

            List<SectorApplication> updatedListOfSectors = new List<SectorApplication>();

            foreach (var sector in SectorApplications)
            {
                if (updatedSectorIds.Contains(sector.SectorId))
                {
                    updatedListOfSectors.Add(sector);
                    updatedSectorApplications.RemoveAll(sa => sa.SectorId == sector.SectorId);
                }
            }
            updatedListOfSectors.AddRange(updatedSectorApplications);

            SectorApplications = updatedListOfSectors;
        }

        public void UpdateNationalComponentsRelations(List<ApplicationNationalComponent> updatedApplicationNationalComponent)
        {
            var nationalComponentsIdsToUpdate =
                updatedApplicationNationalComponent.Select(anc => anc.NationalComponentId).ToList();

            List<ApplicationNationalComponent> updatedListOfNationalComponents = new List<ApplicationNationalComponent>();

            foreach (var item in ApplicationNationalComponent)
            {
                if (nationalComponentsIdsToUpdate.Contains(item.NationalComponentId))
                {
                    updatedListOfNationalComponents.Add(item);
                    updatedApplicationNationalComponent.RemoveAll(anc =>
                        anc.NationalComponentId == item.NationalComponentId);
                }
            }
            updatedListOfNationalComponents.AddRange(updatedApplicationNationalComponent);

            ApplicationNationalComponent = updatedListOfNationalComponents;
        }
    }
}