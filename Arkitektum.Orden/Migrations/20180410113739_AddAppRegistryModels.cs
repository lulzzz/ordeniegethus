using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Arkitektum.Orden.Migrations
{
    public partial class AddAppRegistryModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommonApplications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    VendorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommonApplications_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommonApplicationVersion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VersionNumber = table.Column<string>(nullable: true),
                    CommonApplicationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonApplicationVersion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommonApplicationVersion_CommonApplications_CommonApplicationId",
                        column: x => x.CommonApplicationId,
                        principalTable: "CommonApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommonDataset",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Purpose = table.Column<string>(nullable: true),
                    HasPersonalData = table.Column<bool>(nullable: false),
                    HasSensitivePersonalData = table.Column<bool>(nullable: false),
                    HasMasterData = table.Column<bool>(nullable: false),
                    CommonApplicationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonDataset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommonDataset_CommonApplications_CommonApplicationId",
                        column: x => x.CommonApplicationId,
                        principalTable: "CommonApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommonApplicationVersionNationalComponent",
                columns: table => new
                {
                    CommonApplicationVersionId = table.Column<int>(nullable: false),
                    NationalComponentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonApplicationVersionNationalComponent", x => new { x.CommonApplicationVersionId, x.NationalComponentId });
                    table.ForeignKey(
                        name: "FK_CommonApplicationVersionNationalComponent_CommonApplicationVersion_CommonApplicationVersionId",
                        column: x => x.CommonApplicationVersionId,
                        principalTable: "CommonApplicationVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommonApplicationVersionStandard",
                columns: table => new
                {
                    CommonApplicationVersionId = table.Column<int>(nullable: false),
                    StandardId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonApplicationVersionStandard", x => new { x.CommonApplicationVersionId, x.StandardId });
                    table.ForeignKey(
                        name: "FK_CommonApplicationVersionStandard_CommonApplicationVersion_CommonApplicationVersionId",
                        column: x => x.CommonApplicationVersionId,
                        principalTable: "CommonApplicationVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommonDatasetField",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsPersonalData = table.Column<bool>(nullable: false),
                    IsSensitivePersonalData = table.Column<bool>(nullable: false),
                    CommonDatasetId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonDatasetField", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommonDatasetField_CommonDataset_CommonDatasetId",
                        column: x => x.CommonDatasetId,
                        principalTable: "CommonDataset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommonApplications_VendorId",
                table: "CommonApplications",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_CommonApplicationVersion_CommonApplicationId",
                table: "CommonApplicationVersion",
                column: "CommonApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_CommonDataset_CommonApplicationId",
                table: "CommonDataset",
                column: "CommonApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_CommonDatasetField_CommonDatasetId",
                table: "CommonDatasetField",
                column: "CommonDatasetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommonApplicationVersionNationalComponent");

            migrationBuilder.DropTable(
                name: "CommonApplicationVersionStandard");

            migrationBuilder.DropTable(
                name: "CommonDatasetField");

            migrationBuilder.DropTable(
                name: "CommonApplicationVersion");

            migrationBuilder.DropTable(
                name: "CommonDataset");

            migrationBuilder.DropTable(
                name: "CommonApplications");

            migrationBuilder.DropTable(
                name: "Vendor");
        }
    }
}
