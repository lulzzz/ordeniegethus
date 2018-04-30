using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Arkitektum.Orden.Migrations
{
    public partial class DcatModelsRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceLink_Distribution_DistributionId",
                table: "ResourceLink");

            migrationBuilder.DropTable(
                name: "AccessRightComment");

            migrationBuilder.DropTable(
                name: "ContactPoint");

            migrationBuilder.DropTable(
                name: "DcatConcept");

            migrationBuilder.DropTable(
                name: "Format");

            migrationBuilder.DropTable(
                name: "Identifier");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "Distribution");

            migrationBuilder.DropTable(
                name: "LicenseDocument");

            migrationBuilder.DropIndex(
                name: "IX_ResourceLink_DistributionId",
                table: "ResourceLink");

            migrationBuilder.DropColumn(
                name: "DistributionId",
                table: "ResourceLink");

            migrationBuilder.AddColumn<string>(
                name: "AccessRightComments",
                table: "Dataset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Concepts",
                table: "Dataset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactPoints",
                table: "Dataset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Distributions",
                table: "Dataset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Identifiers",
                table: "Dataset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subjects",
                table: "Dataset",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessRightComments",
                table: "Dataset");

            migrationBuilder.DropColumn(
                name: "Concepts",
                table: "Dataset");

            migrationBuilder.DropColumn(
                name: "ContactPoints",
                table: "Dataset");

            migrationBuilder.DropColumn(
                name: "Distributions",
                table: "Dataset");

            migrationBuilder.DropColumn(
                name: "Identifiers",
                table: "Dataset");

            migrationBuilder.DropColumn(
                name: "Subjects",
                table: "Dataset");

            migrationBuilder.AddColumn<int>(
                name: "DistributionId",
                table: "ResourceLink",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccessRightComment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccessRightCommentField = table.Column<string>(nullable: true),
                    DatasetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessRightComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessRightComment_Dataset_DatasetId",
                        column: x => x.DatasetId,
                        principalTable: "Dataset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactPoint",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContactPointField = table.Column<string>(nullable: true),
                    DatasetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactPoint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactPoint_Dataset_DatasetId",
                        column: x => x.DatasetId,
                        principalTable: "Dataset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DcatConcept",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    DatasetId = table.Column<int>(nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Identifier",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatasetId = table.Column<int>(nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
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
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatasetId = table.Column<int>(nullable: false),
                    SubjectField = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subject_Dataset_DatasetId",
                        column: x => x.DatasetId,
                        principalTable: "Dataset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Distribution",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatasetId = table.Column<int>(nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_ResourceLink_DistributionId",
                table: "ResourceLink",
                column: "DistributionId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessRightComment_DatasetId",
                table: "AccessRightComment",
                column: "DatasetId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPoint_DatasetId",
                table: "ContactPoint",
                column: "DatasetId");

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
                name: "IX_Subject_DatasetId",
                table: "Subject",
                column: "DatasetId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceLink_Distribution_DistributionId",
                table: "ResourceLink",
                column: "DistributionId",
                principalTable: "Distribution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
