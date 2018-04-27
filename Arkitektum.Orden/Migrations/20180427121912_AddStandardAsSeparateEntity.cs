using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Arkitektum.Orden.Migrations
{
    public partial class AddStandardAsSeparateEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationStandard_Standard_StandardId",
                table: "ApplicationStandard");

            migrationBuilder.DropForeignKey(
                name: "FK_CommonApplicationVersionStandard_Standard_StandardId",
                table: "CommonApplicationVersionStandard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Standard",
                table: "Standard");

            migrationBuilder.RenameTable(
                name: "Standard",
                newName: "Standards");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Standards",
                table: "Standards",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationStandard_Standards_StandardId",
                table: "ApplicationStandard",
                column: "StandardId",
                principalTable: "Standards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommonApplicationVersionStandard_Standards_StandardId",
                table: "CommonApplicationVersionStandard",
                column: "StandardId",
                principalTable: "Standards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationStandard_Standards_StandardId",
                table: "ApplicationStandard");

            migrationBuilder.DropForeignKey(
                name: "FK_CommonApplicationVersionStandard_Standards_StandardId",
                table: "CommonApplicationVersionStandard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Standards",
                table: "Standards");

            migrationBuilder.RenameTable(
                name: "Standards",
                newName: "Standard");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Standard",
                table: "Standard",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationStandard_Standard_StandardId",
                table: "ApplicationStandard",
                column: "StandardId",
                principalTable: "Standard",
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
    }
}
