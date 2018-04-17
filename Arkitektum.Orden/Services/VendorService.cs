using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arkitektum.Orden.Services
{
    public interface IVendorService
    {
        Task<List<Vendor>> GetAll();
    }

    public class VendorService : IVendorService
    {
        private readonly ApplicationDbContext context;

        public VendorService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Vendor>> GetAll()
        {
            return await context.Vendors.OrderBy(v => v.Name).ToListAsync();
        }
    }
}
