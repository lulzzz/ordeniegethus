using System.Collections.Generic;

namespace Arkitektum.Orden.Models
{
    public class CommonApplication
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual List<CommonApplicationVersion> Versions { get; set; }
        public virtual List<CommonDataset> CommonDatasets { get; set; }
    }
}