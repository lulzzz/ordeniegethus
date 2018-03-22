﻿// <auto-generated />
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Arkitektum.Orden.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180322080445_SectorOrganizationConnectionIsRemoved")]
    partial class SectorOrganizationConnectionIsRemoved
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Arkitektum.Orden.Models.AccessRightComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccessRightCommentField");

                    b.Property<int?>("DatasetId");

                    b.HasKey("Id");

                    b.HasIndex("DatasetId");

                    b.ToTable("AccessRightComment");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.Application", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("AnnualFee");

                    b.Property<string>("HostingLocation");

                    b.Property<string>("HostingVendor");

                    b.Property<decimal>("InitialCost");

                    b.Property<string>("Name");

                    b.Property<int>("NumberOfUsers");

                    b.Property<int?>("OrganizationId");

                    b.Property<string>("SystemOwnerId");

                    b.Property<string>("Vendor");

                    b.Property<string>("Version");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("SystemOwnerId");

                    b.ToTable("Application");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.ApplicationDataset", b =>
                {
                    b.Property<int>("ApplicationId");

                    b.Property<int>("DatasetId");

                    b.HasKey("ApplicationId", "DatasetId");

                    b.HasIndex("DatasetId");

                    b.ToTable("ApplicationDataset");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.ApplicationNationalComponent", b =>
                {
                    b.Property<int>("ApplicationId");

                    b.Property<int>("NationalComponentId");

                    b.HasKey("ApplicationId", "NationalComponentId");

                    b.HasIndex("NationalComponentId");

                    b.ToTable("ApplicationNationalComponent");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.ApplicationStandard", b =>
                {
                    b.Property<int>("ApplicationId");

                    b.Property<int>("StandardId");

                    b.HasKey("ApplicationId", "StandardId");

                    b.HasIndex("StandardId");

                    b.ToTable("ApplicationStandard");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.ApplicationSupportedIntegration", b =>
                {
                    b.Property<int>("ApplicationId");

                    b.Property<int>("SupportedIntegrationId");

                    b.HasKey("ApplicationId", "SupportedIntegrationId");

                    b.HasIndex("SupportedIntegrationId");

                    b.ToTable("ApplicationSupportedIntegration");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<int?>("ApplicationId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FullName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.Dataset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessRight");

                    b.Property<string>("DataLocation");

                    b.Property<int?>("DcatCatalogId");

                    b.Property<string>("Description");

                    b.Property<bool>("HasMasterData");

                    b.Property<bool>("HasPersonalData");

                    b.Property<bool>("HasSensitivePersonalData");

                    b.Property<string>("Name");

                    b.Property<DateTime?>("PublishedToSharedDataCatalog");

                    b.Property<string>("Purpose");

                    b.HasKey("Id");

                    b.HasIndex("DcatCatalogId");

                    b.ToTable("Dataset");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.DcatCatalog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Homepage");

                    b.Property<DateTime?>("Issued");

                    b.Property<string>("Language");

                    b.Property<string>("License");

                    b.Property<DateTime?>("Modified");

                    b.Property<int>("OrganizationId");

                    b.Property<string>("ThemesTaxonomy");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId")
                        .IsUnique();

                    b.ToTable("DcatCatalog");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.DcatConcept", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DatasetId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("DatasetId");

                    b.ToTable("DcatConcept");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.Distribution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DatasetId");

                    b.Property<string>("Description");

                    b.Property<int?>("LicenseId");

                    b.HasKey("Id");

                    b.HasIndex("DatasetId");

                    b.HasIndex("LicenseId");

                    b.ToTable("Distribution");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.Field", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DatasetId");

                    b.Property<string>("Description");

                    b.Property<bool>("IsPersonalData");

                    b.Property<bool>("IsSensitivePersonalData");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("DatasetId");

                    b.ToTable("Field");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.Format", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DistributionId");

                    b.Property<string>("FormatField");

                    b.HasKey("Id");

                    b.HasIndex("DistributionId");

                    b.ToTable("Format");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.Identifier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DatasetId");

                    b.Property<string>("IdentifierField");

                    b.HasKey("Id");

                    b.HasIndex("DatasetId");

                    b.ToTable("Identifier");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.Integration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("Integration");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.Keyword", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DatasetId");

                    b.Property<string>("KeywordField");

                    b.HasKey("Id");

                    b.HasIndex("DatasetId");

                    b.ToTable("Keyword");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.LicenseDocument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LicenseType");

                    b.HasKey("Id");

                    b.ToTable("LicenseDocument");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.NationalComponent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("NationalComponent");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.Organization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("OrganizationNumber");

                    b.HasKey("Id");

                    b.ToTable("Organization");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.OrganizationAdministrators", b =>
                {
                    b.Property<int>("OrganizationId");

                    b.Property<string>("ApplicationUserId");

                    b.HasKey("OrganizationId", "ApplicationUserId");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("OrganizationAdministrators");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.OrganizationApplicationUser", b =>
                {
                    b.Property<int>("OrganizationId");

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("Role");

                    b.HasKey("OrganizationId", "ApplicationUserId", "Role");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("OrganizationApplicationUser");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.ResourceLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ApplicationId");

                    b.Property<int?>("DatasetId");

                    b.Property<int?>("DatasetId1");

                    b.Property<int?>("DatasetId2");

                    b.Property<string>("Description");

                    b.Property<int?>("DistributionId");

                    b.Property<int?>("SectorId");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("DatasetId");

                    b.HasIndex("DatasetId1");

                    b.HasIndex("DatasetId2");

                    b.HasIndex("DistributionId");

                    b.HasIndex("SectorId");

                    b.ToTable("ResourceLink");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.Sector", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Sector");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.SectorApplication", b =>
                {
                    b.Property<int>("SectorId");

                    b.Property<int>("ApplicationId");

                    b.HasKey("SectorId", "ApplicationId");

                    b.HasIndex("ApplicationId");

                    b.ToTable("SectorApplication");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.Standard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Standard");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.AccessRightComment", b =>
                {
                    b.HasOne("Arkitektum.Orden.Models.Dataset")
                        .WithMany("AccessRightComments")
                        .HasForeignKey("DatasetId");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.Application", b =>
                {
                    b.HasOne("Arkitektum.Orden.Models.Organization", "Organization")
                        .WithMany("Applications")
                        .HasForeignKey("OrganizationId");

                    b.HasOne("Arkitektum.Orden.Models.ApplicationUser", "SystemOwner")
                        .WithMany()
                        .HasForeignKey("SystemOwnerId");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.ApplicationDataset", b =>
                {
                    b.HasOne("Arkitektum.Orden.Models.Application", "Application")
                        .WithMany("ApplicationDatasets")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Arkitektum.Orden.Models.Dataset", "Dataset")
                        .WithMany("ApplicationDatasets")
                        .HasForeignKey("DatasetId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.ApplicationNationalComponent", b =>
                {
                    b.HasOne("Arkitektum.Orden.Models.Application", "Application")
                        .WithMany("ApplicationNationalComponent")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Arkitektum.Orden.Models.NationalComponent", "NationalComponent")
                        .WithMany("ApplicationNationalComponents")
                        .HasForeignKey("NationalComponentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.ApplicationStandard", b =>
                {
                    b.HasOne("Arkitektum.Orden.Models.Application", "Application")
                        .WithMany("ApplicationStandards")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Arkitektum.Orden.Models.Standard", "Standard")
                        .WithMany("ApplicationStandards")
                        .HasForeignKey("StandardId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.ApplicationSupportedIntegration", b =>
                {
                    b.HasOne("Arkitektum.Orden.Models.Application", "Application")
                        .WithMany("ApplicationSupportedIntegrations")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Arkitektum.Orden.Models.Integration", "SupportedIntegration")
                        .WithMany("ApplicationSupportedIntegrations")
                        .HasForeignKey("SupportedIntegrationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.ApplicationUser", b =>
                {
                    b.HasOne("Arkitektum.Orden.Models.Application")
                        .WithMany("SuperUsers")
                        .HasForeignKey("ApplicationId");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.Dataset", b =>
                {
                    b.HasOne("Arkitektum.Orden.Models.DcatCatalog", "DcatCatalog")
                        .WithMany("Datasets")
                        .HasForeignKey("DcatCatalogId");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.DcatCatalog", b =>
                {
                    b.HasOne("Arkitektum.Orden.Models.Organization", "Organization")
                        .WithOne("DcatCatalog")
                        .HasForeignKey("Arkitektum.Orden.Models.DcatCatalog", "OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.DcatConcept", b =>
                {
                    b.HasOne("Arkitektum.Orden.Models.Dataset")
                        .WithMany("Concepts")
                        .HasForeignKey("DatasetId");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.Distribution", b =>
                {
                    b.HasOne("Arkitektum.Orden.Models.Dataset", "Dataset")
                        .WithMany("Distributions")
                        .HasForeignKey("DatasetId");

                    b.HasOne("Arkitektum.Orden.Models.LicenseDocument", "License")
                        .WithMany()
                        .HasForeignKey("LicenseId");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.Field", b =>
                {
                    b.HasOne("Arkitektum.Orden.Models.Dataset", "Dataset")
                        .WithMany("Fields")
                        .HasForeignKey("DatasetId");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.Format", b =>
                {
                    b.HasOne("Arkitektum.Orden.Models.Distribution")
                        .WithMany("Formats")
                        .HasForeignKey("DistributionId");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.Identifier", b =>
                {
                    b.HasOne("Arkitektum.Orden.Models.Dataset")
                        .WithMany("Identifiers")
                        .HasForeignKey("DatasetId");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.Keyword", b =>
                {
                    b.HasOne("Arkitektum.Orden.Models.Dataset")
                        .WithMany("Keywords")
                        .HasForeignKey("DatasetId");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.OrganizationAdministrators", b =>
                {
                    b.HasOne("Arkitektum.Orden.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("OrganizationAdministrators")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Arkitektum.Orden.Models.Organization", "Organization")
                        .WithMany("OrganizationAdministrators")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.OrganizationApplicationUser", b =>
                {
                    b.HasOne("Arkitektum.Orden.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("Organizations")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Arkitektum.Orden.Models.Organization", "Organization")
                        .WithMany("Users")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.ResourceLink", b =>
                {
                    b.HasOne("Arkitektum.Orden.Models.Application")
                        .WithMany("ResourceLinks")
                        .HasForeignKey("ApplicationId");

                    b.HasOne("Arkitektum.Orden.Models.Dataset")
                        .WithMany("ContactPoints")
                        .HasForeignKey("DatasetId");

                    b.HasOne("Arkitektum.Orden.Models.Dataset")
                        .WithMany("LawReferences")
                        .HasForeignKey("DatasetId1");

                    b.HasOne("Arkitektum.Orden.Models.Dataset")
                        .WithMany("ResourceLinks")
                        .HasForeignKey("DatasetId2");

                    b.HasOne("Arkitektum.Orden.Models.Distribution")
                        .WithMany("Resources")
                        .HasForeignKey("DistributionId");

                    b.HasOne("Arkitektum.Orden.Models.Sector")
                        .WithMany("LawReferences")
                        .HasForeignKey("SectorId");
                });

            modelBuilder.Entity("Arkitektum.Orden.Models.SectorApplication", b =>
                {
                    b.HasOne("Arkitektum.Orden.Models.Application", "Application")
                        .WithMany("SectorApplications")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Arkitektum.Orden.Models.Sector", "Sector")
                        .WithMany("SectorApplications")
                        .HasForeignKey("SectorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Arkitektum.Orden.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Arkitektum.Orden.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Arkitektum.Orden.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Arkitektum.Orden.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
