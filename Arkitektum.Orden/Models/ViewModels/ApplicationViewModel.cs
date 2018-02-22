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
        public decimal AnnualFee { get; set; }
        public List<SelectListItem> AvailableSuperUsers { get; set; }
        public string SystemOwner { get; set; }
        
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
               Id = input.Id,
               Name = input.Name,
               AnnualFee = input.AnnualFee,
               SystemOwner = input.SystemOwner.FullName,
               //Vendor = input.Vendor.ToString(),
               Version = input.Version
           };
        }

        public Application Map(ApplicationViewModel input)
        {
            return new Application
            {
                Id = input.Id,
                Name = input.Name,
                AnnualFee = input.AnnualFee,
                SystemOwnerId = input.SystemOwner,
             
                
            };
        }
    }
}
