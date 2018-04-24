using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Arkitektum.Orden.Migrations
{
    public partial class HostingLocationAsEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"UPDATE Application SET HostingLocation = null;");
            
            migrationBuilder.AlterColumn<int>(
                name: "HostingLocation",
                table: "Application",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommonApplicationVersionStandard_StandardId",
                table: "CommonApplicationVersionStandard",
                column: "StandardId");

            migrationBuilder.CreateIndex(
                name: "IX_CommonApplicationVersionNationalComponent_NationalComponentId",
                table: "CommonApplicationVersionNationalComponent",
                column: "NationalComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommonApplicationVersionNationalComponent_NationalComponent_NationalComponentId",
                table: "CommonApplicationVersionNationalComponent",
                column: "NationalComponentId",
                principalTable: "NationalComponent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommonApplicationVersionStandard_Standard_StandardId",
                table: "CommonApplicationVersionStandard",
                column: "StandardId",
                principalTable: "Standard",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommonApplicationVersionNationalComponent_NationalComponent_NationalComponentId",
                table: "CommonApplicationVersionNationalComponent");

            migrationBuilder.DropForeignKey(
                name: "FK_CommonApplicationVersionStandard_Standard_StandardId",
                table: "CommonApplicationVersionStandard");

            migrationBuilder.DropIndex(
                name: "IX_CommonApplicationVersionStandard_StandardId",
                table: "CommonApplicationVersionStandard");

            migrationBuilder.DropIndex(
                name: "IX_CommonApplicationVersionNationalComponent_NationalComponentId",
                table: "CommonApplicationVersionNationalComponent");

            migrationBuilder.AlterColumn<string>(
                name: "HostingLocation",
                table: "Application",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
