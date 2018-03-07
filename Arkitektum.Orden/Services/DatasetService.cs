using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Microsoft.EntityFrameworkCore;

namespace Arkitektum.Orden.Services
{
    public interface IDatasetService
    {
        Task<Dataset> Get(int? id);
        Task<IEnumerable<Dataset>> GetAll();
        Task<IEnumerable<Dataset>> GetAllDatasetsForOrganisation(int orgId);
        Task<Dataset> Create(Dataset Dataset);
        Task SaveChanges();
        Task Delete(int id);
    }
    /// <summary>
    /// Handles operations on Dataset Entity
    /// </summary>
    public class DatasetService : IDatasetService
    {
        private readonly ApplicationDbContext _context;

        public DatasetService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Dataset>> GetAll()
        {
            return await _context.Dataset.ToListAsync();
        }

        public async Task<Dataset> Create(Dataset Dataset)
        {
            _context.Add(Dataset);
            await SaveChanges();
            return Dataset;
        }

        public async Task Delete(int id)
        {
            var Dataset = await _context.Dataset.SingleOrDefaultAsync(a => a.Id == id);
            _context.Dataset.Remove(Dataset);
            await SaveChanges();
        }

        public async Task<Dataset> Get(int? id)
        {
            return await _context.Dataset.SingleOrDefaultAsync(a => a.Id == id);
        }



        public async Task<IEnumerable<Dataset>> GetAllDatasetsForOrganisation(int orgId)
        {
            return await _context.Dataset.ToListAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

    }
}
