using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberTech.Migrations
{
    /// <inheritdoc />
    public partial class LastMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e2d55110-6114-4f30-8c78-e07eb6861a8b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5d4e6257-c5f9-4c39-8404-525c8e864059", "AQAAAAEAACcQAAAAECDC1zQxmtos3I/iwDWxHN3rdg0G0BMgbJuWm8dhLdzezzSjWo2QzqelOcuK73h9eg==", "62584b4a-5929-452e-bf58-a251ca73e3be" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e2d55110-6114-4f30-8c78-e07eb6861a8b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6344ab37-f747-4d20-a5ac-f73fc8e9a33e", "AQAAAAIAAYagAAAAENv6YsuPQNdT9PbK9bwYYAepQYvKUJXnFygGEpfGL0YGj565bE3V5ksrsS4g5c6Wug==", "22fccc80-fa33-46da-82af-03e485323516" });
        }
    }
}
