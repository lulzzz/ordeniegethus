using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Arkitektum.Orden.Migrations
{
    public partial class ApplicationIdOnResourceLinkIsOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceLink_Application_ApplicationId",
                table: "ResourceLink");

            migrationBuilder.DropForeignKey(
                name: "FK_ResourceLink_Dataset_DatasetConnectionPointsId",
                table: "ResourceLink");

            migrationBuilder.DropIndex(
                name: "IX_ResourceLink_DatasetConnectionPointsId",
                table: "ResourceLink");

            migrationBuilder.DropColumn(
                name: "DatasetConnectionPointsId",
                table: "ResourceLink");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationId",
                table: "ResourceLink",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceLink_Application_ApplicationId",
                table: "ResourceLink",
                column: "ApplicationId",
                principalTable: "Application",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceLink_Application_ApplicationId",
                table: "ResourceLink");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationId",
                table: "ResourceLink",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DatasetConnectionPointsId",
                table: "ResourceLink",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResourceLink_DatasetConnectionPointsId",
                table: "ResourceLink",
                column: "DatasetConnectionPointsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceLink_Application_ApplicationId",
                table: "ResourceLink",
                column: "ApplicationId",
                principalTable: "Application",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceLink_Dataset_DatasetConnectionPointsId",
                table: "ResourceLink",
                column: "DatasetConnectionPointsId",
                principalTable: "Dataset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
