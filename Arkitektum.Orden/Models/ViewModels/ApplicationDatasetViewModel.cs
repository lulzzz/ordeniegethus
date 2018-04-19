using System;
using System.Collections.Generic;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class ApplicationDatasetViewModel
    {
        public int ApplicationId { get; set; }
        public int DatasetId { get; set; }
        public string DatasetName { get; set; }

        internal static List<ApplicationDatasetViewModel> Map(IEnumerable<Dataset> datasets, int applicationId)
        {
            var viewModels = new List<ApplicationDatasetViewModel>();
            foreach (var dataset in datasets)
            {
                viewModels.Add(new ApplicationDatasetViewModel { 
                    ApplicationId = applicationId,
                    DatasetId = dataset.Id,
                    DatasetName = dataset.Name
                });
            }
            return viewModels;
        }
    }
}
