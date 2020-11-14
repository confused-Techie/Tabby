using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tabby_Docker.Migrations
{
    public partial class AddFavicon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Favicon",
                table: "Bookmark",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Favicon",
                table: "Bookmark");
        }
    }
}
