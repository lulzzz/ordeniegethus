using System.ComponentModel.DataAnnotations;

namespace Arkitektum.Orden.Models
{
    public enum HostingLocation
    {
        [Display(Name = "Sky")] Cloud,
        [Display(Name = "Lokal server")] LocalServer,
        [Display(Name = "Ekstern server")] ExternalServer
    }
}