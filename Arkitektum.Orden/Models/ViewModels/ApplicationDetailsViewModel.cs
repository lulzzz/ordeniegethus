namespace Arkitektum.Orden.Models.ViewModels
{
    public class ApplicationDetailsViewModel
    {
        public int ApplicationId { get; set; }
        public int OrganizationId { get; set; }
        public bool HasWriteAccess { get; set; }
    }
}