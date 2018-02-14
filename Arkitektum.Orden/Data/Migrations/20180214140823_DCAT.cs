using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Arkitektum.Orden.Data.Migrations
{
    public partial class DCAT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DatasetId1",
                table: "ResourceLink",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DistributionId",
                table: "ResourceLink",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccessRight",
                table: "Dataset",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DcatCatalogId",
                table: "Dataset",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccessRightComment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccessRightCommentField = table.Column<string>(nullable: true),
                    DatasetId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessRightComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessRightComment_Dataset_DatasetId",
                        column: x => x.DatasetId,
                        principalTable: "Dataset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DcatCatalog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Homepage = table.Column<string>(nullable: true),
                    Issued = table.Column<DateTime>(nullable: true),
                    Language = table.Column<string>(nullable: true),
                    License = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    ThemesTaxonomy = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DcatCatalog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DcatCatalog_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DcatConcept",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatasetId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DcatConcept", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DcatConcept_Dataset_DatasetId",
                        column: x => x.DatasetId,
                        principalTable: "Dataset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Identifier",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatasetId = table.Column<int>(nullable: true),
                    IdentifierField = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identifier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Identifier_Dataset_DatasetId",
                        column: x => x.DatasetId,
                        principalTable: "Dataset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Keyword",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatasetId = table.Column<int>(nullable: true),
                    KeywordField = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keyword", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Keyword_Dataset_DatasetId",
                        column: x => x.DatasetId,
                        principalTable: "Dataset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LicenseDocument",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LicenseType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseDocument", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Distribution",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatasetId = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    LicenseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distribution", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Distribution_Dataset_DatasetId",
                        column: x => x.DatasetId,
                        principalTable: "Dataset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Distribution_LicenseDocument_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "LicenseDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Format",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DistributionId = table.Column<int>(nullable: true),
                    FormatField = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Format", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Format_Distribution_DistributionId",
                        column: x => x.DistributionId,
                        principalTable: "Distribution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResourceLink_DatasetId1",
                table: "ResourceLink",
                column: "DatasetId1");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceLink_DistributionId",
                table: "ResourceLink",
                column: "DistributionId");

            migrationBuilder.CreateIndex(
                name: "IX_Dataset_DcatCatalogId",
                table: "Dataset",
                column: "DcatCatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessRightComment_DatasetId",
                table: "AccessRightComment",
                column: "DatasetId");

            migrationBuilder.CreateIndex(
                name: "IX_DcatCatalog_OrganizationId",
                table: "DcatCatalog",
                column: "OrganizationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DcatConcept_DatasetId",
                table: "DcatConcept",
                column: "DatasetId");

            migrationBuilder.CreateIndex(
                name: "IX_Distribution_DatasetId",
                table: "Distribution",
                column: "DatasetId");

            migrationBuilder.CreateIndex(
                name: "IX_Distribution_LicenseId",
                table: "Distribution",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Format_DistributionId",
                table: "Format",
                column: "DistributionId");

            migrationBuilder.CreateIndex(
                name: "IX_Identifier_DatasetId",
                table: "Identifier",
                column: "DatasetId");

            migrationBuilder.CreateIndex(
                name: "IX_Keyword_DatasetId",
                table: "Keyword",
                column: "DatasetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dataset_DcatCatalog_DcatCatalogId",
                table: "Dataset",
                column: "DcatCatalogId",
                principalTable: "DcatCatalog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceLink_Dataset_DatasetId1",
                table: "ResourceLink",
                column: "DatasetId1",
                principalTable: "Dataset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceLink_Distribution_DistributionId",
                table: "ResourceLink",
                column: "DistributionId",
                principalTable: "Distribution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dataset_DcatCatalog_DcatCatalogId",
                table: "Dataset");

            migrationBuilder.DropForeignKey(
                name: "FK_ResourceLink_Dataset_DatasetId1",
                table: "ResourceLink");

            migrationBuilder.DropForeignKey(
                name: "FK_ResourceLink_Distribution_DistributionId",
                table: "ResourceLink");

            migrationBuilder.DropTable(
                name: "AccessRightComment");

            migrationBuilder.DropTable(
                name: "DcatCatalog");

            migrationBuilder.DropTable(
                name: "DcatConcept");

            migrationBuilder.DropTable(
                name: "Format");

            migrationBuilder.DropTable(
                name: "Identifier");

            migrationBuilder.DropTable(
                name: "Keyword");

            migrationBuilder.DropTable(
                name: "Distribution");

            migrationBuilder.DropTable(
                name: "LicenseDocument");

            migrationBuilder.DropIndex(
                name: "IX_ResourceLink_DatasetId1",
                table: "ResourceLink");

            migrationBuilder.DropIndex(
                name: "IX_ResourceLink_DistributionId",
                table: "ResourceLink");

            migrationBuilder.DropIndex(
                name: "IX_Dataset_DcatCatalogId",
                table: "Dataset");

            migrationBuilder.DropColumn(
                name: "DatasetId1",
                table: "ResourceLink");

            migrationBuilder.DropColumn(
                name: "DistributionId",
                table: "ResourceLink");

            migrationBuilder.DropColumn(
                name: "AccessRight",
                table: "Dataset");

            migrationBuilder.DropColumn(
                name: "DcatCatalogId",
                table: "Dataset");
        }
    }
}
