using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class ApplicationViewModel : Mapper<Application, ApplicationViewModel>
    {
   
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Version { get; set; }
        public string Vendor {get; set;}
        public string AnnualFee { get; set; }
        public List<SelectListItem> AvailableSuperUsers { get; set; }
        public string SystemOwner { get; set; }
        public string InitialCost { get; set; }
        public string HostingLocation { get; set; }
        public int NumberOfUsers { get; set; }
        public int? OrganizationId { get; set; }
        public List<CheckboxApplicationSector> Sectors { get; set; }

        public override IEnumerable<ApplicationViewModel> MapToEnumerable(IEnumerable<Application> inputs)
        {
            var viewModels = new List<ApplicationViewModel>();
            if (inputs != null)
                foreach (var item in inputs) viewModels.Add(Map(item));

            return viewModels;
        }

        public override ApplicationViewModel Map(Application input)
        {
           return new ApplicationViewModel
           {
               Id = input.Id,
               Name = input.Name,
               AnnualFee = input.DecimalToString(input.AnnualFee),
               SystemOwner = input.SystemOwner?.FullName,
               Vendor = input.Vendor,
               Version = input.Version,
               InitialCost = input.DecimalToString(input.InitialCost),
               HostingLocation = input.HostingLocation,
               NumberOfUsers = input.NumberOfUsers,
               OrganizationId = input.OrganizationId,
               Sectors = Map(input.SectorApplications)
           };
        }

        private List<CheckboxApplicationSector> Map(List<SectorApplication> input)
        {
            var output = new List<CheckboxApplicationSector>();
            if (input != null)
            {
                foreach (var item in input)
                {
                    output.Add(Map(item));
                }
            }

            return output;
        }

        private CheckboxApplicationSector Map(SectorApplication input)
        {
            return new CheckboxApplicationSector()
            {
                SectorId = input.SectorId,
                SectorName = input.Sector.Name,
                Selected = true
            };
        }

        public Application Map(ApplicationViewModel input)
        {
            return new Application
            {
                Id = input.Id,
                Name = input.Name,
                AnnualFee = System.Convert.ToDecimal(input.AnnualFee),
                SystemOwnerId = input.SystemOwner,
                Vendor = input.Vendor,
                Version = input.Version,
                InitialCost = System.Convert.ToDecimal(input.InitialCost),
                HostingLocation = input.HostingLocation,
                NumberOfUsers = input.NumberOfUsers,
                OrganizationId = input.OrganizationId,
                SectorApplications = Map(input.Sectors, input.Id)
            };
        }

        private List<SectorApplication> Map(List<CheckboxApplicationSector> input, int applicationId)
        {
            var sectorApplications = new List<SectorApplication>();
            if (input != null)
            {
                foreach (var item in input.Where(i => i.Selected))
                {
                    sectorApplications.Add(new SectorApplication()
                    {
                        ApplicationId = applicationId,
                        SectorId = item.SectorId
                    });
                }
            }

            return sectorApplications;
        }

        public void MergeSectors(List<CheckboxApplicationSector> availableSectors)
        {
            List<int> sectorIds = Sectors.Select(s => s.SectorId).ToList();
            foreach (var availableSector in availableSectors)
            {
                if (sectorIds.Contains(availableSector.SectorId))
                    continue;

                Sectors.Add(availableSector);
            }
        }
    }

    public class CheckboxApplicationSector
    {
        public int SectorId { get; set; }
        public string SectorName { get; set; }
        public bool Selected { get; set; }
    }

}
