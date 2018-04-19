using System.Collections.Generic;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class NationalComponentApiViewModel : Mapper<NationalComponent, NationalComponentApiViewModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override NationalComponentApiViewModel Map(NationalComponent input)
        {
            return new NationalComponentApiViewModel
            {
                Id = input.Id,
                Name = input.Name
            };
        }

        public override IEnumerable<NationalComponentApiViewModel> MapToEnumerable(IEnumerable<NationalComponent> inputs)
        {
            throw new System.NotImplementedException();
        }
    }
}
