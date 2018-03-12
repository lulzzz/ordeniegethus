using System;
using System.Threading.Tasks;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Services.Search;
using Nest;

namespace Arkitektum.Orden.Services
{
    public interface ISearchIndexingService
    {
        Task AddToIndex(Application application);
        Task AddToIndex(Dataset dataset);
    }

    public class ElasticSearchIndexingService : ISearchIndexingService
    {
        public async Task AddToIndex(Application application)
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex("application");
            var client = new ElasticClient(settings);

            await client.IndexDocumentAsync(new ApplicationDocument(application));
        }
        
        public async Task AddToIndex(Dataset dataset)
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex("dataset");
            var client = new ElasticClient(settings);

            await client.IndexDocumentAsync(new DatasetDocument(dataset));
        }
    }
}