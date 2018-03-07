using System.Collections.Generic;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class SectorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ApplicationViewModel> Applications { get; set; }

        public static SectorViewModel Map(Sector input)
        {
            return new SectorViewModel
            {
                Id = input.Id,
                Name = input.Name,
                Applications = new ApplicationViewModel().MapToEnumerable(input.ApplicationsAsEnumerable())
            };
        }
    }
}