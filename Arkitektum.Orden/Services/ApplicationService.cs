using System;
using System.Collections;
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
        Task<Application> Get(int? id);
        Task<IEnumerable<Application>> GetAll();
        Task<IEnumerable<Application>> GetAllApplicationsForOrganisation(int orgId);
        Task<Application> Create(Application application);
        Task SaveChanges();
        Task Delete(int id);
    }
    /// <summary>
    /// Handles operations on Application Entity
    /// </summary>
    public class ApplicationService : IApplicationService
    {
        private readonly ApplicationDbContext _context;

        public ApplicationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Application>> GetAll()
        {
            return await _context.Application.Include(a => a.SystemOwner).Include(a => a.Organization).ToListAsync();
        }

        public async Task<Application> Create(Application application)
        {
            _context.Add(application);
            await SaveChanges();
            return application;
        }

        public async Task Delete(int id)
        {
            var application = await _context.Application.SingleOrDefaultAsync(a => a.Id == id);
            _context.Application.Remove(application);
            await SaveChanges();
        }

        public async Task<Application> Get(int? id)
        {
           return await _context.Application.Include(a => a.SystemOwner).Include(a => a.Organization).SingleOrDefaultAsync(a => a.Id == id);
        }

        
       
        public async Task<IEnumerable<Application>> GetAllApplicationsForOrganisation(int orgId)
        {
            return await _context.Application.Where(a => a.OrganizationId == orgId).ToListAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

    }
}
