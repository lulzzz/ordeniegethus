namespace Arkitektum.Orden.Models
{
    /// <summary>
    ///     Many-to-many relation between Application and NationalComponent
    /// </summary>
    public class ApplicationNationalComponent
    {
        public int ApplicationId { get; set; }
        public int NationalComponentId { get; set; }

        public virtual Application Application { get; set; }

        public virtual NationalComponent NationalComponent { get; set; }
    }
}