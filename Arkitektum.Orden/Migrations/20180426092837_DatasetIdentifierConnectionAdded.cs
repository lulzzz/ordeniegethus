using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Arkitektum.Orden.Migrations
{
    public partial class DatasetIdentifierConnectionAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Identifier_Dataset_DatasetId",
                table: "Identifier");

            migrationBuilder.AlterColumn<int>(
                name: "DatasetId",
                table: "Identifier",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Identifier_Dataset_DatasetId",
                table: "Identifier",
                column: "DatasetId",
                principalTable: "Dataset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Identifier_Dataset_DatasetId",
                table: "Identifier");

            migrationBuilder.AlterColumn<int>(
                name: "DatasetId",
                table: "Identifier",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Identifier_Dataset_DatasetId",
                table: "Identifier",
                column: "DatasetId",
                principalTable: "Dataset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
