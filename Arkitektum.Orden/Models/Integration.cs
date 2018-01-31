using System.Collections.Generic;

namespace Arkitektum.Orden.Models
{
    public class Integration
    {
        public int Id { get; set; }

        public List<ApplicationSupportedIntegration> ApplicationSupportedIntegrations { get; set; }
    }
}