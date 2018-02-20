using System.Collections.Generic;

namespace Arkitektum.Orden.Models
{
    public class SharedService
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<ApplicationSharedService> ApplicationSharedServices { get; set; }

    }
}