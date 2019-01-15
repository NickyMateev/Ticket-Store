using Microsoft.EntityFrameworkCore.Migrations;

namespace BasketballTickets.Data.Migrations
{
    public partial class ChangeArenaTeamRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Arenas_ArenaId",
                table: "Teams");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "3d7ca5d9-dbb5-44e1-acad-798bc8860d3f", "bedff451-b021-4cee-8f13-496246b16759" });

            migrationBuilder.AlterColumn<int>(
                name: "ArenaId",
                table: "Teams",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5db25185-d5c3-452d-bd1e-49ad1da1a105", "1d9d242f-d4cf-4add-a9f8-29c2ab2517e2", "Admin", "ADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Arenas_ArenaId",
                table: "Teams",
                column: "ArenaId",
                principalTable: "Arenas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Arenas_ArenaId",
                table: "Teams");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "5db25185-d5c3-452d-bd1e-49ad1da1a105", "1d9d242f-d4cf-4add-a9f8-29c2ab2517e2" });

            migrationBuilder.AlterColumn<int>(
                name: "ArenaId",
                table: "Teams",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3d7ca5d9-dbb5-44e1-acad-798bc8860d3f", "bedff451-b021-4cee-8f13-496246b16759", "Admin", "ADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Arenas_ArenaId",
                table: "Teams",
                column: "ArenaId",
                principalTable: "Arenas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
