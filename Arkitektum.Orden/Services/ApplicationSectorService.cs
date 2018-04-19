using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arkitektum.Orden.Services
{
    public interface IApplicationSectorService
    {
        Task<IEnumerable<Sector>> GetSectorsForApplication(int applicationId);
        Task CreateApplicationSector(int applicationId, int sectorId);
        Task DeleteApplicationSector(int applicationId, int sectorId);
    }

    public class ApplicationSectorService : IApplicationSectorService
    {
        private ApplicationDbContext _context;

        public ApplicationSectorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateApplicationSector(int applicationId, int sectorId)
        {
            var application = await GetApplication(applicationId);
            application.SectorApplications.Add(new SectorApplication { ApplicationId = applicationId, SectorId = sectorId});
            await _context.SaveChangesAsync();
        }

        public async Task DeleteApplicationSector(int applicationId, int sectorId)
        {
            var application = await GetApplication(applicationId);
            application.RemoveSector(sectorId);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Sector>> GetSectorsForApplication(int applicationId)
        {
            return await _context.SectorApplication
                .Where(sa => sa.ApplicationId == applicationId)
                .Select(sa => sa.Sector)
                .OrderBy(sa => sa.Name)
                .ToListAsync();
        }

        private async Task<Application> GetApplication(int applicationId)
        {
            return await _context.Application.Include(a => a.SectorApplications)
                .SingleOrDefaultAsync(a => a.Id == applicationId);
        }
    }
}
