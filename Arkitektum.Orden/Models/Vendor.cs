using System.Collections.Generic;

namespace Arkitektum.Orden.Models
{
    public class Vendor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HomepageUrl {get;set;}
        public virtual List<Application> Applications { get; set; }
    }
}