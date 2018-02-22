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
        public Vendor Vendor {get; set;}
        public decimal AnnualFee { get; set; }
        public List<SelectListItem> AvailableSuperUsers { get; set; }
        public string SuperUser { get; set; }

        public override IEnumerable<ApplicationViewModel> MapToEnumerable(IEnumerable<Application> inputs)
        {
            var viewModels = new List<ApplicationViewModel>();
            foreach (var item in inputs)
            {
                viewModels.Add(Map(item));
            }

            return viewModels;
        }

        public override ApplicationViewModel Map(Application input)
        {
           return new ApplicationViewModel
           {
               Name = input.Name,
               AnnualFee = input.AnnualFee,
               //SuperUsers = input.SuperUsers,
               Vendor = input.Vendor,
               Version = input.Version
           };
        }

        public Application Map(ApplicationViewModel input)
        {
            return new Application
            {
                Name = input.Name,
                AnnualFee = input.AnnualFee,
                //SuperUsers = input.SuperUsers,
                Vendor = input.Vendor,
                Version = input.Version
            };
        }
    }
}
