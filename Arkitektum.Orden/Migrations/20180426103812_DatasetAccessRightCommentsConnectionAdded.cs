using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Arkitektum.Orden.Migrations
{
    public partial class DatasetAccessRightCommentsConnectionAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessRightComment_Dataset_DatasetId",
                table: "AccessRightComment");

            migrationBuilder.AlterColumn<int>(
                name: "DatasetId",
                table: "AccessRightComment",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessRightComment_Dataset_DatasetId",
                table: "AccessRightComment",
                column: "DatasetId",
                principalTable: "Dataset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessRightComment_Dataset_DatasetId",
                table: "AccessRightComment");

            migrationBuilder.AlterColumn<int>(
                name: "DatasetId",
                table: "AccessRightComment",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_AccessRightComment_Dataset_DatasetId",
                table: "AccessRightComment",
                column: "DatasetId",
                principalTable: "Dataset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
