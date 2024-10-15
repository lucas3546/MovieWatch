using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieInfo.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Movies",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpirationDate", "StartDate" },
                values: new object[] { new DateTime(2029, 10, 14, 22, 21, 55, 282, DateTimeKind.Utc).AddTicks(2574), new DateTime(2024, 10, 14, 22, 21, 55, 282, DateTimeKind.Utc).AddTicks(2572) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ExpirationDate", "StartDate" },
                values: new object[] { new DateTime(2029, 10, 14, 22, 21, 55, 282, DateTimeKind.Utc).AddTicks(2581), new DateTime(2024, 10, 14, 22, 21, 55, 282, DateTimeKind.Utc).AddTicks(2581) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ExpirationDate", "StartDate" },
                values: new object[] { new DateTime(2029, 10, 14, 22, 21, 55, 282, DateTimeKind.Utc).AddTicks(2583), new DateTime(2024, 10, 14, 22, 21, 55, 282, DateTimeKind.Utc).AddTicks(2583) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "Movies");

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpirationDate", "StartDate" },
                values: new object[] { new DateTime(2029, 10, 10, 19, 17, 28, 141, DateTimeKind.Utc).AddTicks(7696), new DateTime(2024, 10, 10, 19, 17, 28, 141, DateTimeKind.Utc).AddTicks(7693) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ExpirationDate", "StartDate" },
                values: new object[] { new DateTime(2029, 10, 10, 19, 17, 28, 141, DateTimeKind.Utc).AddTicks(7704), new DateTime(2024, 10, 10, 19, 17, 28, 141, DateTimeKind.Utc).AddTicks(7703) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ExpirationDate", "StartDate" },
                values: new object[] { new DateTime(2029, 10, 10, 19, 17, 28, 141, DateTimeKind.Utc).AddTicks(7705), new DateTime(2024, 10, 10, 19, 17, 28, 141, DateTimeKind.Utc).AddTicks(7705) });
        }
    }
}
