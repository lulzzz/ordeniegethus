namespace Arkitektum.Orden.Models
{
    /// <summary>
    ///     Many-to-many relation between Application and Dataset
    /// </summary>
    public class ApplicationDataset
    {
        public int ApplicationId { get; set; }
        public int DatasetId { get; set; }

        public AccessPermission AccessPermission { get; set; }

        public virtual Application Application { get; set; }

        public virtual Dataset Dataset { get; set; }

      }

    public enum AccessPermission
    {
        Read,
        Write
    }
}