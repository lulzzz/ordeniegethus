using System.Collections.Generic;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class DatasetApiViewModel : Mapper<Dataset, DatasetApiViewModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override DatasetApiViewModel Map(Dataset input)
        {
            return new DatasetApiViewModel { Id = input.Id, Name = input.Name };
        }

        public override IEnumerable<DatasetApiViewModel> MapToEnumerable(IEnumerable<Dataset> inputs)
        {
            throw new System.NotImplementedException();
        }
    }
}
