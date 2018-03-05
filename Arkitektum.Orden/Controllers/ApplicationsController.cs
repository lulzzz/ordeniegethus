using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.ViewModels;
using Arkitektum.Orden.Services;
using Microsoft.AspNetCore.Authorization;

namespace Arkitektum.Orden.Controllers
{
    [Authorize]
    public class ApplicationsController : BaseController
    {
        private readonly IApplicationService _applicationService;
        private readonly IUserService _userService;
    
        public ApplicationsController(IApplicationService applicationService, IUserService userService, ISecurityService securityService) : base(securityService)
        {
            _applicationService = applicationService;
            _userService = userService;
        }


        // GET: Applications
        public async Task<IActionResult> Index()
        {            
            SimpleOrganization currentOrganization = CurrentOrganization();
            var applications = await _applicationService.GetAllApplicationsForOrganisation(currentOrganization.Id);
            return View(new ApplicationViewModel().MapToEnumerable(applications));
        }

        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _applicationService.Get(id.Value);
            if (application == null)
            {
                return NotFound();
            }

            return View(new ApplicationViewModel().Map(application));
        }

        // GET: Applications/Create
        public async Task<IActionResult> Create()
        {
            var model = new ApplicationViewModel();
            model.OrganizationId = CurrentOrganizationId().Value;

            model.AvailableSuperUsers = new List<SelectListItem>();

            foreach (var applicationUser in await _userService.GetAll())
            {
                model.AvailableSuperUsers.Add(new SelectListItem()
                {
                    Text = applicationUser.FullName,
                    Value = applicationUser.Id
                });
            }

            return View(model);

        }

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,AnnualFee,Vendor,SystemOwner,Version,OrganizationId")] ApplicationViewModel application)
        {
            if (ModelState.IsValid)
            {
                var createApplication = await _applicationService.Create(new ApplicationViewModel().Map(application));
                return RedirectToAction(nameof(Index));
            }

            return View(application);
        }

        // GET: Applications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _applicationService.Get(id);
            if (application == null)
            {
                return NotFound();
            }

            var model = new ApplicationViewModel().Map(application);

            model.AvailableSuperUsers = new List<SelectListItem>();

            foreach (var applicationUser in await _userService.GetAll())
            {
                model.AvailableSuperUsers.Add(new SelectListItem()
                {
                    Text = applicationUser.FullName,
                    Value = applicationUser.Id
                });
            }
            
            return View(model);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Version,AnnualFee,InitialCost,HostingLocation,NumberOfUsers,Vendor,OrganizationId")] Application application)
        {
            if (id != application.Id)
            {
                return NotFound();
            }

            var appliactionToUpdate = await _applicationService.Get(id);
            if (await TryUpdateModelAsync(
                appliactionToUpdate,
                "",
                a => a.Name, a => a.Vendor, a => a.AnnualFee, a => a.HostingLocation,
                a => a.InitialCost, a => a.NumberOfUsers, a => a.SystemOwnerId,
                a => a.Version))
                try
                {
                    await _applicationService.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                                                 "Try again, and if the problem persists, " +
                                                 "see your system administrator.");
                }

            return View(new ApplicationViewModel().Map(appliactionToUpdate));
        }

        // GET: Applications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            
            var applicationToDelete = await _applicationService.Get(id);
            if (applicationToDelete == null) return NotFound();
            
            return View(applicationToDelete);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _applicationService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

       }
}
