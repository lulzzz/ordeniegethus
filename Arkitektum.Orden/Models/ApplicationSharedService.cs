namespace Arkitektum.Orden.Models
{
    /// <summary>
    ///     Many-to-many relation between Application and SharedService
    /// </summary>
    public class ApplicationSharedService
    {
        public int ApplicationId { get; set; }
        public int SharedServiceId { get; set; }

        public Application Application { get; set; }

        public SharedService SharedService { get; set; }
    }
}