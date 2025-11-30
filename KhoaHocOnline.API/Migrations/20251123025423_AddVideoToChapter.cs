using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KhoaHocOnline.API.Migrations
{
    /// <inheritdoc />
    public partial class AddVideoToChapter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Chapters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VideoURL",
                table: "Chapters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Chapters");

            migrationBuilder.DropColumn(
                name: "VideoURL",
                table: "Chapters");
        }
    }
}
