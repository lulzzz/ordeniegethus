using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Microsoft.EntityFrameworkCore;

namespace Arkitektum.Orden.Services
{
    public interface ISharedServiceService
    {
        Task<IEnumerable<SharedService>> GetAll();
        Task<SharedService> Get(int id);
        Task<SharedService> Create(SharedService sharedService);
        Task SaveChangesAsync();
        Task Delete(int id);

    }
    /// <summary>
    /// Handles operations on SharedService entity
    /// </summary>
    public class SharedServiceService : ISharedServiceService
    {
        private readonly ApplicationDbContext _context;


        public SharedServiceService(ApplicationDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Retrieve all SharedServices
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SharedService>> GetAll()
        {
            return await _context.SharedService.ToListAsync();
        }

        /// <summary>
        /// Retrieve SharedService with given id or null if not found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SharedService> Get(int id)
        {
            return  await _context.SharedService.SingleOrDefaultAsync(ss => ss.Id == id);
        }

        /// <summary>
        /// Create a new SharedService, save changes to db context
        /// </summary>
        /// <param name="sharedService"></param>
        /// <returns></returns>
        public async Task<SharedService> Create(SharedService sharedService)
        {
            _context.Add(sharedService);
            await SaveChangesAsync();
            return sharedService;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete SharedService with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  async Task Delete(int id)
        {
            var sharedService = await _context.SharedService.SingleOrDefaultAsync(ss => ss.Id == id);
            _context.SharedService.Remove(sharedService);
            await SaveChangesAsync();
        }
    }
}
