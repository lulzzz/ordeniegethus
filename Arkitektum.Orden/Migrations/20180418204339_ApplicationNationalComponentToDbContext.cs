using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Arkitektum.Orden.Migrations
{
    public partial class ApplicationNationalComponentToDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationNationalComponent_Application_ApplicationId",
                table: "ApplicationNationalComponent");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationNationalComponent_NationalComponent_NationalComponentId",
                table: "ApplicationNationalComponent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationNationalComponent",
                table: "ApplicationNationalComponent");

            migrationBuilder.RenameTable(
                name: "ApplicationNationalComponent",
                newName: "ApplicationNationalComponents");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationNationalComponent_NationalComponentId",
                table: "ApplicationNationalComponents",
                newName: "IX_ApplicationNationalComponents_NationalComponentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationNationalComponents",
                table: "ApplicationNationalComponents",
                columns: new[] { "ApplicationId", "NationalComponentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationNationalComponents_Application_ApplicationId",
                table: "ApplicationNationalComponents",
                column: "ApplicationId",
                principalTable: "Application",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationNationalComponents_NationalComponent_NationalComponentId",
                table: "ApplicationNationalComponents",
                column: "NationalComponentId",
                principalTable: "NationalComponent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationNationalComponents_Application_ApplicationId",
                table: "ApplicationNationalComponents");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationNationalComponents_NationalComponent_NationalComponentId",
                table: "ApplicationNationalComponents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationNationalComponents",
                table: "ApplicationNationalComponents");

            migrationBuilder.RenameTable(
                name: "ApplicationNationalComponents",
                newName: "ApplicationNationalComponent");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationNationalComponents_NationalComponentId",
                table: "ApplicationNationalComponent",
                newName: "IX_ApplicationNationalComponent_NationalComponentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationNationalComponent",
                table: "ApplicationNationalComponent",
                columns: new[] { "ApplicationId", "NationalComponentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationNationalComponent_Application_ApplicationId",
                table: "ApplicationNationalComponent",
                column: "ApplicationId",
                principalTable: "Application",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationNationalComponent_NationalComponent_NationalComponentId",
                table: "ApplicationNationalComponent",
                column: "NationalComponentId",
                principalTable: "NationalComponent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
