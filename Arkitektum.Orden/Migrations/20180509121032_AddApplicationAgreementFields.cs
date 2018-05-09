using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Arkitektum.Orden.Migrations
{
    public partial class AddApplicationAgreementFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Agreement_DateStart",
                table: "Application",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Agreement_Description",
                table: "Application",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Agreement_DocumentUrl",
                table: "Application",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Agreement_ResponsibleRole",
                table: "Application",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Agreement_TerminationClauses",
                table: "Application",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Agreement_DateStart",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "Agreement_Description",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "Agreement_DocumentUrl",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "Agreement_ResponsibleRole",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "Agreement_TerminationClauses",
                table: "Application");
        }
    }
}
