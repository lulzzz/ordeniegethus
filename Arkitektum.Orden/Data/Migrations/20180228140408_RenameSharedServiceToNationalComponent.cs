using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Arkitektum.Orden.Data.Migrations
{
    public partial class RenameSharedServiceToNationalComponent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationSharedService");

            migrationBuilder.DropTable(
                name: "SharedService");

            migrationBuilder.CreateTable(
                name: "NationalComponent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NationalComponent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationNationalComponent",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(nullable: false),
                    NationalComponentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationNationalComponent", x => new { x.ApplicationId, x.NationalComponentId });
                    table.ForeignKey(
                        name: "FK_ApplicationNationalComponent_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationNationalComponent_NationalComponent_NationalComponentId",
                        column: x => x.NationalComponentId,
                        principalTable: "NationalComponent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationNationalComponent_NationalComponentId",
                table: "ApplicationNationalComponent",
                column: "NationalComponentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationNationalComponent");

            migrationBuilder.DropTable(
                name: "NationalComponent");

            migrationBuilder.CreateTable(
                name: "SharedService",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedService", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationSharedService",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(nullable: false),
                    SharedServiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationSharedService", x => new { x.ApplicationId, x.SharedServiceId });
                    table.ForeignKey(
                        name: "FK_ApplicationSharedService_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationSharedService_SharedService_SharedServiceId",
                        column: x => x.SharedServiceId,
                        principalTable: "SharedService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationSharedService_SharedServiceId",
                table: "ApplicationSharedService",
                column: "SharedServiceId");
        }
    }
}
