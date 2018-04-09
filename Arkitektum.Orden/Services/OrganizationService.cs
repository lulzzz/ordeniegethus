using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Microsoft.EntityFrameworkCore;

namespace Arkitektum.Orden.Services
{
    public interface IOrganizationService
    {
        Task<Organization> Get(int id);
        Task<IEnumerable<Organization>> GetAll();
        Task<Organization> Create(Organization organization);
        Task SaveChangesAsync();
        Task Delete(int id);
        Task<IEnumerable<Organization>> GetAllAdministrableByUser(string userId);
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
        public async Task<Organization> Get(int id)
        {
            return await _context.Organization.SingleOrDefaultAsync(m => m.Id == id);
        }

        /// <summary>
        ///     Retrieve all organizations
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Organization>> GetAll()
        {
            return await _context.Organization.ToListAsync();
        }

        /// <summary>
        ///     Creates a new organization, saves changes to the db context.
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        public async Task<Organization> Create(Organization organization)
        {
            _context.Add(organization);
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

        public async Task<IEnumerable<Organization>> GetAllAdministrableByUser(string userId)
        {
            ApplicationUser applicationUser = await _context.ApplicationUser.Where(u => u.Id == userId)
                .AsNoTracking()
                .Include(u => u.Organizations).ThenInclude(uo => uo.Organization)
                .FirstAsync();

            List<Organization> organizations = new List<Organization>();
            foreach (var membership in applicationUser.Organizations)
            {
                if (membership.Role == Roles.OrganizationAdmin)
                    organizations.Add(membership.Organization);
            }
            return organizations;
        }
    }
}