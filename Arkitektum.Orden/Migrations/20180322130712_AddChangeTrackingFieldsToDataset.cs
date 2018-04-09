using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Arkitektum.Orden.Migrations
{
    public partial class AddChangeTrackingFieldsToDataset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Dataset",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Dataset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "Dataset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "Dataset",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Dataset");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Dataset");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "Dataset");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "Dataset");
        }
    }
}
