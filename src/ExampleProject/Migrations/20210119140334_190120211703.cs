using Microsoft.EntityFrameworkCore.Migrations;

namespace ExampleProject.Migrations
{
    public partial class _190120211703 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "user_information");

            migrationBuilder.AddColumn<string>(
                name: "content",
                table: "user_information",
                type: "varchar(32)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "user_information",
                type: "varchar(32)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "location",
                table: "user_information",
                type: "varchar(32)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "content",
                table: "user_information");

            migrationBuilder.DropColumn(
                name: "email",
                table: "user_information");

            migrationBuilder.DropColumn(
                name: "location",
                table: "user_information");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "user_information",
                type: "text",
                nullable: true);
        }
    }
}
