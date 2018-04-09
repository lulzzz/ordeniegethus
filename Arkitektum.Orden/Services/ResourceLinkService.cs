using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Microsoft.EntityFrameworkCore;

namespace Arkitektum.Orden.Services
{
    public interface IResourceLinkService
    {
        Task<ResourceLink> GetAsync(int id);
        Task<IEnumerable<ResourceLink>> GetResourceLinksForApplication(int applicationId);
        Task<IEnumerable<ResourceLink>> GetResourceLinksForDataset(int datasetId);
        Task<ResourceLink> Create(ResourceLink resourceLink);
        Task Delete(int id);
        Task UpdateAsync(ResourceLink updatedResourceLink);
       
    }

    public class ResourceLinkService : IResourceLinkService
    {
        private readonly ApplicationDbContext _context;

        public ResourceLinkService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResourceLink> GetAsync(int id)
        {
            return await _context.ResourceLink.SingleOrDefaultAsync(l => l.Id == id);
        }


        public async Task<IEnumerable<ResourceLink>> GetResourceLinksForApplication(int applicationId)
        {
            return await _context.ResourceLink.Where(l => l.ApplicationId == applicationId).ToListAsync();
        }

        public async Task<IEnumerable<ResourceLink>> GetResourceLinksForDataset(int datasetId)
        {
            return await _context.ResourceLink.Where(l => l.DatasetResourceLinkId == datasetId).ToListAsync();
        }

        public async Task<ResourceLink> Create(ResourceLink resourceLink)
        {
            _context.Add(resourceLink);
            await SaveChanges();
            return resourceLink;
        }


        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var resourceLink = await GetAsync(id);
            _context.ResourceLink.Remove(resourceLink);

            await SaveChanges();
        }

        public async Task UpdateAsync(ResourceLink updatedResourceLink)
        {
            var resourceLinkToEdit = await GetAsync(updatedResourceLink.Id);
            _context.Entry(resourceLinkToEdit).CurrentValues.SetValues(updatedResourceLink);

            await SaveChanges();
        }
    }
}
