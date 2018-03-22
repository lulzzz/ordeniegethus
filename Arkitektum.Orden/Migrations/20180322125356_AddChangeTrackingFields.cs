using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Arkitektum.Orden.Migrations
{
    public partial class AddChangeTrackingFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Application",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Application",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "Application",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "Application",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "Application");
        }
    }
}
