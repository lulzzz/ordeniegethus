using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class DatasetsOverviewViewModel 
    {
        public int Id { get; set; }
        public IEnumerable<DatasetViewModel> PublishedDatasets { get; set; }
        public IEnumerable<DatasetViewModel> NotPublishedDatasets { get; set; }

 
    }
}
