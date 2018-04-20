using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arkitektum.Orden.Services.Insights
{
    public interface IDatasetInsightsService
    {
        Task<List<Dataset>> DatasetsWithPrivacyConcerns();
    }

    public class DatasetInsightsService : IDatasetInsightsService
    {
        private readonly ApplicationDbContext _context;

        public DatasetInsightsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Dataset>> DatasetsWithPrivacyConcerns()
        {
            return await _context.Dataset
                .Where(d => d.HasPersonalData || d.HasSensitivePersonalData)
                .Include(d => d.Fields)
                .ToListAsync();
        }
    }
}
