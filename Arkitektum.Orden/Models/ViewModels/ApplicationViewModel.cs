using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class ApplicationViewModel : Mapper<Application, ApplicationViewModel>, IValidatableObject
    {
   
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
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
        [EnumDataType(typeof(Models.HostingLocation))]
        public Models.HostingLocation? HostingLocation { get; set; }

        /// <summary>
        /// Translated value of HostingLocation to use when displaying data
        /// </summary>
        public string HostingLocationText { get; set; }

        public List<SelectListItem> AvailableHostingLocations { get; set; }

        public int NumberOfUsers { get; set; }

        public int? OrganizationId { get; set; }

        public List<SectorApiViewModel> Sectors { get; set; }

        public List<NationalComponentApiViewModel> NationalComponents { get; set; }

        public List<DatasetApiViewModel> Datasets { get;set; }

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

        public static string TranslateHostingLocation(HostingLocation? location)
        {
            if (location == null)
                return "";
            
            if (location.Value == Models.HostingLocation.Cloud)
                return UIResource.HostingLocationCloud;
            else if (location.Value == Models.HostingLocation.LocalServer)
                return UIResource.HostingLocationLocalServer;
            else if (location.Value == Models.HostingLocation.ExternalServer)
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

        private List<DatasetApiViewModel> Map(List<ApplicationDataset> datasets)
        {
            var output = new List<DatasetApiViewModel>();
            if (datasets != null && datasets.Any())
            {
                var sortedDatasets = datasets.OrderBy(d => d.Dataset.Name);
                foreach (var item in sortedDatasets)
                {
                    output.Add(new DatasetApiViewModel().Map(item.Dataset));
                }
            }
            return output;
        }

        private List<NationalComponentApiViewModel> Map(List<ApplicationNationalComponent> input)
        {
            var output = new List<NationalComponentApiViewModel>();

            if (input != null)
            {
                var sortedInput = input.OrderBy(a => a.NationalComponent.Name);
                foreach (var item in input)
                {
                    output.Add(new NationalComponentApiViewModel { Id = item.NationalComponentId, Name = item.NationalComponent.Name });
                }
            }

            return output;
        }

        private List<SectorApiViewModel> Map(List<SectorApplication> input)
        {
            var output = new List<SectorApiViewModel>();
            if (input != null)
            {
                var sortedInput = input.OrderBy(a => a.Sector.Name);
                foreach (var item in sortedInput)
                {
                    output.Add(new SectorApiViewModel { Id = item.SectorId, Name = item.Sector.Name });
                }
            }

            return output;
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
                VendorId = input.VendorId,
                Vendor = CreateNewVendor(input.VendorId, input.VendorName)
            };
        }

        private HostingLocation GetEnumHostingLocation(string input)
        {
            Enum.TryParse(input, out Models.HostingLocation result);
            return result;
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (VendorId == 0 && string.IsNullOrWhiteSpace(VendorName))
            {
                yield return new ValidationResult(UIResource.ApplicationCreateModelVendorNotDefined, new List<string>() { "VendorId" });
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
