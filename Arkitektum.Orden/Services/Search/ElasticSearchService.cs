using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace Arkitektum.Orden.Services.Search
{
    public interface ISearchService
    {
        Task<ISearchResponse<ApplicationDocument>> Search(string query);
    }

    public class ElasticSearchService : ISearchService
    {
        public async Task<ISearchResponse<ApplicationDocument>> Search(string query)
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex("application");
            var client = new ElasticClient(settings);

            return await client.SearchAsync<ApplicationDocument>(s => s.Query(q => q.MatchAll()));
        } 

    }
}
