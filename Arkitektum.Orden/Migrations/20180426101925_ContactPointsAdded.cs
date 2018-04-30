using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Arkitektum.Orden.Migrations
{
    public partial class ContactPointsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceLink_Dataset_DatasetContactPointsId",
                table: "ResourceLink");

            migrationBuilder.RenameColumn(
                name: "DatasetContactPointsId",
                table: "ResourceLink",
                newName: "DatasetConnectionPointsId");

            migrationBuilder.RenameIndex(
                name: "IX_ResourceLink_DatasetContactPointsId",
                table: "ResourceLink",
                newName: "IX_ResourceLink_DatasetConnectionPointsId");

            migrationBuilder.CreateTable(
                name: "ContactPoint",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContactPointField = table.Column<string>(nullable: true),
                    DatasetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactPoint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactPoint_Dataset_DatasetId",
                        column: x => x.DatasetId,
                        principalTable: "Dataset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactPoint_DatasetId",
                table: "ContactPoint",
                column: "DatasetId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceLink_Dataset_DatasetConnectionPointsId",
                table: "ResourceLink",
                column: "DatasetConnectionPointsId",
                principalTable: "Dataset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceLink_Dataset_DatasetConnectionPointsId",
                table: "ResourceLink");

            migrationBuilder.DropTable(
                name: "ContactPoint");

            migrationBuilder.RenameColumn(
                name: "DatasetConnectionPointsId",
                table: "ResourceLink",
                newName: "DatasetContactPointsId");

            migrationBuilder.RenameIndex(
                name: "IX_ResourceLink_DatasetConnectionPointsId",
                table: "ResourceLink",
                newName: "IX_ResourceLink_DatasetContactPointsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceLink_Dataset_DatasetContactPointsId",
                table: "ResourceLink",
                column: "DatasetContactPointsId",
                principalTable: "Dataset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
