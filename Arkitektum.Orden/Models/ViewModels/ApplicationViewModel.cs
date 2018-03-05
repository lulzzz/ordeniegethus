using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class ApplicationViewModel : Mapper<Application, ApplicationViewModel>
    {
   
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Version { get; set; }
        public string Vendor {get; set;}
        public string AnnualFee { get; set; }
        public List<SelectListItem> AvailableSuperUsers { get; set; }
        public string SystemOwner { get; set; }
        public string InitialCost { get; set; }
        public string HostingLocation { get; set; }
        public int NumberOfUsers { get; set; }

      

        public override IEnumerable<ApplicationViewModel> MapToEnumerable(IEnumerable<Application> inputs)
        {
            var viewModels = new List<ApplicationViewModel>();
            foreach (var item in inputs) viewModels.Add(Map(item));

            return viewModels;
        }

        public override ApplicationViewModel Map(Application input)
        {
           return new ApplicationViewModel
           {
               Id = input.Id,
               Name = input.Name,
               AnnualFee = input.DecimalToString(input.AnnualFee),
               SystemOwner = input.SystemOwner.FullName,
               Vendor = input.Vendor,
               Version = input.Version,
               InitialCost = input.DecimalToString(input.InitialCost),
               HostingLocation = input.HostingLocation,
               NumberOfUsers = input.NumberOfUsers
           };
        }

        public Application Map(ApplicationViewModel input)
        {
            return new Application
            {
                Id = input.Id,
                Name = input.Name,
                AnnualFee = System.Convert.ToDecimal(input.AnnualFee),
                SystemOwnerId = input.SystemOwner,
                Vendor = input.Vendor,
                Version = input.Version,
                InitialCost = System.Convert.ToDecimal(input.InitialCost),
                HostingLocation = input.HostingLocation,
                NumberOfUsers = input.NumberOfUsers,
            };
        }
    }
}
