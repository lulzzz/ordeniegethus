using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;

namespace Arkitektum.Orden.Services
{
    public interface IResourceLinkService
    {
        Task<ResourceLink> GetAsync(int id);
        Task<IEnumerable<ResourceLink>> GetAll();
        Task<IEnumerable<ResourceLink>> GetResourceLinksForApplication(int applicationId);
        Task<IEnumerable<ResourceLink>> GetResourceLinksForDataset(int datasetId);
        Task<ResourceLink> Create(ResourceLink resourceLink);
        Task SaveChanges();
        Task Delete(int id);
        Task UpdateAsync(int id, ResourceLink updatedResourceLink);
       
    }

    public class ResourceLinkService : IResourceLinkService
    {
        private readonly ApplicationDbContext _context;

        public ResourceLinkService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<ResourceLink> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ResourceLink>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ResourceLink>> GetResourceLinksForApplication(int applicationId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ResourceLink>> GetResourceLinksForDataset(int datasetId)
        {
            throw new NotImplementedException();
        }


        public async Task<ResourceLink> Create(ResourceLink resourceLink)
        {
            _context.Add(resourceLink);
            await SaveChanges();
            return resourceLink;
        }


        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(int id, ResourceLink updatedResourceLink)
        {
            var resourceLinkToEdit = GetAsync(id);
            _context.Entry(resourceLinkToEdit).CurrentValues.SetValues(updatedResourceLink);

            await _context.SaveChangesAsync();
        }
    }
}
