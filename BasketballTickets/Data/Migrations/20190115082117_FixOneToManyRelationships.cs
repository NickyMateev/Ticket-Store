using Microsoft.EntityFrameworkCore.Migrations;

namespace BasketballTickets.Data.Migrations
{
    public partial class FixOneToManyRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_GameTypes_GameTypeId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Teams_HomeTeamId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Leagues_LeagueId",
                table: "Games");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "5db25185-d5c3-452d-bd1e-49ad1da1a105", "1d9d242f-d4cf-4add-a9f8-29c2ab2517e2" });

            migrationBuilder.AlterColumn<int>(
                name: "LeagueId",
                table: "Games",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HomeTeamId",
                table: "Games",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GameTypeId",
                table: "Games",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "736e5057-8e89-453c-a86f-0c9dedf15443", "da9f7782-4c56-4cec-acd3-671258570f2f", "Admin", "ADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GameTypes_GameTypeId",
                table: "Games",
                column: "GameTypeId",
                principalTable: "GameTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Teams_HomeTeamId",
                table: "Games",
                column: "HomeTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Leagues_LeagueId",
                table: "Games",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_GameTypes_GameTypeId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Teams_HomeTeamId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Leagues_LeagueId",
                table: "Games");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "736e5057-8e89-453c-a86f-0c9dedf15443", "da9f7782-4c56-4cec-acd3-671258570f2f" });

            migrationBuilder.AlterColumn<int>(
                name: "LeagueId",
                table: "Games",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "HomeTeamId",
                table: "Games",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "GameTypeId",
                table: "Games",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5db25185-d5c3-452d-bd1e-49ad1da1a105", "1d9d242f-d4cf-4add-a9f8-29c2ab2517e2", "Admin", "ADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GameTypes_GameTypeId",
                table: "Games",
                column: "GameTypeId",
                principalTable: "GameTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Teams_HomeTeamId",
                table: "Games",
                column: "HomeTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Leagues_LeagueId",
                table: "Games",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
