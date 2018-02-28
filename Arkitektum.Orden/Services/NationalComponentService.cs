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
    }
}
