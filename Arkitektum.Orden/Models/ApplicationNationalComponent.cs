namespace Arkitektum.Orden.Models
{
    /// <summary>
    ///     Many-to-many relation between Application and NationalComponent
    /// </summary>
    public class ApplicationNationalComponent
    {
        public int ApplicationId { get; set; }
        public int NationalComponentId { get; set; }

        public Application Application { get; set; }

        public NationalComponent NationalComponent { get; set; }
    }
}