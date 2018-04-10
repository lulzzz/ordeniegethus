using System.Collections.Generic;

namespace Arkitektum.Orden.Models
{
    public class CommonApplication
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Vendor Vendor { get; set; }
        public List<CommonApplicationVersion> Versions { get; set; }
        public List<CommonDataset> CommonDatasets { get; set; }
    }
}