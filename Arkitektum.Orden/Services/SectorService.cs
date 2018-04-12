using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
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
        Task<IEnumerable<SectorInformationViewModel>> GetSectorsWithApplicationsForOrganization(int organizationId);

        Task<IEnumerable<Application>> GetApplicationsForSector(int id, int organizationId);
        Task<Sector> GetSectorWithNoTracking(int id);
        Task<Sector> GetSectorWithTracking(int id);
        Task<Sector> Create(Sector sector);
        Task Update(int id, Sector sectorToEdit);
        Task<IEnumerable<Sector>> GetAll();
    }

    public class SectorService : ISectorService
    {
        private readonly ApplicationDbContext _context;

        public SectorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SectorInformationViewModel>> GetSectorsWithApplicationsForOrganization(int organizationId)
        {

            var sectors = from sa in _context.SectorApplication
                          join app in _context.Application on sa.ApplicationId equals app.Id
                          where app.OrganizationId == organizationId
                          group sa by sa.SectorId
                into grp
                          select new SectorInformationViewModel()
                          {
                              SectorId = grp.Key,
                              ApplicationCount = grp.Select(x => x.ApplicationId).Distinct().Count(),
                              Name = _context.Sector.SingleOrDefault(s => s.Id == grp.Key).Name
                          };


            return await sectors.ToListAsync();

        }

        public async Task<IEnumerable<Application>> GetApplicationsForSector(int id, int organizationId)
        {
            var applications = from sa in _context.SectorApplication
                               join a in _context.Application on sa.ApplicationId equals a.Id
                               where sa.SectorId == id
                               where a.OrganizationId == organizationId
                               select a;

            return await applications.ToListAsync();


        }

        public async Task<Sector> GetSectorWithNoTracking(int id)
        {
            return await _context.Sector.AsNoTracking()
                .SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Sector> GetSectorWithTracking(int id)
        {
            return await _context.Sector
                .SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Sector> Create(Sector sector)
        {
            _context.Add(sector);
            await _context.SaveChangesAsync();
            return sector;
        }

        public async Task Update(int id, Sector sectorDataForEdit)
        {
           
            var sectorToEdit = await GetSectorWithTracking(id);

            _context.Entry(sectorToEdit).CurrentValues.SetValues(sectorDataForEdit);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Sector>> GetAll()
        {
            return await _context.Sector.ToListAsync();
        }
    }
}