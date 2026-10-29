using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace wt_lab6_6minimalapi_sudinko.Migrations
{
    /// <inheritdoc />
    public partial class SeedFlights : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "Id", "CreatedAt", "Destination", "IsAvailable", "Number", "Origin", "Price" },
                values: new object[,]
                {
                    { 101, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Warsaw", true, "B2-101", "Minsk", 150m },
                    { 102, new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Vilnius", true, "B2-202", "Minsk", 80m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 102);
        }
    }
}
