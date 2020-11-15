using Microsoft.EntityFrameworkCore.Migrations;

namespace Tabby_Docker.Migrations
{
    public partial class AddFoldersSimple : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmark_Folder_FolderID",
                table: "Bookmark");

            migrationBuilder.DropIndex(
                name: "IX_Bookmark_FolderID",
                table: "Bookmark");

            migrationBuilder.DropColumn(
                name: "FolderID",
                table: "Bookmark");

            migrationBuilder.CreateTable(
                name: "FolderBookmarks",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookmarkID = table.Column<int>(nullable: false),
                    FolderID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolderBookmarks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FolderBookmarks_Folder_FolderID",
                        column: x => x.FolderID,
                        principalTable: "Folder",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FolderBookmarks_FolderID",
                table: "FolderBookmarks",
                column: "FolderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FolderBookmarks");

            migrationBuilder.AddColumn<int>(
                name: "FolderID",
                table: "Bookmark",
                type: "int",
                nullable: true);

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
    }
}
