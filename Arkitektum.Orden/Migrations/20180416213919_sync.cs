using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Arkitektum.Orden.Migrations
{
    public partial class sync : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_Vendor_VendorId",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_CommonApplications_Vendor_VendorId",
                table: "CommonApplications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vendor",
                table: "Vendor");

            migrationBuilder.RenameTable(
                name: "Vendor",
                newName: "Vendors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vendors",
                table: "Vendors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_Vendors_VendorId",
                table: "Application",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommonApplications_Vendors_VendorId",
                table: "CommonApplications",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_Vendors_VendorId",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_CommonApplications_Vendors_VendorId",
                table: "CommonApplications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vendors",
                table: "Vendors");

            migrationBuilder.RenameTable(
                name: "Vendors",
                newName: "Vendor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vendor",
                table: "Vendor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_Vendor_VendorId",
                table: "Application",
                column: "VendorId",
                principalTable: "Vendor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommonApplications_Vendor_VendorId",
                table: "CommonApplications",
                column: "VendorId",
                principalTable: "Vendor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
