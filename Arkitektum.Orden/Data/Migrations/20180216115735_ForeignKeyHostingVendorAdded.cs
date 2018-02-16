using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Arkitektum.Orden.Data.Migrations
{
    public partial class ForeignKeyHostingVendorAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HostingVendorId",
                table: "Application",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Application_HostingVendorId",
                table: "Application",
                column: "HostingVendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Application_VendorId",
                table: "Application",
                column: "VendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_Vendor_HostingVendorId",
                table: "Application",
                column: "HostingVendorId",
                principalTable: "Vendor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Application_Vendor_VendorId",
                table: "Application",
                column: "VendorId",
                principalTable: "Vendor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_Vendor_HostingVendorId",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_Application_Vendor_VendorId",
                table: "Application");

            migrationBuilder.DropTable(
                name: "Vendor");

            migrationBuilder.DropIndex(
                name: "IX_Application_HostingVendorId",
                table: "Application");

            migrationBuilder.DropIndex(
                name: "IX_Application_VendorId",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "HostingVendorId",
                table: "Application");
        }
    }
}
