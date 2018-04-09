using System.Collections.Generic;
using Arkitektum.Orden.Services.Search;

namespace Arkitektum.Orden.Models.ViewModels
{
    public class SearchResultViewModel
    {
        public long NumberOfHits { get; set; }

        public IEnumerable<object> Items { get; set; }
    }


    public class SearchResultItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static IEnumerable<SearchResultItem> Map(IReadOnlyCollection<ApplicationDocument> documents)
        {
            var output = new List<SearchResultItem>();

            foreach (var doc in documents)
                output.Add(new SearchResultItem
                {
                    Id = doc.Id,
                    Name = doc.Name
                });

            return output;
        }
    }
}