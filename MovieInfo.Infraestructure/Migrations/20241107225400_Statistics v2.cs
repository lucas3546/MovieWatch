using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieInfo.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Statisticsv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpirationDate", "StartDate" },
                values: new object[] { new DateTime(2029, 11, 7, 22, 53, 59, 846, DateTimeKind.Utc).AddTicks(1844), new DateTime(2024, 11, 7, 22, 53, 59, 846, DateTimeKind.Utc).AddTicks(1843) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ExpirationDate", "StartDate" },
                values: new object[] { new DateTime(2029, 11, 7, 22, 53, 59, 846, DateTimeKind.Utc).AddTicks(1857), new DateTime(2024, 11, 7, 22, 53, 59, 846, DateTimeKind.Utc).AddTicks(1856) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ExpirationDate", "StartDate" },
                values: new object[] { new DateTime(2029, 11, 7, 22, 53, 59, 846, DateTimeKind.Utc).AddTicks(1859), new DateTime(2024, 11, 7, 22, 53, 59, 846, DateTimeKind.Utc).AddTicks(1859) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ExpirationDate", "StartDate" },
                values: new object[] { new DateTime(2029, 11, 7, 22, 53, 59, 846, DateTimeKind.Utc).AddTicks(1861), new DateTime(2024, 11, 7, 22, 53, 59, 846, DateTimeKind.Utc).AddTicks(1861) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegistrationUser",
                value: new DateTime(2024, 10, 23, 22, 53, 59, 846, DateTimeKind.Utc).AddTicks(1349));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegistrationUser",
                value: new DateTime(2024, 11, 6, 22, 53, 59, 846, DateTimeKind.Utc).AddTicks(1377));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegistrationUser",
                value: new DateTime(2024, 9, 28, 22, 53, 59, 846, DateTimeKind.Utc).AddTicks(1608));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "RegistrationUser",
                value: new DateTime(2024, 10, 30, 22, 53, 59, 846, DateTimeKind.Utc).AddTicks(1614));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpirationDate", "StartDate" },
                values: new object[] { new DateTime(2029, 11, 7, 21, 34, 13, 321, DateTimeKind.Utc).AddTicks(2939), new DateTime(2024, 11, 7, 21, 34, 13, 321, DateTimeKind.Utc).AddTicks(2935) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ExpirationDate", "StartDate" },
                values: new object[] { new DateTime(2029, 11, 7, 21, 34, 13, 321, DateTimeKind.Utc).AddTicks(2951), new DateTime(2024, 11, 7, 21, 34, 13, 321, DateTimeKind.Utc).AddTicks(2951) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ExpirationDate", "StartDate" },
                values: new object[] { new DateTime(2029, 11, 7, 21, 34, 13, 321, DateTimeKind.Utc).AddTicks(2954), new DateTime(2024, 11, 7, 21, 34, 13, 321, DateTimeKind.Utc).AddTicks(2953) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ExpirationDate", "StartDate" },
                values: new object[] { new DateTime(2029, 11, 7, 21, 34, 13, 321, DateTimeKind.Utc).AddTicks(2956), new DateTime(2024, 11, 7, 21, 34, 13, 321, DateTimeKind.Utc).AddTicks(2956) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegistrationUser",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegistrationUser",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegistrationUser",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "RegistrationUser",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
