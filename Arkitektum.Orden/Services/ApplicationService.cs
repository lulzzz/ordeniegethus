﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Nest;
using Remotion.Linq.Parsing.Structure.IntermediateModel;
using Serilog;

namespace Arkitektum.Orden.Services
{
    public interface IApplicationService
    {
        Task<Application> GetAsync(int id);
        Task<IEnumerable<Application>> GetAll();
        Task<IEnumerable<Application>> GetAllApplicationsForOrganization(int orgId);
        Task<Application> Create(Application application);
        Task SaveChanges();
        Task Delete(int id);
        Task UpdateAsync(int id, Application updatedApplication);
        Task<int> GetApplicationCountForOrganization(int currentOrganizationId);
        Task<IEnumerable<Application>> GetApplicationsWithFilter(int currentOrganizationId, int sectorId, int nationalComponentId, string sortingOrder);
        InsightsViewModel GetApplicationsGroupedByNationalComponents(int currentOrganizationId);
        Task<List<Application>> GetApplicationsForUser(string currentUser, int currentOrganizationId);
    }
    /// <summary>
    /// Handles operations on Dataset Entity
    /// </summary>
    public class ApplicationService : IApplicationService
    {
        private static readonly ILogger Log = Serilog.Log.ForContext(MethodBase.GetCurrentMethod().DeclaringType);
        
        private readonly ApplicationDbContext _context;
        private readonly ISecurityService _securityService;
        private readonly ISearchIndexingService _searchIndexingService;

        public ApplicationService(ApplicationDbContext context, ISecurityService securityService, ISearchIndexingService searchIndexingService)
        {
            _context = context;
            _securityService = securityService;
            _searchIndexingService = searchIndexingService;
        }

        public async Task<IEnumerable<Application>> GetAll()
        {
            return await _context.Application.Include(a => a.SystemOwner).Include(a => a.Organization).ToListAsync();
        }

        public async Task<Application> Create(Application application)
        {
            Log.Information("Create new application {application} for organization {organization}", application.Name, application.OrganizationId);
            
            _context.Add(application);
            await SaveChanges();
            await _searchIndexingService.AddToIndex(application);
            return application;
        }

        public async Task Delete(int id)
        {
            var application = await _context.Application.SingleOrDefaultAsync(a => a.Id == id);
            
            Log.Information("Deleting {application} for organization {organization}", application.Name, application.OrganizationId);
            
            _context.Application.Remove(application);
            await SaveChanges();
        }

        public async Task UpdateAsync(int id, Application updatedApplication)
        {
            var currentApplication = await GetAsync(id);

            _context.Entry(currentApplication).CurrentValues.SetValues(updatedApplication);

            currentApplication.Agreement = updatedApplication.Agreement;
            currentApplication.UpdateSectorRelations(updatedApplication.SectorApplications);

            currentApplication.UpdateNationalComponentsRelations(updatedApplication.ApplicationNationalComponent);

            _context.Entry(currentApplication).State = EntityState.Modified;

            await SaveChanges();

            await _searchIndexingService.AddToIndex(currentApplication);
        }

        public async Task<int> GetApplicationCountForOrganization(int currentOrganizationId)
        {
            return await _context.Application.Where(a => a.OrganizationId == currentOrganizationId).CountAsync();
        }

        public List<Application> GetAllApplicationsBySector(int sectorId, IEnumerable<Application> applications)
        {

            List<Application> filteredApplications = new List<Application>();

            foreach (var application in applications)
            {
                if (application.SectorApplications != null && application.SectorApplications.Any())
                {
                    foreach (var sectorApplication in application.SectorApplications)
                    {
                        if (sectorApplication.SectorId == sectorId)
                        {
                            filteredApplications.Add(application);
                        }
                    }
                }

            }

            return filteredApplications;
        }

        public async Task<IEnumerable<Application>> GetApplicationsWithFilter(int currentOrganizationId, int sectorId,
            int nationalComponentId, string sortingOrder)
        {
            var applications = await GetAllApplicationsForOrganization(currentOrganizationId);
            if (sectorId != 0)
            {
                applications = GetAllApplicationsBySector(sectorId, applications);
            }
            else if (nationalComponentId != 0)
            {
                applications = GetAllApplicationsByNationalComponent(nationalComponentId, applications);
            }
            else if (sortingOrder != null && sortingOrder.Any())
            {
                applications = SortApplicationsByPrice(applications, sortingOrder);
            }

            return applications;
        }

