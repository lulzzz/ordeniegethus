using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Arkitektum.Orden.Migrations
{
    public partial class ApplicationSuperUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ApplicationSuperUser_SuperUserId",
                table: "ApplicationSuperUser",
                column: "SuperUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationSuperUser_SuperUser_SuperUserId",
                table: "ApplicationSuperUser",
                column: "SuperUserId",
                principalTable: "SuperUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationSuperUser_SuperUser_SuperUserId",
                table: "ApplicationSuperUser");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationSuperUser_SuperUserId",
                table: "ApplicationSuperUser");
        }
    }
}
