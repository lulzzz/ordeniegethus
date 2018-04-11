namespace Arkitektum.Orden.Models
{
    /// <summary>
    ///     Many-to-many between Application and SupportedIntegration
    /// </summary>
    public class ApplicationSupportedIntegration
    {
        public int ApplicationId { get; set; }
        public int SupportedIntegrationId { get; set; }

        public virtual Application Application { get; set; }
        public virtual Integration SupportedIntegration { get; set; }
    }
}