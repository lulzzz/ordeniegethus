using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arkitektum.Orden.Models
{
    public class ApplicationStandard
    {
        public int ApplicationId { get; set; }
        public int StandardId { get; set; }

        public Application Application { get; set; }

        public Standard Standard { get; set; }
    }
}
