using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.ViewModels;
using Arkitektum.Orden.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Arkitektum.Orden.Controllers
{
    [Route("/dataset/metadata")]
    public class DatasetMetadataController : BaseController
    {
        private readonly IDatasetService _datasetService;
        private readonly IConceptService _conceptService;

        public DatasetMetadataController(ISecurityService securityService, IDatasetService datasetService, IConceptService conceptService) : base(securityService)
        {
            _datasetService = datasetService;
            _conceptService = conceptService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var model = new DatasetMetadataViewModel();

            model.AvailableConcepts = new MultiSelectList(_conceptService.GetConcepts(), "Code", "Name");

            var dataset = await _datasetService.GetAsync(id);

            return Json(model.Map(dataset));
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DatasetMetadataViewModel model)
        {
            var dataset = new Dataset();
            dataset.Id = id;

            if (ModelState.IsValid)
            {
                var fieldName = model.FindOutChangedFieldName(model);
                dataset = await _datasetService.UpdateMetadataAsync(id, new DatasetMetadataViewModel().Map(model), fieldName);
            }

            return Json(new DatasetMetadataViewModel().Map(dataset));
        }



    }
}