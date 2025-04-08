using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberTech.Migrations
{
    /// <inheritdoc />
    public partial class CheckPendingChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e2d55110-6114-4f30-8c78-e07eb6861a8b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6344ab37-f747-4d20-a5ac-f73fc8e9a33e", "AQAAAAIAAYagAAAAENv6YsuPQNdT9PbK9bwYYAepQYvKUJXnFygGEpfGL0YGj565bE3V5ksrsS4g5c6Wug==", "22fccc80-fa33-46da-82af-03e485323516" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e2d55110-6114-4f30-8c78-e07eb6861a8b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e323b3e7-4a68-44ce-8e8a-71e0a07f173d", "AQAAAAIAAYagAAAAEFu8gFXi3M2ef0P9QRl5ZYl+TYM6I5gsSs39Y1qEPjEajJvUdWYQDPq3htaLW1Pu9w==", "78601793-5eb4-4adb-aa40-1dd360838386" });
        }
    }
}
