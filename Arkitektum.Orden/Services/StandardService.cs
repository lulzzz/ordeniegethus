using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Microsoft.EntityFrameworkCore;

namespace Arkitektum.Orden.Services
{
    public interface IStandardService
    {
        Task<IEnumerable<Standard>> All();

        Task AddStandardToApplication(ApplicationStandard relation);
        
        Task RemoveStandardFromApplication(ApplicationStandard relation);
    }

    public class StandardService : IStandardService
    {
        private ApplicationDbContext _context;
        private readonly ISecurityService _securityService;

        public StandardService(ApplicationDbContext context, ISecurityService securityService)
        {
            _context = context;
            _securityService = securityService;
        }

        public async Task<IEnumerable<Standard>> All()
        {
            return await _context.Standards.OrderBy(s => s.Name).ToListAsync();
        }

        public async Task AddStandardToApplication(ApplicationStandard relation)
        {
            var application = await GetApplication(relation.ApplicationId);
            
            application.ApplicationStandards.Add(relation);

            await SaveChanges();
        }

        public async Task RemoveStandardFromApplication(ApplicationStandard relation)
        {
            var application = await GetApplication(relation.ApplicationId);

            application.RemoveApplicationStandard(relation.StandardId);
            
            await SaveChanges();
        }

        private async Task SaveChanges()
        {
            await _context.SaveChangesAsync(_securityService.GetCurrentUser().FullName());
        }

        private async Task<Application> GetApplication(int applicationId)
        {
            Application application = await _context.Application
                .Include(a => a.ApplicationStandards)
                .SingleOrDefaultAsync(a => a.Id == applicationId);
            return application;
        }

    }
}