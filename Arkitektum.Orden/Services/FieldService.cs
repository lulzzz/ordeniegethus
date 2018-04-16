using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Arkitektum.Orden.Services
{
    public interface IFieldService
    {
        Task<IEnumerable<Field>> GetAllFieldsForDataset(int datasetId);
        Task<Field> Get(int id);
        Task<Field> Create(Field field);
        Task Update(int id, Field newFieldData);
        Task Delete(int fieldId);
    }

    public class FieldService : IFieldService
    {
        private readonly ApplicationDbContext _context;

        public FieldService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Field>> GetAllFieldsForDataset(int datasetId)
        {
            return await _context.Fields.Where(f => f.DatasetId == datasetId).ToListAsync();
        }

        public async Task<Field> Get(int id)
        {
          return await _context.Fields.SingleOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Field> Create(Field field)
        {
            _context.Add(field);
            await SaveChanges();
            return field;
        }

        public async Task Update(int id, Field newFieldData)
        {
            var fieldToEdit = await Get(id);
            _context.Entry(fieldToEdit).CurrentValues.SetValues(newFieldData);

            await SaveChanges();
        }

        public async Task Delete(int fieldId)
        {
            var fieldToDelete = await Get(fieldId);
            _context.Fields.Remove(fieldToDelete);

            await SaveChanges();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}