using System;
using System.Collections.Generic;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class SectorApiViewModel : Mapper<Sector, SectorApiViewModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override SectorApiViewModel Map(Sector input)
        {
            return new SectorApiViewModel { Id = input.Id, Name = input.Name };
        }

        public override IEnumerable<SectorApiViewModel> MapToEnumerable(IEnumerable<Sector> inputs)
        {
            throw new NotImplementedException();
        }

       
    }
}
