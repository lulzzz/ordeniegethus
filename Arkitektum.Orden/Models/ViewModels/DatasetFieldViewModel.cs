using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class DatasetFieldViewModel : Mapper<Field, DatasetFieldViewModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPersonalData { get; set; }
        public bool IsSensitivePersonalData { get; set; }

        public override IEnumerable<DatasetFieldViewModel> MapToEnumerable(IEnumerable<Field> inputs)
        {
            var viewModels = new List<DatasetFieldViewModel>();

            if (inputs != null)
            {
                foreach (var item in inputs)
                {
                    viewModels.Add(Map(item));
                }
            }

            return viewModels;
        }

        public override DatasetFieldViewModel Map(Field input)
        {
            var model = new DatasetFieldViewModel
            {
                Id = input.Id,
                Description = input.Description,
                IsPersonalData = input.IsPersonalData,
                IsSensitivePersonalData = input.IsSensitivePersonalData,
                Name = input.Name
            };

            return model;
        }

        public Field Map(DatasetFieldViewModel model, int datasetId)
        {
            return new Field()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                IsPersonalData = model.IsPersonalData,
                IsSensitivePersonalData = model.IsSensitivePersonalData,
                DatasetId = datasetId
            };
        }
    }
}
