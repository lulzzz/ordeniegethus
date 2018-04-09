using System.Collections.Generic;
using System.Linq;
using Arkitektum.Orden.Models;

namespace Arkitektum.Orden.Services.Search
{
    public class ApplicationDocument
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string Vendor { get; set; }
        public decimal AnnualFee { get; set; }
        public decimal InitialCost { get; set; }
        public OrganizationDoc Organization { get; set; }
        public UserDoc SystemOwner { get; set; }

        public List<SectorDoc> Sectors { get; set; }

        public ApplicationDocument()
        {
        }

        public ApplicationDocument(Application application)
        {
            Id = application.Id;
            Name = application.Name;
            Version = application.Version;
            Vendor = application.Vendor;
            AnnualFee = application.AnnualFee;
            InitialCost = application.InitialCost;
            Organization = new OrganizationDoc(application.Organization);
            SystemOwner = application.SystemOwner == null ? null : new UserDoc(application.SystemOwner);
            Sectors = SectorDoc.Map(application.SectorApplications);
        }
    }

    public class SectorDoc
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static List<SectorDoc> Map(List<SectorApplication> sectorApplications)
        {
            var output = new List<SectorDoc>();
            if (sectorApplications.Any())
                foreach (var sector in sectorApplications)
                    output.Add(new SectorDoc
                    {
                        Id = sector.SectorId,
                        Name = sector.Sector.Name
                    });
            return output;
        }
    }

    public class UserDoc
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public UserDoc()
        {
        }

        public UserDoc(ApplicationUser applicationUser)
        {
            Id = applicationUser.Id;
            Name = applicationUser.FullName;
        }
    }

    public class OrganizationDoc
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public OrganizationDoc()
        {
        }

        public OrganizationDoc(Organization organization)
        {
            Id = organization.Id;
            Name = organization.Name;
        }
    }
}