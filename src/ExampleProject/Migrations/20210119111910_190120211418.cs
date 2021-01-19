using Microsoft.EntityFrameworkCore.Migrations;

namespace ExampleProject.Migrations
{
    public partial class _190120211418 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReportCreateTime",
                table: "report",
                newName: "report_create_date");

            migrationBuilder.AddColumn<string>(
                name: "location",
                table: "report",
                type: "varchar(64)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "location",
                table: "report");

            migrationBuilder.RenameColumn(
                name: "report_create_date",
                table: "report",
                newName: "ReportCreateTime");
        }
    }
}
