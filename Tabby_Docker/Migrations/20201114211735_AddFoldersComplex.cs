using Microsoft.EntityFrameworkCore.Migrations;

namespace Tabby_Docker.Migrations
{
    public partial class AddFoldersComplex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FolderID",
                table: "Bookmark",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Folder",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folder", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookmark_FolderID",
                table: "Bookmark",
                column: "FolderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmark_Folder_FolderID",
                table: "Bookmark",
                column: "FolderID",
                principalTable: "Folder",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmark_Folder_FolderID",
                table: "Bookmark");

            migrationBuilder.DropTable(
                name: "Folder");

            migrationBuilder.DropIndex(
                name: "IX_Bookmark_FolderID",
                table: "Bookmark");

            migrationBuilder.DropColumn(
                name: "FolderID",
                table: "Bookmark");
        }
    }
}
