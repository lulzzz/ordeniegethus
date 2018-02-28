using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Arkitektum.Orden.Data.Migrations
{
    public partial class AddRoleAsKeyOnOrganizationApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganizationApplicationUser",
                table: "OrganizationApplicationUser");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "SharedService");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "OrganizationApplicationUser",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganizationApplicationUser",
                table: "OrganizationApplicationUser",
                columns: new[] { "OrganizationId", "ApplicationUserId", "Role" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganizationApplicationUser",
                table: "OrganizationApplicationUser");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "OrganizationApplicationUser");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "SharedService",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganizationApplicationUser",
                table: "OrganizationApplicationUser",
                columns: new[] { "OrganizationId", "ApplicationUserId" });
        }
    }
}
