using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FınalProjectBeginning.Migrations
{
    /// <inheritdoc />
    public partial class kulsayfasisFollowed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFollowedByCurrentUser",
                table: "Kulsayfasis",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFollowedByCurrentUser",
                table: "Kulsayfasis");
        }
    }
}
