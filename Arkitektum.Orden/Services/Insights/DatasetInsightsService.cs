using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arkitektum.Orden.Models.ViewModels;

namespace Arkitektum.Orden.Services.Insights
{
    public interface IDatasetInsightsService
    {
        Task<List<Dataset>> DatasetsWithPrivacyConcerns(int currentOrganizationId);
        Task<DatasetsOverviewViewModel> GetDatasetWithPublishingStatus(int currentOrganizationId);
    }

    public class DatasetInsightsService : IDatasetInsightsService
    {
        private readonly ApplicationDbContext _context;
       
        public DatasetInsightsService(ApplicationDbContext context)
        {
            _context = context;
      
        }

        public async Task<List<Dataset>> DatasetsWithPrivacyConcerns(int currentOrganizationId)
        {
            return await _context.Dataset
                .Where(d => d.OrganizationId == currentOrganizationId)
                .Where(d => d.HasPersonalData || d.HasSensitivePersonalData)
                .Include(d => d.Fields)
                .ToListAsync();

        }

        public async Task<DatasetsOverviewViewModel> GetDatasetWithPublishingStatus(int currentOrganizationId)
        {
            DatasetsOverviewViewModel model = new DatasetsOverviewViewModel();
            var datasets = await _context.Dataset.Where(d => d.OrganizationId == currentOrganizationId).ToListAsync();

            model.PublishedDatasets =
                new DatasetViewModel().MapToEnumerable(datasets.Where(d => d.PublishedToSharedDataCatalog != null));
            model.NotPublishedDatasets =
                new DatasetViewModel().MapToEnumerable(datasets.Where(d => d.PublishedToSharedDataCatalog == null));
               
            return model;
        }
    }
}
