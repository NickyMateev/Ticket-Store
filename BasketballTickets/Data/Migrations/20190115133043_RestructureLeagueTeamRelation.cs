using Microsoft.EntityFrameworkCore.Migrations;

namespace BasketballTickets.Data.Migrations
{
    public partial class RestructureLeagueTeamRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Leagues_LeagueId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_LeagueId",
                table: "Games");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "a60e58e2-8baa-4302-9d5f-b0d3a85068bf", "f4256356-fd08-4f7f-b70a-65069233ae10" });

            migrationBuilder.DropColumn(
                name: "LeagueId",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "LeagueId",
                table: "Teams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0a5aff60-d8c8-4924-b8ad-3a84c669cc7c", "b94d4fd5-25e3-42ff-9198-0b1e80d7d1d9", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_LeagueId",
                table: "Teams",
                column: "LeagueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Leagues_LeagueId",
                table: "Teams",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Leagues_LeagueId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_LeagueId",
                table: "Teams");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "0a5aff60-d8c8-4924-b8ad-3a84c669cc7c", "b94d4fd5-25e3-42ff-9198-0b1e80d7d1d9" });

            migrationBuilder.DropColumn(
                name: "LeagueId",
                table: "Teams");

            migrationBuilder.AddColumn<int>(
                name: "LeagueId",
                table: "Games",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a60e58e2-8baa-4302-9d5f-b0d3a85068bf", "f4256356-fd08-4f7f-b70a-65069233ae10", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_Games_LeagueId",
                table: "Games",
                column: "LeagueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Leagues_LeagueId",
                table: "Games",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
