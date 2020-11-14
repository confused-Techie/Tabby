using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tabby_Docker.Migrations
{
    public partial class AddFaviconString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Favicon",
                table: "Bookmark");

            migrationBuilder.AddColumn<string>(
                name: "FaviconLoc",
                table: "Bookmark",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FaviconLoc",
                table: "Bookmark");

            migrationBuilder.AddColumn<byte[]>(
                name: "Favicon",
                table: "Bookmark",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
