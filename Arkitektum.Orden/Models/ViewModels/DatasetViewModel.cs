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
        public string AccessRight { get; set; }
        public List<SelectListItem> AvailableApplications { get; set; }
        public string Application { get; set; }
        public DateTime? PublishedToSharedDataCatalog { get; set; }
        public string DataLocation { get; set; }
        public bool HasMasterData { get; set; }
        public bool HasPersonalData { get; set; }
        public bool HasSensitivePersonalData { get; set; }



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
                AccessRight = input.AccessRight.ToString(),
                HasMasterData = input.HasMasterData,
                HasPersonalData = input.HasPersonalData,
                HasSensitivePersonalData = input.HasSensitivePersonalData,
                PublishedToSharedDataCatalog = input.PublishedToSharedDataCatalog,
                DataLocation = input.DataLocation
            };
        }

        public Dataset Map(DatasetViewModel input)
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
                PublishedToSharedDataCatalog = input.PublishedToSharedDataCatalog,
                DataLocation = input.DataLocation
             
            };
        } 
    }
}
