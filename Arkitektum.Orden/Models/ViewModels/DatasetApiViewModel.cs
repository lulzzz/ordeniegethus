using System.Collections.Generic;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class DatasetApiViewModel : Mapper<Dataset, DatasetApiViewModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Purpose { get; set; }
        public bool HasPersonalData { get; set; }
        public bool HasSensitivePersonalData { get; set; }
        
        public override DatasetApiViewModel Map(Dataset input)
        {
            return new DatasetApiViewModel { 
                Id = input.Id, 
                Name = input.Name,
                Description = input.Description,
                Purpose = input.Purpose,
                HasPersonalData = input.HasPersonalData,
                HasSensitivePersonalData = input.HasSensitivePersonalData
            };
        }

        public override IEnumerable<DatasetApiViewModel> MapToEnumerable(IEnumerable<Dataset> inputs)
        {
            throw new System.NotImplementedException();
        }
    }
}
