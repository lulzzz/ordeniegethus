﻿using System;
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
        private readonly AppSettings _appSettings;

        public ElasticSearchService(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task<ISearchResponse<object>> Search(string query)
        {
            if (!_appSettings.SearchEngineEnabled)
                return new SearchResponse<object>();

            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex("application");
            var client = new ElasticClient(settings);

            ISearchResponse<object> result = await client.SearchAsync<object>(s => s
                .AllIndices()
                .Type(Types.Type(new List<TypeName>(){Types.Type<ApplicationDocument>(), Types.Type<DatasetDocument>()}))
                .Query(q => q.MatchAll()));

            return result;

        } 

    }
}
