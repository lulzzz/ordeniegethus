using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Arkitektum.Orden.Migrations
{
    public partial class AdjustAppRegistry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommonApplications_Vendors_VendorId",
                table: "CommonApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_CommonApplicationVersion_CommonApplications_CommonApplicationId",
                table: "CommonApplicationVersion");

            migrationBuilder.DropForeignKey(
                name: "FK_CommonDataset_CommonApplications_CommonApplicationId",
                table: "CommonDataset");

            migrationBuilder.AlterColumn<int>(
                name: "CommonApplicationId",
                table: "CommonDataset",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CommonApplicationId",
                table: "CommonApplicationVersion",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VendorId",
                table: "CommonApplications",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CommonApplications_Vendors_VendorId",
                table: "CommonApplications",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommonApplicationVersion_CommonApplications_CommonApplicationId",
                table: "CommonApplicationVersion",
                column: "CommonApplicationId",
                principalTable: "CommonApplications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommonDataset_CommonApplications_CommonApplicationId",
                table: "CommonDataset",
                column: "CommonApplicationId",
                principalTable: "CommonApplications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommonApplications_Vendors_VendorId",
                table: "CommonApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_CommonApplicationVersion_CommonApplications_CommonApplicationId",
                table: "CommonApplicationVersion");

            migrationBuilder.DropForeignKey(
                name: "FK_CommonDataset_CommonApplications_CommonApplicationId",
                table: "CommonDataset");

            migrationBuilder.AlterColumn<int>(
                name: "CommonApplicationId",
                table: "CommonDataset",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CommonApplicationId",
                table: "CommonApplicationVersion",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "VendorId",
                table: "CommonApplications",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_CommonApplications_Vendors_VendorId",
                table: "CommonApplications",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommonApplicationVersion_CommonApplications_CommonApplicationId",
                table: "CommonApplicationVersion",
                column: "CommonApplicationId",
                principalTable: "CommonApplications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommonDataset_CommonApplications_CommonApplicationId",
                table: "CommonDataset",
                column: "CommonApplicationId",
                principalTable: "CommonApplications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
