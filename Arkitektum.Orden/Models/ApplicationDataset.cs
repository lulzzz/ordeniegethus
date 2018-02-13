namespace Arkitektum.Orden.Models
{
    /// <summary>
    ///     Many-to-many relation between Application and Dataset
    /// </summary>
    public class ApplicationDataset
    {
        public int ApplicationId { get; set; }
        public int DatasetId { get; set; }

        public Application Application { get; set; }

        public Dataset Dataset { get; set; }
    }
}