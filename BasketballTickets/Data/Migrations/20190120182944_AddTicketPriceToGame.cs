using Microsoft.EntityFrameworkCore.Migrations;

namespace BasketballTickets.Data.Migrations
{
    public partial class AddTicketPriceToGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "85e20e40-4b97-4c43-9b29-6548c0ecbb03", "b9fe3043-c93c-4b01-9cea-8b357ee1d694" });

            migrationBuilder.AddColumn<decimal>(
                name: "TicketPrice",
                table: "Games",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a2b38242-6622-4249-913f-b17ff8053bbf", "ef1def3a-0feb-4460-b78a-0b5cec783822", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "a2b38242-6622-4249-913f-b17ff8053bbf", "ef1def3a-0feb-4460-b78a-0b5cec783822" });

            migrationBuilder.DropColumn(
                name: "TicketPrice",
                table: "Games");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "85e20e40-4b97-4c43-9b29-6548c0ecbb03", "b9fe3043-c93c-4b01-9cea-8b357ee1d694", "Admin", "ADMIN" });
        }
    }
}
