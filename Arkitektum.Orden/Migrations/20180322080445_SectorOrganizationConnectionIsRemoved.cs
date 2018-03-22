using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Arkitektum.Orden.Migrations
{
    public partial class SectorOrganizationConnectionIsRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sector_Organization_OrganizationId",
                table: "Sector");

            migrationBuilder.DropIndex(
                name: "IX_Sector_OrganizationId",
                table: "Sector");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Sector");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "Sector",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sector_OrganizationId",
                table: "Sector",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sector_Organization_OrganizationId",
                table: "Sector",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
