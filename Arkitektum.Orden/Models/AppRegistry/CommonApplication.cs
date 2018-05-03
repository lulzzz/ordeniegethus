using System;
using System.Collections.Generic;
using System.Linq;
using Arkitektum.Orden.Models.AppRegistry;

namespace Arkitektum.Orden.Models
{
    public class CommonApplication
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual List<CommonApplicationVersion> Versions { get; set; } = new List<CommonApplicationVersion>();
        public virtual List<CommonDataset> CommonDatasets { get; set; } = new List<CommonDataset>();
        public int OriginalApplicationId { get; set; }

        public AppRegistryStatus Status { get; set; } = AppRegistryStatus.Submitted;
        
        public int? SubmittedByOrganizationId { get; set; }
        public virtual Organization SubmittedByOrganization { get; set; }
        public string SubmittedByUserId { get; set; }
        public virtual ApplicationUser SubmittedByUser { get; set; }
        public DateTime? SubmittedDateTime { get; set; } = DateTime.Now;
        
        public Application CreateApplicationForOrganization(int organizationId, string versionNumber)
        {
            var application = new Application
            {
                Name = Name,
                VendorId = VendorId,
                CreatedFromCommonApplicationId = Id,
                OrganizationId = organizationId
            };

            var selectedVersion = Versions.FirstOrDefault(v => v.VersionNumber == versionNumber);
            if (selectedVersion != null)
            {
                application.Version = versionNumber;
                foreach (var component in selectedVersion.SupportedNationalComponents)
                    application.ApplicationNationalComponent.Add(new ApplicationNationalComponent
                    {
                        NationalComponentId = component.NationalComponentId
                    });
            }

            foreach (var dataset in CommonDatasets)
                application.ApplicationDatasets.Add(new ApplicationDataset
                {
                    Dataset = new Dataset
                    {
                        Name = dataset.Name,
                        Description = dataset.Description,
                        Purpose = dataset.Purpose,
                        HasPersonalData = dataset.HasPersonalData,
                        HasSensitivePersonalData = dataset.HasSensitivePersonalData,
                        HasMasterData = dataset.HasMasterData,
                        OrganizationId = organizationId
                    }
                });
            return application;
        }
    }
}