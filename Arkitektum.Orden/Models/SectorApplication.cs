using System.ComponentModel.DataAnnotations;

namespace Arkitektum.Orden.Models
{
    public class SectorApplication
    {
        public int SectorId { get; set; }

        public int ApplicationId { get; set; }

        public virtual Sector Sector { get; set; }

        public virtual Application Application { get; set; }
    }
}