using System.Collections.Generic;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class ApplicationApiViewModel : Mapper<Application, ApplicationApiViewModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string VendorName { get; set; }
        
        public override ApplicationApiViewModel Map(Application input)
        {
            return new ApplicationApiViewModel { 
                Id = input.Id, 
                Name = input.Name,
                Version = input.Version,
                VendorName = input.Vendor?.Name
            };
        }

        public override IEnumerable<ApplicationApiViewModel> MapToEnumerable(IEnumerable<Application> inputs)
        {
            throw new System.NotImplementedException();
        }
    }
}
