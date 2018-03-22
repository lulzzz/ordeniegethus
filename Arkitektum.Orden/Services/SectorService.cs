using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Arkitektum.Orden.Services
{
    public interface ISectorService
    {
        /// <summary>
        /// Returns sectors associated with the given organization.
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        Task<IEnumerable<Sector>> GetSectorsForOrganization(int organizationId);

        Task<Sector> GetAsync(int id);
        Task<Sector> Create(Sector sector);
        Task UpdateAsync(int id, Sector sectorToEdit);
    }

    public class SectorService : ISectorService
    {
        private readonly ApplicationDbContext _context;

        public SectorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sector>> GetSectorsForOrganization(int organizationId)
        {
            return await _context.Sector.Where(s => s.OrganizationId == organizationId)
                .Include(s => s.Organization)
                .Include(s => s.SectorApplications).ThenInclude(sa => sa.Application)
                .ToListAsync();
        }

        public async Task<Sector> GetAsync(int id)
        {
            return await _context.Sector
                .Include(s => s.Organization)
                .Include(s => s.SectorApplications).ThenInclude(sa => sa.Application)
                .SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Sector> Create(Sector sector)
        {
            _context.Add(sector);
            await _context.SaveChangesAsync();
            return sector;
        }

        public async Task UpdateAsync(int id, Sector sectorDataForEdit)
        {
            var sectorToEdit = await GetAsync(id);

            _context.Entry(sectorToEdit).CurrentValues.SetValues(sectorDataForEdit);

            await _context.SaveChangesAsync();
        }
    }
}