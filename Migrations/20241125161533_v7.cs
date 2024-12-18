using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class v7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d156ef26-3d3e-4daa-805f-3f0e0f5df543");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fac9a68c-78aa-4e1f-8ea4-dc090b7a2e35");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "93dc5494-95c4-4c67-898e-4db4d05aa836", null, "customer", "CUSTOMER" },
                    { "9adb344b-1929-4583-9dd9-c724819e79bf", null, "admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93dc5494-95c4-4c67-898e-4db4d05aa836");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9adb344b-1929-4583-9dd9-c724819e79bf");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d156ef26-3d3e-4daa-805f-3f0e0f5df543", null, "admin", "ADMIN" },
                    { "fac9a68c-78aa-4e1f-8ea4-dc090b7a2e35", null, "customer", "CUSTOMER" }
                });
        }
    }
}
