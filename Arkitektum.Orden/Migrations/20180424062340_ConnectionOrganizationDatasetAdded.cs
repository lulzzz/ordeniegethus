using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Arkitektum.Orden.Migrations
{
    public partial class ConnectionOrganizationDatasetAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "Dataset",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Dataset_OrganizationId",
                table: "Dataset",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dataset_Organization_OrganizationId",
                table: "Dataset",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dataset_Organization_OrganizationId",
                table: "Dataset");

            migrationBuilder.DropIndex(
                name: "IX_Dataset_OrganizationId",
                table: "Dataset");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Dataset");
        }
    }
}
