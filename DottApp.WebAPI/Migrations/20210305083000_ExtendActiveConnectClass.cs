using Microsoft.EntityFrameworkCore.Migrations;

namespace DottApp.WebAPI.Migrations
{
    public partial class ExtendActiveConnectClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAuth",
                table: "ActiveConnections",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "ActiveConnections",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAuth",
                table: "ActiveConnections");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "ActiveConnections");
        }
    }
}
