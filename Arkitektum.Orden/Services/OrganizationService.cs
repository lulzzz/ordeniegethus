using System.Collections.Generic;
using System.Threading.Tasks;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Microsoft.EntityFrameworkCore;

namespace Arkitektum.Orden.Services
{
    public interface IOrganizationService
    {
        Task<Organization> GetOrganization(int id);
        Task<IEnumerable<Organization>> GetOrganizations();
        Task<Organization> CreateOrganization(Organization organization);
        Task<Organization> UpdateOrganization(Organization organization);
        Task SaveChangesAsync();
        Task Delete(int id);
    }

    /// <summary>
    ///     Handles operations on the Organization entity
    /// </summary>
    public class OrganizationService : IOrganizationService
    {
        private readonly ApplicationDbContext _context;

        public OrganizationService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        ///     Retrieve organization with given id or null if not found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Organization> GetOrganization(int id)
        {
            return await _context.Organization.SingleOrDefaultAsync(m => m.Id == id);
        }

        /// <summary>
        ///     Retrieve all organizations
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Organization>> GetOrganizations()
        {
            return await _context.Organization.ToListAsync();
        }

        /// <summary>
        ///     Creates a new organization, saves changes to the db context.
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        public async Task<Organization> CreateOrganization(Organization organization)
        {
            _context.Add(organization);
            await SaveChangesAsync();
            return organization;
        }

        /// <summary>
        /// Updates an organization. All 
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        public async Task<Organization> UpdateOrganization(Organization organization)
        {
            _context.Update(organization);
            await SaveChangesAsync();
            return organization;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var organization = await _context.Organization.SingleOrDefaultAsync(m => m.Id == id);
            _context.Organization.Remove(organization);
            await SaveChangesAsync();
        }
    }
}