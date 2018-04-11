using System.Collections.Generic;

namespace Arkitektum.Orden.Models
{
    public class CommonDataset
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Purpose { get; set; }
        public bool HasPersonalData { get; set; }
        public bool HasSensitivePersonalData { get; set; }
        public bool HasMasterData { get; set; }
        public virtual List<CommonDatasetField> Fields { get; set; }

        public CommonDataset()
        {
            Fields = new List<CommonDatasetField>();
        }
    }
}