using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class v13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "42fbc60d-4ec1-4bd0-9f7b-cca3586e174a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bf51cb2b-6b80-47fc-a82b-9a61b0e90409");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "136b2662-dcb9-4444-9d53-771af3182145", null, "admin", "ADMIN" },
                    { "7b5f73db-609c-4c67-a512-5db2091355a1", null, "customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "136b2662-dcb9-4444-9d53-771af3182145");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7b5f73db-609c-4c67-a512-5db2091355a1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "42fbc60d-4ec1-4bd0-9f7b-cca3586e174a", null, "admin", "ADMIN" },
                    { "bf51cb2b-6b80-47fc-a82b-9a61b0e90409", null, "customer", "CUSTOMER" }
                });
        }
    }
}
