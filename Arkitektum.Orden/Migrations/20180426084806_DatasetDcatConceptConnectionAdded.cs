using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Arkitektum.Orden.Migrations
{
    public partial class DatasetDcatConceptConnectionAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DcatConcept_Dataset_DatasetId",
                table: "DcatConcept");

            migrationBuilder.AlterColumn<int>(
                name: "DatasetId",
                table: "DcatConcept",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "DcatConcept",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DcatConcept_Dataset_DatasetId",
                table: "DcatConcept",
                column: "DatasetId",
                principalTable: "Dataset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DcatConcept_Dataset_DatasetId",
                table: "DcatConcept");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "DcatConcept");

            migrationBuilder.AlterColumn<int>(
                name: "DatasetId",
                table: "DcatConcept",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_DcatConcept_Dataset_DatasetId",
                table: "DcatConcept",
                column: "DatasetId",
                principalTable: "Dataset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
