using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Arkitektum.Orden.Models;

namespace Arkitektum.Orden.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Organization>().HasMany(o => o.Sectors)
                .WithOne(s => s.Organization)
                .HasForeignKey(s => s.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Organization>().HasMany(o => o.Users)
                .WithOne(oau => oau.Organization)
                .HasForeignKey(oau => oau.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationUser>().HasMany(au => au.Organizations)
                .WithOne(oau => oau.ApplicationUser)
                .HasForeignKey(oau => oau.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Organization>().HasMany(o => o.Applications)
                .WithOne(a => a.Organization)
                .HasForeignKey(a => a.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Sector>().HasMany(s => s.SectorApplications)
                .WithOne(sa => sa.Sector)
                .HasForeignKey(sa => sa.SectorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Application>().HasMany(app => app.SectorApplications)
                .WithOne(sa => sa.Application)
                .HasForeignKey(sa => sa.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Application>().HasMany(app => app.ApplicationSharedServices)
                .WithOne(ass => ass.Application)
                .HasForeignKey(ass => ass.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SharedService>().HasMany(sa => sa.ApplicationSharedServices)
                .WithOne(ass => ass.SharedService)
                .HasForeignKey(ass => ass.SharedServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Application>().HasMany(app => app.ApplicationDatasets)
                .WithOne(ad => ad.Application)
                .HasForeignKey(ad => ad.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Dataset>().HasMany(d => d.ApplicationDatasets)
                .WithOne(ad => ad.Dataset)
                .HasForeignKey(ad => ad.DatasetId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Application>().HasMany(app => app.ApplicationSupportedIntegrations)
                .WithOne(asi => asi.Application)
                .HasForeignKey(asi => asi.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Integration>().HasMany(i => i.ApplicationSupportedIntegrations)
                .WithOne(asi => asi.SupportedIntegration)
                .HasForeignKey(asi => asi.SupportedIntegrationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Application>().HasMany(app => app.ApplicationStandards)
                .WithOne(apps => apps.Application)
                .HasForeignKey(apps => apps.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Standard>().HasMany(s => s.ApplicationStandards)
                .WithOne(apps => apps.Standard)
                .HasForeignKey(apps => apps.StandardId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Vendor>().HasMany(v => v.Applications)
                .WithOne(app => app.Vendor)
                .HasForeignKey(app => app.VendorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationDataset>().HasKey("ApplicationId", "DatasetId");
            builder.Entity<ApplicationSharedService>().HasKey("ApplicationId", "SharedServiceId");
            builder.Entity<ApplicationStandard>().HasKey("ApplicationId", "StandardId");
            builder.Entity<ApplicationSupportedIntegration>().HasKey("ApplicationId", "SupportedIntegrationId");
            builder.Entity<OrganizationApplicationUser>().HasKey("OrganizationId", "ApplicationUserId");
            builder.Entity<SectorApplication>().HasKey("SectorId", "ApplicationId");
        }
    }
}
