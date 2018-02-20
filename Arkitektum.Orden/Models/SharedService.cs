using System.Collections.Generic;

namespace Arkitektum.Orden.Models
{
    /// <summary>
    /// Felleskomponent e.g. Matrikkel, Id-porten
    /// </summary>
    public class SharedService
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<ApplicationSharedService> ApplicationSharedServices { get; set; }

    }
}