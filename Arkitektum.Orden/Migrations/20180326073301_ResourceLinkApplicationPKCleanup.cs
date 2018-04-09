using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Arkitektum.Orden.Migrations
{
    public partial class ResourceLinkApplicationPKCleanup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceLink_Application_ApplicationId1",
                table: "ResourceLink");

            migrationBuilder.DropIndex(
                name: "IX_ResourceLink_ApplicationId1",
                table: "ResourceLink");

            migrationBuilder.DropColumn(
                name: "ApplicationId1",
                table: "ResourceLink");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationId",
                table: "ResourceLink",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResourceLink_ApplicationId",
                table: "ResourceLink",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceLink_Application_ApplicationId",
                table: "ResourceLink",
                column: "ApplicationId",
                principalTable: "Application",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceLink_Application_ApplicationId",
                table: "ResourceLink");

            migrationBuilder.DropIndex(
                name: "IX_ResourceLink_ApplicationId",
                table: "ResourceLink");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationId",
                table: "ResourceLink",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId1",
                table: "ResourceLink",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResourceLink_ApplicationId1",
                table: "ResourceLink",
                column: "ApplicationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceLink_Application_ApplicationId1",
                table: "ResourceLink",
                column: "ApplicationId1",
                principalTable: "Application",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
