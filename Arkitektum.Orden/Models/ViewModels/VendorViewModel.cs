using System;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class VendorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HomepageUrl {get;set;}

        internal VendorViewModel Map(Vendor vendor)
        {
            return new VendorViewModel
            {
                Id = vendor.Id,
                Name = vendor.Name,
                HomepageUrl = vendor.HomepageUrl
            };
        }
    }
}
