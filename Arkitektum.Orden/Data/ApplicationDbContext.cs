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
                
            builder.Entity<ApplicationDataset>().HasKey("ApplicationId", "DatasetId");
            builder.Entity<ApplicationSharedService>().HasKey("ApplicationId", "SharedServiceId");
            builder.Entity<ApplicationStandard>().HasKey("ApplicationId", "StandardId");
            builder.Entity<ApplicationSupportedIntegration>().HasKey("ApplicationId", "SupportedIntegrationId");
            builder.Entity<OrganizationApplicationUser>().HasKey("OrganizationId", "ApplicationUserId");
            builder.Entity<SectorApplication>().HasKey("SectorId", "ApplicationId");
        }

        public DbSet<Arkitektum.Orden.Models.Organization> Organization { get; set; }
    }
}
