using System.ComponentModel.DataAnnotations;

namespace Arkitektum.Orden.Models
{
    public class SectorApplication
    {
        public int SectorId { get; set; }

        public int ApplicationId { get; set; }

        public Sector Sector { get; set; }

        public Application Application { get; set; }
    }
}