        private IEnumerable<Application> SortApplicationsByPrice(IEnumerable<Application> applications, string sortingOrder)
        {
            IEnumerable<Application> sortedApplications = new List<Application>();

            sortedApplications = sortingOrder.Equals("desc") ? applications.OrderByDescending(a => a.AnnualFee) : applications.OrderBy(a => a.AnnualFee);

            return sortedApplications;
        }


        private IEnumerable<Application> GetAllApplicationsByNationalComponent(int nationalComponentId, IEnumerable<Application> applications)
        {
            List<Application> applicationsByNationalComponent = new List<Application>();
            foreach (var application in applications)
            {
                foreach (var applicationNationalComponent in application.ApplicationNationalComponent)
                {
                    if (applicationNationalComponent.NationalComponentId == nationalComponentId)
                    {
                        applicationsByNationalComponent.Add(application);
                    }
                }
            }

            return applicationsByNationalComponent;
        }

        public InsightsViewModel GetApplicationsGroupedByNationalComponents(int currentOrganizationId)
        {
            var models = _context.Application.Where(a => a.OrganizationId == currentOrganizationId)
                .SelectMany(a => a.ApplicationNationalComponent)
                .GroupBy(anc => anc.NationalComponentId)
                .Select(g => new
                {
                    Name = g.First().NationalComponent.Name,
                    Applications = g.Select(m => new ApplicationListDetailViewModel
                    {
                        AnnualFee = m.Application.DecimalToString(m.Application.AnnualFee),
                        Id = m.Application.Id,
                        Name = m.Application.Name,
                        SystemOwner = m.Application.SystemOwner.FullName,
                        Vendor = m.Application.Vendor.Name,
                        Version = m.Application.Version
                    })

                });

           InsightsViewModel viewModel = new InsightsViewModel();

            Dictionary<string, IEnumerable<ApplicationListDetailViewModel>> nationalComponentsWithApplications = new Dictionary<string, IEnumerable<ApplicationListDetailViewModel>>();
          

            foreach (var model in models)
            {
                nationalComponentsWithApplications.Add(
                        model.Name,
                        model.Applications
                        );
                      
            }

            viewModel.ApplicationsForNationalComponent = nationalComponentsWithApplications;

            return viewModel;
        }

        public async Task<List<Application>> GetApplicationsForUser(string currentUserId, int currentOrganizationId)
        {
            return await _context.Application
                .Where(a => a.SystemOwnerId == currentUserId && a.OrganizationId == currentOrganizationId)
                .Include(a => a.SystemOwner)
                .OrderBy(a => a.Name)
                .ToListAsync();
        }


        public async Task<Application> GetAsync(int id)
        {
            return await _context.Application
                .Include(a => a.SystemOwner)
                .Include(a => a.Organization)
                .Include(a => a.ApplicationNationalComponent).ThenInclude(anc => anc.NationalComponent)
                .Include(a => a.ApplicationStandards).ThenInclude(s => s.Standard)
                .Include(a => a.SectorApplications).ThenInclude(sa => sa.Sector)
                .Include(a => a.ApplicationDatasets).ThenInclude(ad => ad.Dataset)
                .Include(a => a.Vendor)
                .SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Application>> GetAllApplicationsForOrganization(int orgId)
        {
           var applications = await _context.Application
                .Include(a => a.SectorApplications)
                .ThenInclude(sa => sa.Sector)
                .Include(a => a.ApplicationNationalComponent)
                .ThenInclude(anc => anc.NationalComponent)
                .Include(a => a.Vendor)
                .Where(a => a.OrganizationId == orgId).OrderBy(a => a.Name)
                .ToListAsync();

            return applications;
        }

        public async Task SaveChanges()
        {
            string username = _securityService.GetCurrentUser().FullName();
            await _context.SaveChangesAsync(username);
        }

    }
}
