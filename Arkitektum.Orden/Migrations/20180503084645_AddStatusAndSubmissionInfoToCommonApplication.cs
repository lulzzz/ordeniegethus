using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Arkitektum.Orden.Migrations
{
    public partial class AddStatusAndSubmissionInfoToCommonApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "CommonApplications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubmittedByOrganizationId",
                table: "CommonApplications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubmittedByUserId",
                table: "CommonApplications",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommonApplications_SubmittedByOrganizationId",
                table: "CommonApplications",
                column: "SubmittedByOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_CommonApplications_SubmittedByUserId",
                table: "CommonApplications",
                column: "SubmittedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommonApplications_Organization_SubmittedByOrganizationId",
                table: "CommonApplications",
                column: "SubmittedByOrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommonApplications_AspNetUsers_SubmittedByUserId",
                table: "CommonApplications",
                column: "SubmittedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommonApplications_Organization_SubmittedByOrganizationId",
                table: "CommonApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_CommonApplications_AspNetUsers_SubmittedByUserId",
                table: "CommonApplications");

            migrationBuilder.DropIndex(
                name: "IX_CommonApplications_SubmittedByOrganizationId",
                table: "CommonApplications");

            migrationBuilder.DropIndex(
                name: "IX_CommonApplications_SubmittedByUserId",
                table: "CommonApplications");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "CommonApplications");

            migrationBuilder.DropColumn(
                name: "SubmittedByOrganizationId",
                table: "CommonApplications");

            migrationBuilder.DropColumn(
                name: "SubmittedByUserId",
                table: "CommonApplications");
        }
    }
}
