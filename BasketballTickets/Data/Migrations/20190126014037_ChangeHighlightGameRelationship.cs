using Microsoft.EntityFrameworkCore.Migrations;

namespace BasketballTickets.Data.Migrations
{
    public partial class ChangeHighlightGameRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HighlightGames_GameId",
                table: "HighlightGames");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "0bd94e8f-7ac0-48ad-98a9-cdd9d462e7c2", "47d877d9-69c9-4e57-a580-a4f5ac15ec3b" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8d991ba6-0c21-4bcd-bf7b-04495a1a5fa0", "f1cda0a3-dcf8-4c50-95ea-12c43aa019ca", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_HighlightGames_GameId",
                table: "HighlightGames",
                column: "GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HighlightGames_GameId",
                table: "HighlightGames");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "8d991ba6-0c21-4bcd-bf7b-04495a1a5fa0", "f1cda0a3-dcf8-4c50-95ea-12c43aa019ca" });

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
    }
}
