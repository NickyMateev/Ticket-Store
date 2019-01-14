using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BasketballTickets.Data.Migrations
{
    public partial class AddGameTypesAndArenas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArenaId",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GameTypeId",
                table: "Games",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Arenas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Capacity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arenas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_ArenaId",
                table: "Games",
                column: "ArenaId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_GameTypeId",
                table: "Games",
                column: "GameTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Arenas_ArenaId",
                table: "Games",
                column: "ArenaId",
                principalTable: "Arenas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GameTypes_GameTypeId",
                table: "Games",
                column: "GameTypeId",
                principalTable: "GameTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Arenas_ArenaId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_GameTypes_GameTypeId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "Arenas");

            migrationBuilder.DropTable(
                name: "GameTypes");

            migrationBuilder.DropIndex(
                name: "IX_Games_ArenaId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_GameTypeId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ArenaId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "GameTypeId",
                table: "Games");
        }
    }
}
