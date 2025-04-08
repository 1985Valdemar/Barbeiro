using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberTech.Migrations
{
    /// <inheritdoc />
    public partial class DetectChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e2d55110-6114-4f30-8c78-e07eb6861a8b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e323b3e7-4a68-44ce-8e8a-71e0a07f173d", "AQAAAAIAAYagAAAAEFu8gFXi3M2ef0P9QRl5ZYl+TYM6I5gsSs39Y1qEPjEajJvUdWYQDPq3htaLW1Pu9w==", "78601793-5eb4-4adb-aa40-1dd360838386" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e2d55110-6114-4f30-8c78-e07eb6861a8b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "55529a94-6e43-4c84-9071-85f48c592d73", "AQAAAAIAAYagAAAAEOmSKBykyO5G254yyteJoR/MQW7sPvliwNYRXCriqUHxWHQYDc3H+yDuIPb+3BULOQ==", "c3f32d79-b739-4df9-bec8-dae1ebb574af" });
        }
    }
}
