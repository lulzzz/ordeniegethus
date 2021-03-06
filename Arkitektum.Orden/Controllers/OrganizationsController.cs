﻿using System.Net;
using System.Threading.Tasks;
using Arkitektum.Orden.Models;
using Arkitektum.Orden.Models.ViewModels;
using Arkitektum.Orden.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Arkitektum.Orden.Controllers
{
    [Authorize]
    public class OrganizationsController : BaseController
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationsController(IOrganizationService organizationService, ISecurityService securityService) : base(securityService)
        {
            _organizationService = organizationService;
        }

        // GET: Organizations
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole(Roles.Admin))
            {
                var organizations = await _organizationService.GetAll();
                return View(new OrganizationViewModel().MapToEnumerable(organizations));
            }

            if (CurrentUser().IsOrganizationAdminForOrganization(CurrentOrganization()))
            {
                var organizations = await _organizationService.GetAllAdministrableByUser(CurrentUser().Id());
                return View(new OrganizationViewModel().MapToEnumerable(organizations));
            }
        
            return Forbid();
        }

        // GET: Organizations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var organization = await _organizationService.Get(id.Value);
            if (organization == null) return NotFound();

            return View(new OrganizationViewModel().Map(organization));
        }

        // GET: Organizations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Organizations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Create([Bind("Id,Name,OrganizationNumber")] OrganizationViewModel organization)
        {
            if (ModelState.IsValid)
            {
                var createOrganization =
                    await _organizationService.Create(new OrganizationViewModel().Map(organization));
                return RedirectToAction(nameof(Index));
            }

            return View(organization);
        }

        // GET: Organizations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var organization = await _organizationService.Get(id.Value);
            if (organization == null) return NotFound();
            return View(new OrganizationViewModel().Map(organization));
        }

        // POST: Organizations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null) return NotFound();

            var organizationToUpdate = await _organizationService.Get(id.Value);
            if (await TryUpdateModelAsync(
                organizationToUpdate,
                "",
                s => s.Name, s => s.OrganizationNumber))
                try
                {
                    await _organizationService.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                                                 "Try again, and if the problem persists, " +
                                                 "see your system administrator.");
                }

            return View(new OrganizationViewModel().Map(organizationToUpdate));
        }

        // GET: Organizations/Delete/5
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var organization = await _organizationService.Get(id.Value);
            if (organization == null) return NotFound();

            return View(organization);
        }

        // POST: Organizations/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _organizationService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}