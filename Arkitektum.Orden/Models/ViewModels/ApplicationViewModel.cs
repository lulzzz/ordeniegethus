using System;
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

        public int VendorId { get; set; }

        public string VendorName {get;set;}

        public VendorViewModel Vendor { get; set; }

        [Range(0, double.MaxValue)]
        [DataType(DataType.Currency, ErrorMessage = "Du kan bruke kun tall")]
        public string AnnualFee { get; set; }

        public List<SelectListItem> AvailableSystemOwners { get; set; }

        public string SystemOwner { get; set; }

        [Range(0, double.MaxValue)]
        [DataType(DataType.Currency, ErrorMessage = "Du kan bruke kun tall")]
        public string InitialCost { get; set; }

        public string HostingVendor { get; set; }

        /// <summary>
        /// Contains enum value of HostingLocation
        /// </summary>
        public string HostingLocation { get; set; }

        /// <summary>
        /// Translated value of HostingLocation to use when displaying data
        /// </summary>
        public string HostingLocationText { get; set; }

        public List<SelectListItem> AvailableHostingLocations { get; set; }

        public int NumberOfUsers { get; set; }

        public int? OrganizationId { get; set; }

        public List<CheckboxApplicationSector> Sectors { get; set; }

        public List<CheckboxApplicationNationalComponents> NationalComponents { get; set; }

        public List<DatasetViewModel> Datasets { get;set; }

        public DateTime? DateCreated { get; set; }
        public string UserCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModified { get; set; }

        public List<SelectListItem> AvailableVendors {get;set;}

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
               Vendor = Map(input.Vendor),
               VendorId = input.VendorId,
               Version = input.Version,
               InitialCost = input.DecimalToString(input.InitialCost),
               HostingVendor = input.HostingVendor,
               HostingLocation = input.HostingLocation,
               HostingLocationText = TranslateHostingLocation(input.HostingLocation),
               NumberOfUsers = input.NumberOfUsers,
               OrganizationId = input.OrganizationId,
               Sectors = Map(input.SectorApplications),
               NationalComponents = Map(input.ApplicationNationalComponent),
               Datasets = Map(input.ApplicationDatasets),
               DateCreated = input.DateCreated,
               DateModified = input.DateModified,
               UserCreated = input.UserCreated,
               UserModified = input.UserModified
           };
        }

        private string TranslateHostingLocation(string value)
        {
            if (value == Models.HostingLocation.Cloud.ToString())
                return UIResource.HostingLocationCloud;
            else if (value == Models.HostingLocation.LocalServer.ToString())
                return UIResource.HostingLocationLocalServer;
            else if (value == Models.HostingLocation.ExternalServer.ToString())
                return UIResource.HostingLocationExternalServer;
            else
                return "";
        }

        private VendorViewModel Map(Vendor vendor)
        {
            VendorViewModel viewModel = null;
            if (vendor != null)
            {
                viewModel = new VendorViewModel
                {
                    Id = vendor.Id,
                    Name = vendor.Name,
                    HomepageUrl = vendor.HomepageUrl
                };
            }
            return viewModel;
        }

        private List<DatasetViewModel> Map(List<ApplicationDataset> datasets)
        {
            var output = new List<DatasetViewModel>();
            if (datasets != null && datasets.Any())
            {
                foreach (var item in datasets)
                {
                    output.Add(new DatasetViewModel()
                    {
                        Id = item.Dataset.Id,
                        Name = item.Dataset.Name,
                        HasMasterData = item.Dataset.HasMasterData,
                        HasPersonalData = item.Dataset.HasPersonalData,
                        HasSensitivePersonalData = item.Dataset.HasSensitivePersonalData
                    });
                }
            }
            return output;
        }

        private List<CheckboxApplicationNationalComponents> Map(List<ApplicationNationalComponent> applicationNationalComponent)
        {
            var output = new List<CheckboxApplicationNationalComponents>();

            if (applicationNationalComponent != null)
            {
                foreach (var item in applicationNationalComponent)
                {
                    output.Add(Map(item));
                }
            }

            return output;
        }

        private CheckboxApplicationNationalComponents Map(ApplicationNationalComponent input)
        {
            return new CheckboxApplicationNationalComponents()
            {
                Id = input.NationalComponentId,
                Name = input.NationalComponent.Name,
                Selected = true
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
                Version = input.Version,
                InitialCost = System.Convert.ToDecimal(input.InitialCost),
                HostingVendor = input.HostingVendor,
                HostingLocation = input.HostingLocation,
                NumberOfUsers = input.NumberOfUsers,
                OrganizationId = input.OrganizationId,
                SectorApplications = Map(input.Sectors, input.Id),
                ApplicationNationalComponent = Map(input.NationalComponents, input.Id),
                VendorId = input.VendorId,
                Vendor = CreateNewVendor(input.VendorId, input.VendorName)
            };
        }

        private Vendor CreateNewVendor(int vendorId, string vendorName)
        {
            if (vendorId == 0 && !string.IsNullOrWhiteSpace(vendorName))
            {
                return new Vendor { Name = vendorName };
            }
            return null;
        }

        private List<ApplicationNationalComponent> Map(List<CheckboxApplicationNationalComponents> input, int id)
        {
            var nationalComponents = new List<ApplicationNationalComponent>();

            if (input != null)
            {
                foreach (var item in input.Where(i => i.Selected))
                {
                    nationalComponents.Add(new ApplicationNationalComponent()
                    {
                        ApplicationId = id,
                        NationalComponentId = item.Id
                    });
                }
            }

            return nationalComponents;
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

    public class CheckboxApplicationNationalComponents
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
    }

    public class CheckboxApplicationSector
    {
        public int SectorId { get; set; }
        public string SectorName { get; set; }
        public bool Selected { get; set; }
    }

}
