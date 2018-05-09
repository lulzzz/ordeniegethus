using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arkitektum.Orden.Services
{
    public interface IApplicationDatasetService
    {
        Task<IEnumerable<ApplicationDataset>> GetDatasetsForApplication(int applicationId);
        Task CreateApplicationDataset(int applicationId, int datasetId, string roleName);
        Task DeleteApplicationDataset(int applicationId, int datasetId);
    }

    public class ApplicationDatasetService : IApplicationDatasetService
    {
        private ApplicationDbContext _context;

        public ApplicationDatasetService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateApplicationDataset(int applicationId, int datasetId, string roleName)
        {
            var application = await GetApplication(applicationId);
            application.ApplicationDatasets.Add(new ApplicationDataset { ApplicationId = applicationId, DatasetId = datasetId, RoleName = roleName});
            await _context.SaveChangesAsync();
        }

        public async Task DeleteApplicationDataset(int applicationId, int datasetId)
        {
            var application = await GetApplication(applicationId);
            application.RemoveDataset(datasetId);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ApplicationDataset>> GetDatasetsForApplication(int applicationId)
        {
            return await _context.ApplicationDataset
                .Where(sa => sa.ApplicationId == applicationId)
                .Include(sa => sa.Dataset)
                .OrderBy(sa => sa.Dataset.Name)
                .ToListAsync();
        }

        private async Task<Application> GetApplication(int applicationId)
        {
            return await _context.Application.Include(a => a.ApplicationDatasets)
                .SingleOrDefaultAsync(a => a.Id == applicationId);
        }
    }
}
