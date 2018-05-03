namespace Arkitektum.Orden.Models
{
    public class ResourceLink
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public int? ApplicationId { get; set; }
        
        public int? DatasetResourceLinkId { get; set; }
        public virtual Dataset DatasetResourceLink { get; set; }

        public int? DatasetLawReferenceId { get; set; }
        public virtual Dataset DatasetLawReference { get; set; }

        }
    }
