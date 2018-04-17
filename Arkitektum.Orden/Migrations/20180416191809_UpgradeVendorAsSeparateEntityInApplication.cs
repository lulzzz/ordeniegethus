using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Arkitektum.Orden.Migrations
{
    public partial class UpgradeVendorAsSeparateEntityInApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Field_Dataset_DatasetId",
                table: "Field");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Field",
                table: "Field");

            migrationBuilder.DropColumn(
                name: "Vendor",
                table: "Application");

            migrationBuilder.RenameTable(
                name: "Field",
                newName: "Fields");

            migrationBuilder.RenameIndex(
                name: "IX_Field_DatasetId",
                table: "Fields",
                newName: "IX_Fields_DatasetId");

            migrationBuilder.AddColumn<string>(
                name: "HomepageUrl",
                table: "Vendor",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VendorId",
                table: "Application",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fields",
                table: "Fields",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Application_VendorId",
                table: "Application",
                column: "VendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_Vendor_VendorId",
                table: "Application",
                column: "VendorId",
                principalTable: "Vendor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Dataset_DatasetId",
                table: "Fields",
                column: "DatasetId",
                principalTable: "Dataset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_Vendor_VendorId",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Dataset_DatasetId",
                table: "Fields");

            migrationBuilder.DropIndex(
                name: "IX_Application_VendorId",
                table: "Application");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fields",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "HomepageUrl",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "VendorId",
                table: "Application");

            migrationBuilder.RenameTable(
                name: "Fields",
                newName: "Field");

            migrationBuilder.RenameIndex(
                name: "IX_Fields_DatasetId",
                table: "Field",
                newName: "IX_Field_DatasetId");

            migrationBuilder.AddColumn<string>(
                name: "Vendor",
                table: "Application",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Field",
                table: "Field",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Field_Dataset_DatasetId",
                table: "Field",
                column: "DatasetId",
                principalTable: "Dataset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
