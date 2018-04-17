using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.ViewModels;
using Arkitektum.Orden.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Arkitektum.Orden.Controllers
{
    public class DatasetFieldsController : Controller
    {
        private readonly IFieldService _fieldService;

        public DatasetFieldsController(IFieldService fieldService)
        {
            _fieldService = fieldService;
        }

        [Route("/datasets/{id}/fields")]
        public async Task<IActionResult> GetFields(int id)
        {
            IEnumerable<Field> fields = await _fieldService.GetAllFieldsForDataset(id);
            IEnumerable<DatasetFieldViewModel> viewModels = new DatasetFieldViewModel().MapToEnumerable(fields);

            return Json(viewModels);

        }

        [HttpPost]
        [Route("/datasets/{id}/fields")]
        public async Task<IActionResult> CreateFieldForDataset(int id, [FromBody] DatasetFieldViewModel model)
        {
            try
            {
                var field = new DatasetFieldViewModel().Map(model, id);
                var fieldToCreate = await _fieldService.Create(field);
                var result = Json(new DatasetFieldViewModel().Map(fieldToCreate));
                result.StatusCode = StatusCodes.Status201Created;

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPut]
        [Route("/datasets/{id}/fields")]
        public async Task<IActionResult> Update(int id, [FromBody] DatasetFieldViewModel datasetFieldModel)
        {
            try
            {
                var fieldToUpdate = new DatasetFieldViewModel().Map(datasetFieldModel, id);

                var field = await _fieldService.Update(fieldToUpdate.Id, fieldToUpdate);

                return Json(new DatasetFieldViewModel().Map(field));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("/datasets/{id}/fields/{fieldId}")]
        public async Task<IActionResult> Delete(int id,  int fieldId)
        {
            if (id == 0 || fieldId == 0)
            {
                return BadRequest();
            }

            await _fieldService.Delete(fieldId);

            return Ok();

        }

    }
}