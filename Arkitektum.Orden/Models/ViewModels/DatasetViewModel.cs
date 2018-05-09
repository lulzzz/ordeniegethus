using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class DatasetViewModel : Mapper<Dataset, DatasetViewModel>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Purpose { get; set; }
        public List<SelectListItem> AvailableAccessRights { get; set; }
        public AccessRight? AccessRight { get; set; }
        public List<SelectListItem> AvailableApplications { get; set; }
        public IEnumerable<ApplicationApiViewModel> Applications { get; set; }
        public int Application { get; set; }
        public DateTime? PublishedToSharedDataCatalog { get; set; }
        public HostingLocation? HostingLocation { get; set; }
        public string HostingLocationText { get; set; }
        public bool HasMasterData { get; set; }
        public bool HasPersonalData { get; set; }
        public bool HasSensitivePersonalData { get; set; }
        public int? OrganizationId { get; set; }
        public string AccessPermission { get; set; }
        public IEnumerable<DatasetFieldViewModel> Fields { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModified { get; set; }
        
        public override IEnumerable<DatasetViewModel> MapToEnumerable(IEnumerable<Dataset> inputs)
        {
            var viewModels = new List<DatasetViewModel>();

            foreach (var item in inputs)
            {
                viewModels.Add(Map(item));
            }

            return viewModels;
        }

        public override DatasetViewModel Map(Dataset input)
        {
            return new DatasetViewModel
            {
                Id = input.Id,
                Name = input.Name,
                Description = input.Description,
                Purpose = input.Purpose,
                AccessRight = input.AccessRight,
                HasMasterData = input.HasMasterData,
                HasPersonalData = input.HasPersonalData,
                HasSensitivePersonalData = input.HasSensitivePersonalData,
                PublishedToSharedDataCatalog = input.PublishedToSharedDataCatalog,
                HostingLocation = input.HostingLocation,
                HostingLocationText = ApplicationViewModel.TranslateHostingLocation(input.HostingLocation),
                OrganizationId = input.OrganizationId,
                Applications = new ApplicationApiViewModel().Map(input.ApplicationDatasets),
                Fields = new DatasetFieldViewModel().Map(input.Fields),
                DateCreated = input.DateCreated,
                UserCreated = input.UserCreated,
                DateModified = input.DateModified,
                UserModified = input.UserModified
            };
        }


        private List<SelectListItem> Map(List<ApplicationDataset> applicationDatasets)
        {
            var output = new List<SelectListItem>();
            if (applicationDatasets != null)
            {
                foreach (var item in applicationDatasets)
                {
                    output.Add(Map(item));
                }
            }

            return output;
        }

        private SelectListItem Map(ApplicationDataset item)
        {
            SelectListItem selectListItem = new SelectListItem();
            selectListItem.Value = item.ApplicationId.ToString();
            selectListItem.Text = item.Application.Name;

            return selectListItem;
        }

        public Dataset Map(DatasetViewModel input)
        {
            if (input.Application != 0)
            {
                return new Dataset
                {
                    Id = input.Id,
                    Name = input.Name,
                    Description = input.Description,
                    Purpose = input.Purpose,
                    HasMasterData = input.HasMasterData,
                    HasPersonalData = input.HasPersonalData,
                    HasSensitivePersonalData = input.HasSensitivePersonalData,
                    PublishedToSharedDataCatalog = input.PublishedToSharedDataCatalog?.Date,
                    HostingLocation = input.HostingLocation,
                    ApplicationDatasets = Map(input.Application, input.Id),
                    AccessRight = input.AccessRight,
                    OrganizationId = input.OrganizationId.Value
                };
            }

            return new Dataset
            {
                Id = input.Id,
                Name = input.Name,
                Description = input.Description,
                Purpose = input.Purpose,
                HasMasterData = input.HasMasterData,
                HasPersonalData = input.HasPersonalData,
                HasSensitivePersonalData = input.HasSensitivePersonalData,
                PublishedToSharedDataCatalog = input.PublishedToSharedDataCatalog?.Date,
                HostingLocation = input.HostingLocation,
                AccessRight = input.AccessRight,
                OrganizationId = input.OrganizationId.Value
            };

        }


        private AccessRight Map(string input)
        {
            var accessRight = new AccessRight();

            if (input != null)
            {
                accessRight = Enum.Parse<AccessRight>(input);
            }

            return accessRight;
        }

        private List<ApplicationDataset> Map(int applicationId, int id)
        {
            List<ApplicationDataset> applicationDatasets = new List<ApplicationDataset>();

            var applicationDataset = new ApplicationDataset
            {
                ApplicationId = applicationId,
                DatasetId = id
            };

            applicationDatasets.Add(applicationDataset);

            return applicationDatasets;
        }


        public DatasetViewModel Map(Dataset input, IEnumerable<Application> applications)
        {
            return new DatasetViewModel
            {
                Id = input.Id,
                Name = input.Name,
                Description = input.Description,
                Purpose = input.Purpose,
                AccessRight = input.AccessRight,
                HasMasterData = input.HasMasterData,
                HasPersonalData = input.HasPersonalData,
                HasSensitivePersonalData = input.HasSensitivePersonalData,
                PublishedToSharedDataCatalog = input.PublishedToSharedDataCatalog,
                HostingLocation = input.HostingLocation,
                AvailableApplications = Map(applications)
                
            };

        }

        private List<SelectListItem> Map(IEnumerable<Application> applications)
        {
            List<SelectListItem> output = new List<SelectListItem>();

            foreach (var application in applications)
            {
                SelectListItem item = new SelectListItem
                {
                    Value = application.Id.ToString(),
                    Text = application.Name
                };

                output.Add(item);
            }

            return output;
        }
    }
}
