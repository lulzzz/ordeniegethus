using System.Collections.Generic;

namespace Arkitektum.Orden.Models
{
    public class CommonApplicationVersion
    {
        public int Id { get; set; }
        public string VersionNumber { get; set; }

        public virtual List<CommonApplicationVersionNationalComponent> SupportedNationalComponents { get; set;} = new List<CommonApplicationVersionNationalComponent>();
        public virtual List<CommonApplicationVersionStandard> SupportedStandards { get; set; } = new List<CommonApplicationVersionStandard>();

        public int CommonApplicationId { get; set; }
    }
}