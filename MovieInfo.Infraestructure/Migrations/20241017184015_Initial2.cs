using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieInfo.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubscriptionPreferences",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPreferences", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpirationDate", "StartDate" },
                values: new object[] { new DateTime(2029, 10, 17, 18, 40, 15, 208, DateTimeKind.Utc).AddTicks(8188), new DateTime(2024, 10, 17, 18, 40, 15, 208, DateTimeKind.Utc).AddTicks(8184) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ExpirationDate", "StartDate" },
                values: new object[] { new DateTime(2029, 10, 17, 18, 40, 15, 208, DateTimeKind.Utc).AddTicks(8195), new DateTime(2024, 10, 17, 18, 40, 15, 208, DateTimeKind.Utc).AddTicks(8195) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ExpirationDate", "StartDate" },
                values: new object[] { new DateTime(2029, 10, 17, 18, 40, 15, 208, DateTimeKind.Utc).AddTicks(8197), new DateTime(2024, 10, 17, 18, 40, 15, 208, DateTimeKind.Utc).AddTicks(8197) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ExpirationDate", "StartDate" },
                values: new object[] { new DateTime(2029, 10, 17, 18, 40, 15, 208, DateTimeKind.Utc).AddTicks(8199), new DateTime(2024, 10, 17, 18, 40, 15, 208, DateTimeKind.Utc).AddTicks(8198) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionPreferences");

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpirationDate", "StartDate" },
                values: new object[] { new DateTime(2029, 10, 16, 19, 20, 7, 112, DateTimeKind.Utc).AddTicks(4179), new DateTime(2024, 10, 16, 19, 20, 7, 112, DateTimeKind.Utc).AddTicks(4175) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ExpirationDate", "StartDate" },
                values: new object[] { new DateTime(2029, 10, 16, 19, 20, 7, 112, DateTimeKind.Utc).AddTicks(4187), new DateTime(2024, 10, 16, 19, 20, 7, 112, DateTimeKind.Utc).AddTicks(4186) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ExpirationDate", "StartDate" },
                values: new object[] { new DateTime(2029, 10, 16, 19, 20, 7, 112, DateTimeKind.Utc).AddTicks(4189), new DateTime(2024, 10, 16, 19, 20, 7, 112, DateTimeKind.Utc).AddTicks(4189) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ExpirationDate", "StartDate" },
                values: new object[] { new DateTime(2029, 10, 16, 19, 20, 7, 112, DateTimeKind.Utc).AddTicks(4191), new DateTime(2024, 10, 16, 19, 20, 7, 112, DateTimeKind.Utc).AddTicks(4191) });
        }
    }
}
