namespace Arkitektum.Orden.Models
{
    /// <summary>
    ///     Many-to-many relation between Application and Dataset
    /// RoleName describes the relation type, e.g. 'view' or 'manage'
    /// </summary>
    public class ApplicationDataset
    {
        public int ApplicationId { get; set; }
        public int DatasetId { get; set; }

        public string RoleName { get; set; }
        
        public virtual Application Application { get; set; }

        public virtual Dataset Dataset { get; set; }

      }
   
}