using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FınalProjectBeginning.Migrations
{
    /// <inheritdoc />
    public partial class postImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Posts");
        }
    }
}
