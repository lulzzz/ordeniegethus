using Arkitektum.Orden.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Arkitektum.Orden.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Application> Application { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<NationalComponent> NationalComponent { get; set; }
        public DbSet<ApplicationDataset> ApplicationDataset { get; set; }
        public DbSet<ApplicationDataset> ResourceLink { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().HasMany(au => au.OrganizationAdministrators)
                .WithOne(oa => oa.ApplicationUser)
                .HasForeignKey(oa => oa.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Organization>().HasMany(o => o.OrganizationAdministrators)
                .WithOne(oa => oa.Organization)
                .HasForeignKey(oa => oa.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<Organization>().HasOne(o => o.DcatCatalog).WithOne(cat => cat.Organization).HasForeignKey<DcatCatalog>(cat => cat.OrganizationId);
            builder.Entity<Dataset>().HasMany(d => d.ContactPoints).WithOne(cp => cp.DatasetConnectionPoints)
                .HasForeignKey(cp => cp.DatasetContactPointsId);
            builder.Entity<Dataset>().HasMany(d => d.LawReferences).WithOne(lr => lr.DatasetLawReference)
                .HasForeignKey(lr => lr.DatasetLawReferenceId);
            builder.Entity<Dataset>().HasMany(d => d.ResourceLinks).WithOne(rl => rl.DatasetResourceLink)
                .HasForeignKey(rl => rl.DatasetResourceLinkId);



            builder.Entity<ApplicationDataset>().HasKey("ApplicationId", "DatasetId");
            builder.Entity<ApplicationNationalComponent>().HasKey("ApplicationId", "NationalComponentId");
            builder.Entity<ApplicationStandard>().HasKey("ApplicationId", "StandardId");
            builder.Entity<ApplicationSupportedIntegration>().HasKey("ApplicationId", "SupportedIntegrationId");
            builder.Entity<OrganizationApplicationUser>().HasKey("OrganizationId", "ApplicationUserId", "Role");
            builder.Entity<OrganizationAdministrators>().HasKey("OrganizationId", "ApplicationUserId");
            builder.Entity<SectorApplication>().HasKey("SectorId", "ApplicationId");
          
            
        }

        public DbSet<Arkitektum.Orden.Models.Sector> Sector { get; set; }

        public DbSet<Arkitektum.Orden.Models.Dataset> Dataset { get; set; }
    }
}