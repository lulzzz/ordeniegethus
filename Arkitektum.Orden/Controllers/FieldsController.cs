using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Services;

namespace Arkitektum.Orden.Controllers
{
    public class FieldsController : Controller
    {
        private readonly IFieldService _fieldService;

        public FieldsController(IFieldService fieldService)
        {
            _fieldService = fieldService;
        }

        // GET: Fields
        public async Task<JsonResult> Index()
        {
            //GetDatasetId
            var fields = await _fieldService.GetAll();
            return Json(fields);
        }

        // GET: Fields/Details/5
        public async Task<JsonResult> Details(int? id)
        {
            if (id == null)
            {
                return new JsonResult(NotFound());
            }

            var field = await _fieldService.Get(id.Value);

            if (field == null)
            {
                return new JsonResult(NotFound());
            }

            return Json(field);



            //    // GET: Fields/Create
            //    public IActionResult Create()
            //    {
            //        ViewData["DatasetId"] = new SelectList(_context.Dataset, "Id", "Id");
            //        return View();
            //    }

            //    // POST: Fields/Create
            //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
            //    [HttpPost]
            //    [ValidateAntiForgeryToken]
            //    public async Task<IActionResult> Create([Bind("Id,Name,Description,IsPersonalData,IsSensitivePersonalData,DatasetId")] Field @field)
            //    {
            //        if (ModelState.IsValid)
            //        {
            //            _context.Add(@field);
            //            await _context.SaveChangesAsync();
            //            return RedirectToAction(nameof(Index));
            //        }
            //        ViewData["DatasetId"] = new SelectList(_context.Dataset, "Id", "Id", @field.DatasetId);
            //        return View(@field);
            //    }

            //    // GET: Fields/Edit/5
            //    public async Task<IActionResult> Edit(int? id)
            //    {
            //        if (id == null)
            //        {
            //            return NotFound();
            //        }

            //        var @field = await _context.Field.SingleOrDefaultAsync(m => m.Id == id);
            //        if (@field == null)
            //        {
            //            return NotFound();
            //        }
            //        ViewData["DatasetId"] = new SelectList(_context.Dataset, "Id", "Id", @field.DatasetId);
            //        return View(@field);
            //    }

            //    // POST: Fields/Edit/5
            //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
            //    [HttpPost]
            //    [ValidateAntiForgeryToken]
            //    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,IsPersonalData,IsSensitivePersonalData,DatasetId")] Field @field)
            //    {
            //        if (id != @field.Id)
            //        {
            //            return NotFound();
            //        }

            //        if (ModelState.IsValid)
            //        {
            //            try
            //            {
            //                _context.Update(@field);
            //                await _context.SaveChangesAsync();
            //            }
            //            catch (DbUpdateConcurrencyException)
            //            {
            //                if (!FieldExists(@field.Id))
            //                {
            //                    return NotFound();
            //                }
            //                else
            //                {
            //                    throw;
            //                }
            //            }
            //            return RedirectToAction(nameof(Index));
            //        }
            //        ViewData["DatasetId"] = new SelectList(_context.Dataset, "Id", "Id", @field.DatasetId);
            //        return View(@field);
            //    }

            //    // GET: Fields/Delete/5
            //    public async Task<IActionResult> Delete(int? id)
            //    {
            //        if (id == null)
            //        {
            //            return NotFound();
            //        }

            //        var @field = await _context.Field
            //            .Include(@ => @.Dataset)
            //            .SingleOrDefaultAsync(m => m.Id == id);
            //        if (@field == null)
            //        {
            //            return NotFound();
            //        }

            //        return View(@field);
            //    }

            //    // POST: Fields/Delete/5
            //    [HttpPost, ActionName("Delete")]
            //    [ValidateAntiForgeryToken]
            //    public async Task<IActionResult> DeleteConfirmed(int id)
            //    {
            //        var @field = await _context.Field.SingleOrDefaultAsync(m => m.Id == id);
            //        _context.Field.Remove(@field);
            //        await _context.SaveChangesAsync();
            //        return RedirectToAction(nameof(Index));
            //    }

            //    private bool FieldExists(int id)
            //    {
            //        return _context.Field.Any(e => e.Id == id);
            //    }
            //}
        }
    }
}
