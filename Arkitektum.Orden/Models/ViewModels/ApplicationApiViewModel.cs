using System.Collections.Generic;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class ApplicationApiViewModel : Mapper<Application, ApplicationApiViewModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string VendorName { get; set; }
        public string DatasetRoleName { get; set; }
        
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

        public IEnumerable<ApplicationApiViewModel> Map(IEnumerable<ApplicationDataset> applicationsForDataset)
        {
            var viewModels = new List<ApplicationApiViewModel>();
            foreach (var item in applicationsForDataset)
            {
                ApplicationApiViewModel application = Map(item.Application);
                application.DatasetRoleName = item.RoleName;
                viewModels.Add(application);
            }
            return viewModels;
        }
    }
}
