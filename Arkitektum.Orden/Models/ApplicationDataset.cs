using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arkitektum.Orden.Models
{
    public class ApplicationDataset
    {
        public int ApplicationId { get; set; }
        public int DatasetId { get; set; }

        public Application Application { get; set; }

        public Dataset Dataset { get; set; }
    }
}
