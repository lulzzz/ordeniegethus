namespace Arkitektum.Orden.Models
{
    public class Field
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPersonalData { get; set; }

        public bool IsSensitivePersonalData { get; set; }

        public Dataset Dataset { get; set; }

        public int? DatasetId { get; set; }
    }
}