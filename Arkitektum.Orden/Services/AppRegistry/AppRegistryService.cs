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
        Task SubmitApplication(int applicationId, int submittedOrganizationId, string submittedUserId);
        Task<Application> CreateApplicationForOrganization(int commonApplicationId, string versionNumber, int organizationId);
        Task<CommonApplication> Get(int id);
    }

    public class AppRegistryService : IAppRegistry
    {
        private ApplicationDbContext _context;
        private readonly ISecurityService _securityService;

        public AppRegistryService(ApplicationDbContext context, ISecurityService securityService)
        {
            _context = context;
            _securityService = securityService;
        }

        public async Task<Application> CreateApplicationForOrganization(int commonApplicationId, string versionNumber, int organizationId)
        {
            CommonApplication commonApplication = await Get(commonApplicationId);

            Application application = commonApplication.CreateApplicationForOrganization(organizationId, versionNumber);
            _context.Application.Add(application);
            await _context.SaveChangesAsync(_securityService.GetCurrentUser().FullName());

            return application;
        }

        public async Task<CommonApplication> Get(int id)
        {
            return await _context.CommonApplications
                .Include(a => a.Versions).ThenInclude(v => v.SupportedStandards).ThenInclude(s => s.Standard)
                .Include(a => a.Versions).ThenInclude(v => v.SupportedNationalComponents).ThenInclude(c => c.NationalComponent)
                .Include(a => a.CommonDatasets).ThenInclude(d => d.Fields)
                .Include(a => a.Vendor)
                .Include(a => a.SubmittedByOrganization)
                .Include(a => a.SubmittedByUser)
                .SingleOrDefaultAsync(a => a.Id == id);
        }

        public Task<List<CommonApplication>> GetApplicationsAsync()
        {
            return _context.CommonApplications
                .Include(a => a.Vendor)
                .Include(a => a.Versions)
                .Include(a => a.SubmittedByOrganization)
                .Include(a => a.SubmittedByUser)
                .OrderBy(a => a.Name)
                .ToListAsync();
        }

        public async Task SubmitApplication(int applicationId, int submittedOrganizationId, string submittedUserId)
        {
            var application = await _context.Application
                .SingleOrDefaultAsync(a => a.Id == applicationId);
            
            CommonApplication commonApplication = application.CopyToCommonApplication();
            commonApplication.SubmittedByOrganizationId = submittedOrganizationId;
            commonApplication.SubmittedByUserId = submittedUserId;

            _context.CommonApplications.Add(commonApplication);
            await _context.SaveChangesAsync();
        }
    }
}