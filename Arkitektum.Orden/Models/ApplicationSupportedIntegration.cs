using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arkitektum.Orden.Models
{
    public class ApplicationSupportedIntegration
    {
        public int ApplicationId { get; set; }
        public int SupportedIntegrationId { get; set; }

        public Application Application { get; set; }
        public Integration SupportedIntegration { get; set; }
    }
}
