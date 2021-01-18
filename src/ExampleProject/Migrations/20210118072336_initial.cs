using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExampleProject.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    email = table.Column<string>(type: "varchar(64)", nullable: false),
                    password = table.Column<string>(type: "varchar(128)", nullable: false),
                    first_name = table.Column<string>(type: "varchar(64)", nullable: true),
                    last_name = table.Column<string>(type: "varchar(64)", nullable: true),
                    user_type = table.Column<string>(type: "varchar(16)", nullable: false, defaultValue: "User"),
                    create_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_information",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    personal_id = table.Column<string>(type: "varchar(64)", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    phone = table.Column<string>(type: "varchar(32)", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    create_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_information", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_information_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_report",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    ReportCreateTime = table.Column<string>(type: "varchar(32)", nullable: false),
                    UserReportType = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    create_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()")
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
                name: "IX_user_email",
                table: "user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_user_type",
                table: "user",
                column: "user_type")
                .Annotation("Npgsql:IndexMethod", "hash");

            migrationBuilder.CreateIndex(
                name: "IX_user_information_UserId",
                table: "user_information",
                column: "UserId",
                unique: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_information");

            migrationBuilder.DropTable(
                name: "user_report");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
