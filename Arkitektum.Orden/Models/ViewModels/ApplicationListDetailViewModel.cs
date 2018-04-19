using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class ApplicationListDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string AnnualFee { get; set; }
        public string SystemOwner { get; set; }
        public string Vendor { get; set; }

        public IEnumerable<ApplicationListDetailViewModel> Map(IEnumerable<Application> applications)
        {
            var model = new List<ApplicationListDetailViewModel>();
            foreach (var application in applications)
            {
                model.Add(Map(application));
            }

            return model;
        }

        private ApplicationListDetailViewModel Map(Application input)
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
