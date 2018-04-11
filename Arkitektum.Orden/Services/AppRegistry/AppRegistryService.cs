﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Arkitektum.Orden.Services.AppRegistry
{
    public interface IAppRegistry
    {
        Task<List<CommonApplication>> GetApplicationsAsync();
        Task SubmitApplication(int applicationId);
    }

    public class AppRegistryService : IAppRegistry
    {
        private ApplicationDbContext _context;

        public AppRegistryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<CommonApplication>> GetApplicationsAsync()
        {
            return _context.CommonApplications.OrderBy(a => a.Name).ToListAsync();
        }

        public async Task SubmitApplication(int applicationId)
        {
            //TODO: Implement copying of data from input application to CommonApplication
        }
    }
}