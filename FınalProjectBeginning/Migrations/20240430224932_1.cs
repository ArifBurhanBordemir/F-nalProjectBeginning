using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FınalProjectBeginning.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_CetUserId",
                table: "Events");

            migrationBuilder.AlterColumn<string>(
                name: "CetUserId",
                table: "Events",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "ReadCount",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_CetUserId",
                table: "Events",
                column: "CetUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_CetUserId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ReadCount",
                table: "Events");

            migrationBuilder.AlterColumn<string>(
                name: "CetUserId",
                table: "Events",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_CetUserId",
                table: "Events",
                column: "CetUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
