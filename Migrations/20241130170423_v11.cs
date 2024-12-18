using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class v11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8a86380a-7570-48b8-b05c-976bd00e1af4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a86396c0-bc3c-47a1-bc96-b09c83d917ed");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "be54d72f-d67a-466d-aa31-07c6014f5b9e", null, "customer", "CUSTOMER" },
                    { "dddf9ea7-bd00-427c-98f4-e0096dc39613", null, "admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "8a86380a-7570-48b8-b05c-976bd00e1af4", null, "customer", "CUSTOMER" },
                    { "a86396c0-bc3c-47a1-bc96-b09c83d917ed", null, "admin", "ADMIN" }
                });
        }
    }
}
