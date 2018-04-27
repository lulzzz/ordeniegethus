using System.Collections.Generic;
using Arkitektum.Orden.Models.ViewModels;

namespace Arkitektum.Orden.Models.Api
{
    public class StandardViewModel : Mapper<Standard, StandardViewModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public override IEnumerable<StandardViewModel> MapToEnumerable(IEnumerable<Standard> inputs)
        {
            throw new System.NotImplementedException();
        }

        public override StandardViewModel Map(Standard input)
        {
            return new StandardViewModel()
            {
                Id = input.Id,
                Name = input.Name
            };
        }
    }
}