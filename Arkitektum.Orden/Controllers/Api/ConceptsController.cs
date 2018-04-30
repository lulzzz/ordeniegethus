using Arkitektum.Orden.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Arkitektum.Orden.Controllers.Api
{
    [Route("/api/concepts")]
    [Authorize]
    public class ConceptsController : Controller
    {
        private readonly IConceptService _conceptService;

        public ConceptsController(IConceptService conceptService)
        {
            _conceptService = conceptService;
        }


        [HttpGet("")]
        public IActionResult Index()
        {
            var concepts = _conceptService.GetConcepts();
            return Json(concepts);
        }
    }
}