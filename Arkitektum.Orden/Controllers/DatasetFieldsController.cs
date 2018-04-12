using System.Collections.Generic;
using System.Threading.Tasks;
using Arkitektum.Orden.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arkitektum.Orden.Controllers
{
    public class DatasetFieldsController : Controller
    {

        [Route("/datasets/{id}/fields")]
        public async Task<IActionResult> GetFields([FromQuery] int id)
        {
            return Json(new List<DatasetFieldViewModel>() {
                new DatasetFieldViewModel
                {
                    Id = 1,
                    Name = "Personnummer",
                    Description = "Personnummer eller D-nummer",
                    IsPersonalData = true
                },
                new DatasetFieldViewModel
                {
                    Id = 2,
                    Name = "Navn",
                    Description = "Fullt navn",
                    IsPersonalData = true
                },
                new DatasetFieldViewModel
                {
                    Id = 3,
                    Name = "Fagforeningsmedlem",
                    IsPersonalData = true,
                    IsSensitivePersonalData = true
                },
            });
        }

        [HttpPost]
        [Route("/datasets/{id}/fields")]
        public async Task<IActionResult> Create([FromQuery] int id, [FromBody] DatasetFieldViewModel model)
        {
            model.Id = 42;
            var result = Json(model);
            result.StatusCode = StatusCodes.Status201Created;
            return result;
        }

        [HttpPut]
        [Route("/datasets/{id}/fields")]
        public async Task<IActionResult> Update([FromQuery] int id, [FromBody] DatasetFieldViewModel model)
        {
            return Json(model);
        }

        [HttpDelete]
        [Route("/datasets/{id}/fields/{fieldId}")]
        public async Task<IActionResult> Delete([FromQuery] int id, [FromQuery] int fieldId)
        {
            return StatusCode(StatusCodes.Status204NoContent);
        }

    }
}