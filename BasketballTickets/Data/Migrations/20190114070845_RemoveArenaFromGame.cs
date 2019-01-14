using Microsoft.EntityFrameworkCore.Migrations;

namespace BasketballTickets.Data.Migrations
{
    public partial class RemoveArenaFromGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Arenas_ArenaId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_ArenaId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ArenaId",
                table: "Games");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArenaId",
                table: "Games",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_ArenaId",
                table: "Games",
                column: "ArenaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Arenas_ArenaId",
                table: "Games",
                column: "ArenaId",
                principalTable: "Arenas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
