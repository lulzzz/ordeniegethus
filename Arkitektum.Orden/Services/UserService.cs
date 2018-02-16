using System.Collections.Generic;
using System.Threading.Tasks;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Microsoft.EntityFrameworkCore;

namespace Arkitektum.Orden.Services
{
public interface IUserService
    {
        Task<ApplicationUser> Get(string id);
        Task<IEnumerable<ApplicationUser>> GetAll();
        Task<ApplicationUser> Create(ApplicationUser user);
        Task SaveChangesAsync();
        Task Delete(string id);
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
        ///     Retrieve User with given id or null if not found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApplicationUser> Get(string id)
        {
            return await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
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
            var User = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            _context.ApplicationUser.Remove(User);
            await SaveChangesAsync();
        }
    }
}