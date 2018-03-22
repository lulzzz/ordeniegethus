using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arkitektum.Orden.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Arkitektum.Orden.Controllers
{
    public class ResourceLinksController : Controller
    {
 

        // GET: RecourceLinks
        public IActionResult GetApplicationLinks(int applicationId = 1)
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

    //    // GET: RecourceLinks/Create
    //    public ActionResult Create()
    //    {
    //        return View();
    //    }

    //    // POST: RecourceLinks/Create
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Create(IFormCollection collection)
    //    {
    //        try
    //        {
    //            // TODO: Add insert logic here

    //            return RedirectToAction(nameof(Index));
    //        }
    //        catch
    //        {
    //            return View();
    //        }
    //    }

    //    // GET: RecourceLinks/Edit/5
    //    public ActionResult Edit(int id)
    //    {
    //        return View();
    //    }

    //    // POST: RecourceLinks/Edit/5
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Edit(int id, IFormCollection collection)
    //    {
    //        try
    //        {
    //            // TODO: Add update logic here

    //            return RedirectToAction(nameof(Index));
    //        }
    //        catch
    //        {
    //            return View();
    //        }
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