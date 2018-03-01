using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Arkitektum.Orden.Services
{
public interface IUserService
    {
        Task<ApplicationUser> Get(string id);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<IEnumerable<ApplicationUser>> GetAll();
        Task<ApplicationUser> Create(ApplicationUser user);
        Task SaveChangesAsync();
        Task Delete(string id);
        Task AddOrganizationRolesAsync(List<OrganizationApplicationUser> organizationMembership);
    }

    /// <summary>
    ///     Handles operations on the User entity
    /// </summary>
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        ///     Retrieve User with given id or null if not found. 
        /// Includes roles and associated organizations.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApplicationUser> Get(string id)
        {
            return await _context.ApplicationUser
                .Include(u => u.Organizations)
                .ThenInclude(o => o.Organization)
                .SingleOrDefaultAsync(au => au.Id == id);
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _context.ApplicationUser
                .Include(u => u.Organizations)
                .ThenInclude(o => o.Organization)
                .SingleOrDefaultAsync(au =>
                    string.Equals(au.Email, email, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        ///     Retrieve all Users
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ApplicationUser>> GetAll()
        {
            return await _context.ApplicationUser.ToListAsync();
        }

        /// <summary>
        ///     Creates a new User, saves changes to the db context.
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public async Task<ApplicationUser> Create(ApplicationUser User)
        {
            _context.Add(User);
            await SaveChangesAsync();
            return User;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var user = await _context.ApplicationUser.SingleOrDefaultAsync(au => au.Id == id);
            _context.ApplicationUser.Remove(user);
            await SaveChangesAsync();
        }

        public async Task AddOrganizationRolesAsync(List<OrganizationApplicationUser> organizationMemberships)
        {
            //_context.Attach(organizationMemberships.First().ApplicationUser);

            foreach (var item in organizationMemberships)
            {
                //bool isDetached = _context.Entry(item.Organization).State == EntityState.Detached;
                //if (isDetached)
                //    _context.Attach(item.Organization);
                
                _context.Add(item);
            }
            await SaveChangesAsync();
        }
    }
}