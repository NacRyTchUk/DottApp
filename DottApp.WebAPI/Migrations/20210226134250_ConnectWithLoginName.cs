using Microsoft.EntityFrameworkCore.Migrations;

namespace DottApp.WebAPI.Migrations
{
    public partial class ConnectWithLoginName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LoginName",
                table: "ActiveConnections",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoginName",
                table: "ActiveConnections");
        }
    }
}
