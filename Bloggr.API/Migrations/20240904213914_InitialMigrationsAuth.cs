using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bloggr.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationsAuth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a699b2de-7d03-4515-913f-6a722e3f272e", "a699b2de-7d03-4515-913f-6a722e3f272e", "Admin", "ADMIN" },
                    { "d4162388-82fb-43e8-a918-270d38762156", "d4162388-82fb-43e8-a918-270d38762156", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a699b2de-7d03-4515-913f-6a722e3f272e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4162388-82fb-43e8-a918-270d38762156");
        }
    }
}
