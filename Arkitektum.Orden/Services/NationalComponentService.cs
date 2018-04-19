using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Microsoft.EntityFrameworkCore;

namespace Arkitektum.Orden.Services
{
    public interface INationalComponentService
    {
        Task<IEnumerable<NationalComponent>> GetAll();
        Task<NationalComponent> Get(int id);
        Task<NationalComponent> Create(NationalComponent nationalComponent);
        Task SaveChangesAsync();
        Task Delete(int id);

        Task<List<NationalComponent>> GetComponentsForApplication(int applicationId);
        Task AddComponentToApplication(int nationalComponentId, int applicationId);
        Task RemoveComponentFromApplication(int nationalComponentId, int applicationId);
    }
    /// <summary>
    /// Handles operations on NationalComponent entity
    /// </summary>
    public class NationalComponentService : INationalComponentService
    {
        private readonly ApplicationDbContext _context;


        public NationalComponentService(ApplicationDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Retrieve all NationalComponents
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<NationalComponent>> GetAll()
        {
            return await _context.NationalComponent.ToListAsync();
        }

        /// <summary>
        /// Retrieve NationalComponent with given id or null if not found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<NationalComponent> Get(int id)
        {
            return  await _context.NationalComponent.SingleOrDefaultAsync(ss => ss.Id == id);
        }

        /// <summary>
        /// Create a new NationalComponent, save changes to db context
        /// </summary>
        /// <param name="nationalComponent"></param>
        /// <returns></returns>
        public async Task<NationalComponent> Create(NationalComponent nationalComponent)
        {
            _context.Add(nationalComponent);
            await SaveChangesAsync();
            return nationalComponent;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete NationalComponent with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  async Task Delete(int id)
        {
            var nationalComponent = await _context.NationalComponent.SingleOrDefaultAsync(ss => ss.Id == id);
            _context.NationalComponent.Remove(nationalComponent);
            await SaveChangesAsync();
        }

        public async Task<List<NationalComponent>> GetComponentsForApplication(int applicationId)
        {
             var components = await _context.ApplicationNationalComponents
                .Where(a => a.ApplicationId == applicationId)
                .Include(x => x.NationalComponent)
                .Select(x => x.NationalComponent).ToListAsync();

            return components;
        }

        public async Task AddComponentToApplication(int nationalComponentId, int applicationId)
        {
            var application = await GetApplication(applicationId);
            application.ApplicationNationalComponent.Add(
                new ApplicationNationalComponent { 
                    NationalComponentId = nationalComponentId,
                    ApplicationId = applicationId
                }
            );
            await SaveChangesAsync();
        }

        public async Task RemoveComponentFromApplication(int nationalComponentId, int applicationId)
        {
            var application = await GetApplication(applicationId);
            application.RemoveNationalComponent(nationalComponentId);
                         
            await SaveChangesAsync();
            
        }

        private async Task<Application> GetApplication(int applicationId)
        {
            return await _context.Application.Include(a => a.ApplicationNationalComponent)
                .SingleOrDefaultAsync(a => a.Id == applicationId);
        }
    }
}
