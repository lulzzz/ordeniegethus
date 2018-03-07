using System.Collections.Generic;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class HomeViewModel
    {
        public int NumberOfApplications { get; set; }
        public int NumberOfDataset { get; set; }
        public int NumberOfPublishedDataset { get; set; }
        public IEnumerable<SectorViewModel> Sectors { get; set; }
    }
}