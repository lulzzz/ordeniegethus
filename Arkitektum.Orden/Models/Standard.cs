using System.Collections.Generic;

namespace Arkitektum.Orden.Models
{
    public class Standard
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<ApplicationStandard> ApplicationStandards { get; set; }
    }
}