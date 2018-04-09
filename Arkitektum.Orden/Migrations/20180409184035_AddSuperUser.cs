using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Arkitektum.Orden.Migrations
{
    public partial class AddSuperUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Application_ApplicationId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ApplicationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "ApplicationSuperUser",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(nullable: false),
                    SuperUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationSuperUser", x => new { x.ApplicationId, x.SuperUserId });
                    table.ForeignKey(
                        name: "FK_ApplicationSuperUser_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SuperUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuperUser_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SuperUser_OrganizationId",
                table: "SuperUser",
                column: "OrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationSuperUser");

            migrationBuilder.DropTable(
                name: "SuperUser");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ApplicationId",
                table: "AspNetUsers",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Application_ApplicationId",
                table: "AspNetUsers",
                column: "ApplicationId",
                principalTable: "Application",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
