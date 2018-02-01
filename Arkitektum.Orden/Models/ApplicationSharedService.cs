using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arkitektum.Orden.Models
{
    public class ApplicationSharedService
    {
        public int ApplicationId { get; set; }
        public int SharedServiceId { get; set; }

        public Application Application { get; set; }

        public SharedService SharedService { get; set; }
    }
}
