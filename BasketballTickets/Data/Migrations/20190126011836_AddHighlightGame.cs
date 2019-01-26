using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BasketballTickets.Data.Migrations
{
    public partial class AddHighlightGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "68b6f447-f5e3-479b-af41-91b27b861519", "fe13bb3e-c2f4-43f7-8b71-1356d0aa2ffd" });

            migrationBuilder.CreateTable(
                name: "HighlightGames",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    IsBanner = table.Column<bool>(nullable: false),
                    GameId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HighlightGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HighlightGames_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0bd94e8f-7ac0-48ad-98a9-cdd9d462e7c2", "47d877d9-69c9-4e57-a580-a4f5ac15ec3b", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_HighlightGames_GameId",
                table: "HighlightGames",
                column: "GameId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HighlightGames");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "0bd94e8f-7ac0-48ad-98a9-cdd9d462e7c2", "47d877d9-69c9-4e57-a580-a4f5ac15ec3b" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "68b6f447-f5e3-479b-af41-91b27b861519", "fe13bb3e-c2f4-43f7-8b71-1356d0aa2ffd", "Admin", "ADMIN" });
        }
    }
}
