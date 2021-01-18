using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExampleProject.Migrations
{
    public partial class _180120211429 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_report_ReportID",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_information_UserId",
                table: "user_information");

            migrationBuilder.DropIndex(
                name: "IX_user_ReportID",
                table: "user");

            migrationBuilder.DropColumn(
                name: "ReportID",
                table: "user");

            migrationBuilder.CreateIndex(
                name: "IX_user_information_UserId",
                table: "user_information",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_user_information_UserId",
                table: "user_information");

            migrationBuilder.AddColumn<Guid>(
                name: "ReportID",
                table: "user",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_information_UserId",
                table: "user_information",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_ReportID",
                table: "user",
                column: "ReportID");

            migrationBuilder.AddForeignKey(
                name: "FK_user_report_ReportID",
                table: "user",
                column: "ReportID",
                principalTable: "report",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
