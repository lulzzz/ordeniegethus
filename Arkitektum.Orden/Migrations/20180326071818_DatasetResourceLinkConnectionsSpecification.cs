using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Arkitektum.Orden.Migrations
{
    public partial class DatasetResourceLinkConnectionsSpecification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceLink_Application_ApplicationId",
                table: "ResourceLink");

            migrationBuilder.DropForeignKey(
                name: "FK_ResourceLink_Dataset_DatasetId",
                table: "ResourceLink");

            migrationBuilder.DropForeignKey(
                name: "FK_ResourceLink_Dataset_DatasetId1",
                table: "ResourceLink");

            migrationBuilder.DropForeignKey(
                name: "FK_ResourceLink_Dataset_DatasetId2",
                table: "ResourceLink");

            migrationBuilder.DropIndex(
                name: "IX_ResourceLink_ApplicationId",
                table: "ResourceLink");

            migrationBuilder.RenameColumn(
                name: "DatasetId2",
                table: "ResourceLink",
                newName: "DatasetResourceLinkId");

            migrationBuilder.RenameColumn(
                name: "DatasetId1",
                table: "ResourceLink",
                newName: "DatasetLawReferenceId");

            migrationBuilder.RenameColumn(
                name: "DatasetId",
                table: "ResourceLink",
                newName: "DatasetContactPointsId");

            migrationBuilder.RenameIndex(
                name: "IX_ResourceLink_DatasetId2",
                table: "ResourceLink",
                newName: "IX_ResourceLink_DatasetResourceLinkId");

            migrationBuilder.RenameIndex(
                name: "IX_ResourceLink_DatasetId1",
                table: "ResourceLink",
                newName: "IX_ResourceLink_DatasetLawReferenceId");

            migrationBuilder.RenameIndex(
                name: "IX_ResourceLink_DatasetId",
                table: "ResourceLink",
                newName: "IX_ResourceLink_DatasetContactPointsId");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationId",
                table: "ResourceLink",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId1",
                table: "ResourceLink",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResourceLink_ApplicationId1",
                table: "ResourceLink",
                column: "ApplicationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceLink_Application_ApplicationId1",
                table: "ResourceLink",
                column: "ApplicationId1",
                principalTable: "Application",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceLink_Dataset_DatasetContactPointsId",
                table: "ResourceLink",
                column: "DatasetContactPointsId",
                principalTable: "Dataset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceLink_Dataset_DatasetLawReferenceId",
                table: "ResourceLink",
                column: "DatasetLawReferenceId",
                principalTable: "Dataset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceLink_Dataset_DatasetResourceLinkId",
                table: "ResourceLink",
                column: "DatasetResourceLinkId",
                principalTable: "Dataset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceLink_Application_ApplicationId1",
                table: "ResourceLink");

            migrationBuilder.DropForeignKey(
                name: "FK_ResourceLink_Dataset_DatasetContactPointsId",
                table: "ResourceLink");

            migrationBuilder.DropForeignKey(
                name: "FK_ResourceLink_Dataset_DatasetLawReferenceId",
                table: "ResourceLink");

            migrationBuilder.DropForeignKey(
                name: "FK_ResourceLink_Dataset_DatasetResourceLinkId",
                table: "ResourceLink");

            migrationBuilder.DropIndex(
                name: "IX_ResourceLink_ApplicationId1",
                table: "ResourceLink");

            migrationBuilder.DropColumn(
                name: "ApplicationId1",
                table: "ResourceLink");

            migrationBuilder.RenameColumn(
                name: "DatasetResourceLinkId",
                table: "ResourceLink",
                newName: "DatasetId2");

            migrationBuilder.RenameColumn(
                name: "DatasetLawReferenceId",
                table: "ResourceLink",
                newName: "DatasetId1");

            migrationBuilder.RenameColumn(
                name: "DatasetContactPointsId",
                table: "ResourceLink",
                newName: "DatasetId");

            migrationBuilder.RenameIndex(
                name: "IX_ResourceLink_DatasetResourceLinkId",
                table: "ResourceLink",
                newName: "IX_ResourceLink_DatasetId2");

            migrationBuilder.RenameIndex(
                name: "IX_ResourceLink_DatasetLawReferenceId",
                table: "ResourceLink",
                newName: "IX_ResourceLink_DatasetId1");

            migrationBuilder.RenameIndex(
                name: "IX_ResourceLink_DatasetContactPointsId",
                table: "ResourceLink",
                newName: "IX_ResourceLink_DatasetId");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationId",
                table: "ResourceLink",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResourceLink_ApplicationId",
                table: "ResourceLink",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceLink_Application_ApplicationId",
                table: "ResourceLink",
                column: "ApplicationId",
                principalTable: "Application",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceLink_Dataset_DatasetId",
                table: "ResourceLink",
                column: "DatasetId",
                principalTable: "Dataset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceLink_Dataset_DatasetId1",
                table: "ResourceLink",
                column: "DatasetId1",
                principalTable: "Dataset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceLink_Dataset_DatasetId2",
                table: "ResourceLink",
                column: "DatasetId2",
                principalTable: "Dataset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
