using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FınalProjectBeginning.Migrations
{
    /// <inheritdoc />
    public partial class takiptakipci : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Takip_Takipçis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TakipEdenUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TakipEdilenKişiId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Takip_Takipçis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Takip_Takipçis_AspNetUsers_TakipEdenUserId",
                        column: x => x.TakipEdenUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Takip_Takipçis_AspNetUsers_TakipEdilenKişiId",
                        column: x => x.TakipEdilenKişiId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Takip_Takipçis_TakipEdenUserId",
                table: "Takip_Takipçis",
                column: "TakipEdenUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Takip_Takipçis_TakipEdilenKişiId",
                table: "Takip_Takipçis",
                column: "TakipEdilenKişiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Takip_Takipçis");
        }
    }
}
