using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
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
        Task<List<Sector>> GetSectorsForOrganization(int organizationId);

        Task<Sector> GetAsync(int id);
    }

    public class SectorService : ISectorService
    {
        private readonly ApplicationDbContext _context;

        public SectorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Sector>> GetSectorsForOrganization(int organizationId)
        {
            return await _context.Sector.Where(s => s.OrganizationId == organizationId).ToListAsync();
        }

        public async Task<Sector> GetAsync(int id)
        {
            return await _context.Sector
                .Include(s => s.Organization)
                .Include(s => s.SectorApplications).ThenInclude(sa => sa.Application)
                .SingleOrDefaultAsync(s => s.Id == id);
        }
    }
}