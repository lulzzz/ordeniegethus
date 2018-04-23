using System.Collections.Generic;
using System.Threading.Tasks;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Arkitektum.Orden.Services.AppRegistry
{
    public interface IAppRegistry
    {
        Task<List<CommonApplication>> GetApplicationsAsync();
        Task SubmitApplication(int applicationId);
        Task<Application> CreateApplicationForOrganization(int commonApplicationId, string versionNumber, int organizationId);
    }

    public class AppRegistryService : IAppRegistry
    {
        private ApplicationDbContext _context;
        private readonly ISecurityService _securityService;

        public AppRegistryService(ApplicationDbContext context, ISecurityService securityService)
        {
            _context = context;
            _securityService = securityService;
        }

        public async Task<Application> CreateApplicationForOrganization(int commonApplicationId, string versionNumber, int organizationId)
        {
            CommonApplication commonApplication = await _context.CommonApplications
                .Include(a => a.Versions).ThenInclude(v => v.SupportedStandards)
                .Include(a => a.Versions).ThenInclude(v => v.SupportedNationalComponents)
                .Include(a => a.CommonDatasets).ThenInclude(d => d.Fields)
                .SingleOrDefaultAsync(a => a.Id == commonApplicationId);

            Application application = commonApplication.CreateApplicationForOrganization(organizationId, versionNumber);
            _context.Application.Add(application);
            await _context.SaveChangesAsync(_securityService.GetCurrentUser().FullName());

            return application;
        }

        public Task<List<CommonApplication>> GetApplicationsAsync()
        {
            return _context.CommonApplications
                .Include(a => a.Vendor)
                .Include(a => a.Versions)
                .OrderBy(a => a.Name)
                .ToListAsync();
        }

        public async Task SubmitApplication(int applicationId)
        {
            var application = await _context.Application
                .SingleOrDefaultAsync(a => a.Id == applicationId);
            
            CommonApplication commonApplication = application.CopyToCommonApplication();

            _context.CommonApplications.Add(commonApplication);
            await _context.SaveChangesAsync();
        }
    }
}