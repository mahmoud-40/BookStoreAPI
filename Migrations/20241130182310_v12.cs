using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class v12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "be54d72f-d67a-466d-aa31-07c6014f5b9e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dddf9ea7-bd00-427c-98f4-e0096dc39613");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "42fbc60d-4ec1-4bd0-9f7b-cca3586e174a", null, "admin", "ADMIN" },
                    { "bf51cb2b-6b80-47fc-a82b-9a61b0e90409", null, "customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "be54d72f-d67a-466d-aa31-07c6014f5b9e", null, "customer", "CUSTOMER" },
                    { "dddf9ea7-bd00-427c-98f4-e0096dc39613", null, "admin", "ADMIN" }
                });
        }
    }
}
