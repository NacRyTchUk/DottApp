using Microsoft.EntityFrameworkCore.Migrations;

namespace DottApp.WebAPI.Migrations
{
    public partial class AddAesKeyToConnections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AesKey",
                table: "ActiveConnections",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AesKey",
                table: "ActiveConnections");
        }
    }
}
