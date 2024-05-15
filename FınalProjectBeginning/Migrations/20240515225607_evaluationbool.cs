using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FınalProjectBeginning.Migrations
{
    /// <inheritdoc />
    public partial class evaluationbool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Like",
                table: "Evaluations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Like",
                table: "Evaluations");
        }
    }
}
