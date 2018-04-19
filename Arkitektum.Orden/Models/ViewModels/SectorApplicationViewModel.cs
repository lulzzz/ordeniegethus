using System.Collections.Generic;

namespace Arkitektum.Orden.Models.ViewModels
{

    public class SectorApplicationViewModel
    {
        public int ApplicationId { get;set;}
        public int SectorId { get; set; }
        public string SectorName { get; set; }

        internal static IEnumerable<SectorApplicationViewModel> Map(IEnumerable<Sector> sectors, int applicationId)
        {
            var viewModels = new List<SectorApplicationViewModel>();
            foreach (var sector in sectors)
            {
                viewModels.Add(new SectorApplicationViewModel { ApplicationId = applicationId, SectorId = sector.Id, SectorName = sector.Name });
            }
            return viewModels;
        }
    }
}
