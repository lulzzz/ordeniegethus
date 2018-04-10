using System.Collections.Generic;

namespace Arkitektum.Orden.Models
{
    public class CommonApplicationVersion
    {
        public int Id { get; set; }
        public string VersionNumber { get; set; }

        public List<CommonApplicationVersionNationalComponent> SupportedNationalComponents { get; set;}
        public List<CommonApplicationVersionStandard> SupportedStandards { get; set; }
    }
}