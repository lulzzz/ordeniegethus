using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Arkitektum.Orden.Migrations
{
    public partial class AddRoleNameToApplicationDatasetRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessPermission",
                table: "ApplicationDataset");

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "ApplicationDataset",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "ApplicationDataset");

            migrationBuilder.AddColumn<int>(
                name: "AccessPermission",
                table: "ApplicationDataset",
                nullable: false,
                defaultValue: 0);
        }
    }
}
