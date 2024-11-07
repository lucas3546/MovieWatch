using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieInfo.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Statistics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationUser",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistrationUser",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpirationDate", "StartDate" },
                values: new object[] { new DateTime(2029, 11, 2, 4, 58, 4, 71, DateTimeKind.Utc).AddTicks(5407), new DateTime(2024, 11, 2, 4, 58, 4, 71, DateTimeKind.Utc).AddTicks(5404) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ExpirationDate", "StartDate" },
                values: new object[] { new DateTime(2029, 11, 2, 4, 58, 4, 71, DateTimeKind.Utc).AddTicks(5415), new DateTime(2024, 11, 2, 4, 58, 4, 71, DateTimeKind.Utc).AddTicks(5414) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ExpirationDate", "StartDate" },
                values: new object[] { new DateTime(2029, 11, 2, 4, 58, 4, 71, DateTimeKind.Utc).AddTicks(5416), new DateTime(2024, 11, 2, 4, 58, 4, 71, DateTimeKind.Utc).AddTicks(5416) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ExpirationDate", "StartDate" },
                values: new object[] { new DateTime(2029, 11, 2, 4, 58, 4, 71, DateTimeKind.Utc).AddTicks(5418), new DateTime(2024, 11, 2, 4, 58, 4, 71, DateTimeKind.Utc).AddTicks(5417) });
        }
    }
}
