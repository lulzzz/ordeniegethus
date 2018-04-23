using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class ApplicationListDetailViewModel : Mapper<Application, ApplicationListDetailViewModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string AnnualFee { get; set; }
        public string SystemOwner { get; set; }
        public string Vendor { get; set; }

        
        public override IEnumerable<ApplicationListDetailViewModel> MapToEnumerable(IEnumerable<Application> inputs)
        {
            var model = new List<ApplicationListDetailViewModel>();
            foreach (var application in inputs)
            {
                model.Add(Map(application));
            }

            return model;
        }

        public override ApplicationListDetailViewModel Map(Application input)
        {
            return new ApplicationListDetailViewModel
            {
                Id = input.Id,
                AnnualFee = input.DecimalToString(input.AnnualFee),
                Name = input.Name,
                Version = input.Version,
                SystemOwner = input.SystemOwner?.FullName,
                Vendor = input.Vendor?.Name,
            };
        }

    }
}
