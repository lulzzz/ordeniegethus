namespace Arkitektum.Orden.Models
{
    /// <summary>
    ///     Many-to-many relation between Application and Standard
    /// </summary>
    public class ApplicationStandard
    {
        public int ApplicationId { get; set; }
        public int StandardId { get; set; }

        public virtual Application Application { get; set; }

        public virtual Standard Standard { get; set; }
    }
}