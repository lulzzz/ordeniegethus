using System.Threading.Tasks;
using Arkitektum.Orden.Models.ViewModels;
using Arkitektum.Orden.Services.Search;
using Microsoft.AspNetCore.Mvc;

namespace Arkitektum.Orden.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        public async Task<IActionResult> Index(string query)
        {
            var response = await _searchService.Search(query);

            var model = new SearchResultViewModel
            {
                NumberOfHits = response.Total,
                Items = response.Documents
            };
            return View(model);
        }
    }
}