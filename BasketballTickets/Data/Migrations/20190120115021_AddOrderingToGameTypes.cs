using Microsoft.EntityFrameworkCore.Migrations;

namespace BasketballTickets.Data.Migrations
{
    public partial class AddOrderingToGameTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "0a5aff60-d8c8-4924-b8ad-3a84c669cc7c", "b94d4fd5-25e3-42ff-9198-0b1e80d7d1d9" });

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "GameTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "85e20e40-4b97-4c43-9b29-6548c0ecbb03", "b9fe3043-c93c-4b01-9cea-8b357ee1d694", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "85e20e40-4b97-4c43-9b29-6548c0ecbb03", "b9fe3043-c93c-4b01-9cea-8b357ee1d694" });

            migrationBuilder.DropColumn(
                name: "Order",
                table: "GameTypes");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0a5aff60-d8c8-4924-b8ad-3a84c669cc7c", "b94d4fd5-25e3-42ff-9198-0b1e80d7d1d9", "Admin", "ADMIN" });
        }
    }
}
