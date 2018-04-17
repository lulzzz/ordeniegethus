using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Microsoft.EntityFrameworkCore;

namespace Arkitektum.Orden.Services
{
    public interface IApplicationService
    {
        Task<Application> GetAsync(int id);
        Task<IEnumerable<Application>> GetAll();
        Task<IEnumerable<Application>> GetAllApplicationsForOrganization(int orgId);
        Task<Application> Create(Application application);
        Task SaveChanges();
        Task Delete(int id);
        Task UpdateAsync(int id, Application updatedApplication);
        Task<int> GetApplicationCountForOrganization(int currentOrganizationId);
        Task<IEnumerable<Application>> GetApplicationsWithFilter(int currentOrganizationId, int sectorId, int nationalComponentId);
    }
    /// <summary>
    /// Handles operations on Dataset Entity
    /// </summary>
    public class ApplicationService : IApplicationService
    {
        private readonly ApplicationDbContext _context;
        private readonly ISecurityService _securityService;
        private readonly ISearchIndexingService _searchIndexingService;

        public ApplicationService(ApplicationDbContext context, ISecurityService securityService, ISearchIndexingService searchIndexingService)
        {
            _context = context;
            _securityService = securityService;
            _searchIndexingService = searchIndexingService;
        }

        public async Task<IEnumerable<Application>> GetAll()
        {
            return await _context.Application.Include(a => a.SystemOwner).Include(a => a.Organization).ToListAsync();
        }

        public async Task<Application> Create(Application application)
        {
            _context.Add(application);
            await SaveChanges();
            await _searchIndexingService.AddToIndex(application);
            return application;
        }

        public async Task Delete(int id)
        {
            var application = await _context.Application.SingleOrDefaultAsync(a => a.Id == id);
            _context.Application.Remove(application);
            await SaveChanges();
        }

        public async Task UpdateAsync(int id, Application updatedApplication)
        {
            var currentApplication = await GetAsync(id);
            
            _context.Entry(currentApplication).CurrentValues.SetValues(updatedApplication);
            
            currentApplication.UpdateSectorRelations(updatedApplication.SectorApplications);

            currentApplication.UpdateNationalComponentsRelations(updatedApplication.ApplicationNationalComponent);
            
            _context.Entry(currentApplication).State = EntityState.Modified;

            await SaveChanges();

            await _searchIndexingService.AddToIndex(currentApplication);
        }

        public async Task<int> GetApplicationCountForOrganization(int currentOrganizationId)
        {
            return await _context.Application.Where(a => a.OrganizationId == currentOrganizationId).CountAsync();
        }

        public List<Application> GetAllApplicationsBySector(int sectorId, IEnumerable<Application> applications)
        {
         
            List<Application> filteredApplications = new List<Application>();

            foreach (var application in applications)
            {
                if (application.SectorApplications != null && application.SectorApplications.Any())
                {
                    foreach (var sectorApplication in application.SectorApplications)
                    {
                        if (sectorApplication.SectorId == sectorId)
                        {
                            filteredApplications.Add(application);
                        }
                    }
                }
               
            }

            return filteredApplications;
        }

        public async Task<IEnumerable<Application>> GetApplicationsWithFilter(int currentOrganizationId, int sectorId,
            int nationalComponentId)
        {
            var applications = await GetAllApplicationsForOrganization(currentOrganizationId);
            if (sectorId != 0)
            {
                applications = GetAllApplicationsBySector(sectorId, applications);
            }
            else if (nationalComponentId != 0)
            {
                applications = GetAllApplicationsByNationalComponent(nationalComponentId, applications);
            }

            return applications;
        }

        private IEnumerable<Application> GetAllApplicationsByNationalComponent(int nationalComponentId, IEnumerable<Application> applications)
        {
            List<Application> applicationsByNationalComponent = new List<Application>();
            foreach (var application in applications)
            {
                foreach (var applicationNationalComponent in application.ApplicationNationalComponent)
                {
                    if (applicationNationalComponent.NationalComponentId == nationalComponentId)
                    {
                        applicationsByNationalComponent.Add(application);
                    }
                }
            }

            return applicationsByNationalComponent;
        }

        public async Task<Application> GetAsync(int id)
        {
           return await _context.Application
               .Include(a => a.SystemOwner)
               .Include(a => a.Organization)
               .Include(a => a.ApplicationNationalComponent).ThenInclude(anc => anc.NationalComponent)
               .Include(a => a.SectorApplications).ThenInclude(sa => sa.Sector)
               .Include(a => a.ApplicationDatasets).ThenInclude(ad => ad.Dataset)
               .Include(a => a.Vendor)
               .SingleOrDefaultAsync(a => a.Id == id);
        }
       
        public async Task<IEnumerable<Application>> GetAllApplicationsForOrganization(int orgId)
        {
            return await _context.Application.Where(a => a.OrganizationId == orgId).ToListAsync();
        }

        public async Task SaveChanges()
        {
            string username = _securityService.GetCurrentUser().FullName();
            await _context.SaveChangesAsync(username);
        }

    }
}
