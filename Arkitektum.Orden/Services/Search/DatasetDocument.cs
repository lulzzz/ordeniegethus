using Arkitektum.Orden.Models;

namespace Arkitektum.Orden.Services
{
    public class DatasetDocument
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Purpose { get; set; }


        public DatasetDocument()
        {
        }

        public DatasetDocument(Dataset dataset)
        {
            Id = dataset.Id;
            Name = dataset.Name;
            Description = dataset.Description;
            Purpose = dataset.Purpose;
        }
    }
}