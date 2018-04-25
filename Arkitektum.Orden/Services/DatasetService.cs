using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace Arkitektum.Orden.Services
{
    public interface IDatasetService
    {
        Task<Dataset> GetAsync(int id);
        Task<IEnumerable<Dataset>> GetAll();
        Task<IEnumerable<Dataset>> GetAllDatasetsForOrganisation(int orgId);
        Task<Dataset> Create(Dataset Dataset);
        Task SaveChanges();
        Task Delete(int id);
        Task<IEnumerable<Dataset>> GetAllDatasetsForOrganization(int currentOrganizationId);
        Task UpdateAsync(int id, Dataset updatedDataset);
        Task<int> GetDatasetsCountForOrganization(int currentOrganizationId);
    }
    /// <summary>
    /// Handles operations on Dataset Entity
    /// </summary>
    public class DatasetService : IDatasetService
    {
        private readonly ApplicationDbContext _context;
        private readonly ISecurityService _securityService;
        private readonly ISearchIndexingService _searchIndexingService;


        public DatasetService(ApplicationDbContext context, ISecurityService securityService, ISearchIndexingService searchIndexingService)
        {
            _context = context;
            _securityService = securityService;
            _searchIndexingService = searchIndexingService;
        }

        public async Task<IEnumerable<Dataset>> GetAll()
        {
            return await _context.Dataset.ToListAsync();
        }

        public async Task<Dataset> Create(Dataset dataset)
        {
            _context.Add(dataset);
            await SaveChanges();
            await _searchIndexingService.AddToIndex(dataset);
            return dataset;
        }

        public async Task Delete(int id)
        {
            var Dataset = await _context.Dataset.SingleOrDefaultAsync(a => a.Id == id);
            _context.Dataset.Remove(Dataset);
            await SaveChanges();
        }

        public async Task<IEnumerable<Dataset>> GetAllDatasetsForOrganization(int currentOrganizationId)
        {
            var datasets = _context.Dataset
                .Where(d => d.OrganizationId == currentOrganizationId)
                .Include(d => d.ApplicationDatasets).ThenInclude(ad => ad.Application)
                .ToListAsync();

            return await datasets;
        }

        public async Task UpdateAsync(int id, Dataset updatedDataset)
        {
            var currentDataset = await GetAsync(id);

            _context.Entry(currentDataset).CurrentValues.SetValues(updatedDataset);

            if (updatedDataset.ApplicationDatasets != null)
            {
                currentDataset.UpdateApplicationRelation(updatedDataset.ApplicationDatasets);
            }
           
            await SaveChanges();
        }

        public async Task<int> GetDatasetsCountForOrganization(int currentOrganizationId)
        {
            IEnumerable<Dataset> datasets = await GetAllDatasetsForOrganization(currentOrganizationId);

            int numberOfdatasets = datasets.Count();

            return numberOfdatasets;
        }


        public async Task<Dataset> GetAsync(int id)
        {
            return await _context.Dataset
                .Include(d => d.Fields)
                .Include(d => d.ApplicationDatasets).ThenInclude(ad => ad.Application)
                .SingleOrDefaultAsync(d => d.Id == id);

        }



        public async Task<IEnumerable<Dataset>> GetAllDatasetsForOrganisation(int orgId)
        {
            return await _context.Dataset.ToListAsync();
        }

        public async Task SaveChanges()
        {
            string username = _securityService.GetCurrentUser().FullName();
            await _context.SaveChangesAsync(username);
        }

  

    }
}
