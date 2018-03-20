using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace Arkitektum.Orden.Services.Search
{
    public interface ISearchService
    {
        Task<ISearchResponse<object>> Search(string query);
    }

    public class ElasticSearchService : ISearchService
    {
        public async Task<ISearchResponse<object>> Search(string query)
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex("application");
            var client = new ElasticClient(settings);

            ISearchResponse<object> result = await client.SearchAsync<object>(s => s
                .AllIndices()
                .Type(Types.Type<ApplicationDocument>())
                .Query(q => q.MatchAll()));

            return result;

        } 

    }
}
