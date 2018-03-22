namespace Arkitektum.Orden.Models
{
    public class ResourceLink
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public string ApplicationId { get; set; }

        public string DatasetContactPointsId { get; set; } 
        public string DatasetResourceLinkId { get; set; } 
        public string DatasetLawReferenceId { get; set; } 
    }
}