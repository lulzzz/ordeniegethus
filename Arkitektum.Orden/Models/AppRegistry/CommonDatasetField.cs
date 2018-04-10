namespace Arkitektum.Orden.Models
{
    public class CommonDatasetField
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPersonalData { get; set; }

        public bool IsSensitivePersonalData { get; set; }

        public int? CommonDatasetId { get; set; }
        public virtual CommonDataset CommonDataset { get; set; }
    }
}