using Microsoft.EntityFrameworkCore.Migrations;

namespace BasketballTickets.Data.Migrations
{
    public partial class RenameTeamForeignKeyColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Teams_TeamId",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "Games",
                newName: "HomeTeamId");

            migrationBuilder.RenameIndex(
                name: "IX_Games_TeamId",
                table: "Games",
                newName: "IX_Games_HomeTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Teams_HomeTeamId",
                table: "Games",
                column: "HomeTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Teams_HomeTeamId",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "HomeTeamId",
                table: "Games",
                newName: "TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_Games_HomeTeamId",
                table: "Games",
                newName: "IX_Games_TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Teams_TeamId",
                table: "Games",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
