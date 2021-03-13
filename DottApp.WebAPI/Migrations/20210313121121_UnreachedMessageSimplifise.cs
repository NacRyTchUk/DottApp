using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DottApp.WebAPI.Migrations
{
    public partial class UnreachedMessageSimplifise : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttachmentId",
                table: "UnreachedMessages");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "UnreachedMessages");

            migrationBuilder.DropColumn(
                name: "IsEdited",
                table: "UnreachedMessages");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "UnreachedMessages");

            migrationBuilder.DropColumn(
                name: "SendTime",
                table: "UnreachedMessages");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "UnreachedMessages");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "UnreachedMessages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttachmentId",
                table: "UnreachedMessages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ChatId",
                table: "UnreachedMessages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsEdited",
                table: "UnreachedMessages",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "UnreachedMessages",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "SendTime",
                table: "UnreachedMessages",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SenderId",
                table: "UnreachedMessages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "UnreachedMessages",
                type: "TEXT",
                nullable: true);
        }
    }
}
