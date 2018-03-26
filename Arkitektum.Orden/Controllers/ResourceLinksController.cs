using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.ViewModels;
using Arkitektum.Orden.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Json.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Arkitektum.Orden.Controllers
{
    public class ResourceLinksController : Controller
    {

        private readonly IResourceLinkService _resourseLinkService;
        private readonly ApplicationDbContext _context;

        public ResourceLinksController(IResourceLinkService resourseLinkService, ApplicationDbContext context)
        {
            _resourseLinkService = resourseLinkService;
            _context = context;
        }

        // GET: RecourceLinks for application
        public async Task<IActionResult> GetApplicationLinks(int applicationId = 1)
        {
            var data = new List<ResourceLink>()
            {
                new ResourceLink()
                {
                    Description = "LinkDescription",
                    Url = "wwww.arkitektum.no"
                },
                new ResourceLink()
                {
                    Description = "Description2",
                    Url = "www.vg.no"
                }
            };
            return Json(data);
        }

        // GET: RecourceLinks for dataset
        public IActionResult GetDatasetLinks(int datasetId = 1)
        {
            var data = new List<ResourceLink>()
            {
                new ResourceLink()
                {
                    Description = "LinkDescription",
                    Url = "wwww.arkitektum.no"
                },
                new ResourceLink()
                {
                    Description = "Description2",
                    Url = "www.vg.no"
                }
            };
            return Json(data);
        }


        // POST: RecourceLinks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Url")] ResourceLink resourceLink, int applicationId)
        {
            if (ModelState.IsValid)
            {
                var resourceLinkToCreate = await _resourseLinkService.Create(new ResourceLinkViewModel().Map(resourceLink, applicationId));
                return new CreatedResult("", resourceLinkToCreate);
            }
            return new BadRequestObjectResult(resourceLink);

        }

        // POST: RecourceLinks/Edit/5
        [HttpPut("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Id,Description,Url")] ResourceLinkViewModel resourceLink)
        {

            if (resourceLink.ApplicationId != id)
            {
                return NotFound();
            }

            await _resourseLinkService.UpdateAsync(id, resourceLink.Map(resourceLink));
            return Created("", resourceLink);
        }




        //// GET: RecourceLinks/Edit/5
        //public ActionResult Edit(JsonResult jsonResult)
        //{
        //    return View();
        //}


        //    // GET: RecourceLinks
        //    public ActionResult GetDatasetLinks()
        //    {
        //        return View();
        //    }



        //    GET: RecourceLinks/Details/5
        //    public ActionResult Details(int id)
        //    {
        //        return View();
        //    }




        //    // GET: RecourceLinks/Delete/5
        //    public ActionResult Delete(int id)
        //    {
        //        return View();
        //    }

        //    // POST: RecourceLinks/Delete/5
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Delete(int id, IFormCollection collection)
        //    {
        //        try
        //        {
        //            // TODO: Add delete logic here

        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }
    }
}