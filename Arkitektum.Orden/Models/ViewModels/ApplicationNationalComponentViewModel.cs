using System.Collections.Generic;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class ApplicationNationalComponentViewModel : Mapper<>
    {
        public int ApplicationId { get; set; }
        public int NationalComponentId { get; set; }

        public Dictionary<string, List<IEnumerable<ApplicationListDetailViewModel>>> ApplicationsForNationalComponent { get; set; }
    }
}
