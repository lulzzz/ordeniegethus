using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Arkitektum.Orden.Migrations
{
    public partial class DatasetHostingLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataLocation",
                table: "Dataset");

            migrationBuilder.AddColumn<int>(
                name: "HostingLocation",
                table: "Dataset",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HostingLocation",
                table: "Dataset");

            migrationBuilder.AddColumn<string>(
                name: "DataLocation",
                table: "Dataset",
                nullable: true);
        }
    }
}
