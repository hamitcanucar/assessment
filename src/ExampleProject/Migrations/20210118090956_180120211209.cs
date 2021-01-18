using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExampleProject.Migrations
{
    public partial class _180120211209 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_report");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "user_information");

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "user",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReportID",
                table: "user",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "report",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    ReportCreateTime = table.Column<string>(type: "varchar(32)", nullable: false),
                    ReportType = table.Column<int>(type: "integer", nullable: false),
                    create_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_report", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_ReportID",
                table: "user",
                column: "ReportID");

            migrationBuilder.CreateIndex(
                name: "IX_report_ReportType",
                table: "report",
                column: "ReportType")
                .Annotation("Npgsql:IndexMethod", "hash");

            migrationBuilder.AddForeignKey(
                name: "FK_user_report_ReportID",
                table: "user",
                column: "ReportID",
                principalTable: "report",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_report_ReportID",
                table: "user");

            migrationBuilder.DropTable(
                name: "report");

            migrationBuilder.DropIndex(
                name: "IX_user_ReportID",
                table: "user");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "user");

            migrationBuilder.DropColumn(
                name: "ReportID",
                table: "user");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "user_information",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "user_report",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    create_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    ReportCreateTime = table.Column<string>(type: "varchar(32)", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserReportType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_report", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_report_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_report_UserId",
                table: "user_report",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_report_UserReportType",
                table: "user_report",
                column: "UserReportType")
                .Annotation("Npgsql:IndexMethod", "hash");
        }
    }
}
