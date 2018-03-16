using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Arkitektum.Orden.Services
{
    public interface IFieldService
    {
        Task<Field> Get(int id);
        Task<IEnumerable<Field>> GetAll();
        Task<Field> Create(JsonResult field);
        Task SaveChangesAsync();
        Task Delete();
    }
    public class FieldService : IFieldService
    {
        public readonly ApplicationDbContext _context;

        public FieldService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Field> Get(int id)
        {
            return await _context.Field.SingleOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<Field>> GetAll()
        {
           return await _context.Field.ToListAsync();
        }

        public Task<Field> Create(JsonResult field)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task Delete()
        {
            throw new NotImplementedException();
        }
    }
}
