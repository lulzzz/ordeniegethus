using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class InsightsViewModel
    {
       
        public IEnumerable<Application> Applications { get; set; }
        public IEnumerable<Sector> Sectors { get; set; }
        public Dictionary<string, IEnumerable<ApplicationListDetailViewModel>> ApplicationsForNationalComponent { get; set; }

    }
}
