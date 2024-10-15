using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieInfo.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Drama" },
                    { 2, "Comedia" },
                    { 3, "Aventura" },
                    { 4, "Ciencia Ficción" },
                    { 5, "Terror" },
                    { 6, "Acción" }
                });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpirationDate", "StartDate" },
                values: new object[] { new DateTime(2029, 10, 15, 18, 16, 40, 875, DateTimeKind.Utc).AddTicks(96), new DateTime(2024, 10, 15, 18, 16, 40, 875, DateTimeKind.Utc).AddTicks(93) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ExpirationDate", "StartDate" },
                values: new object[] { new DateTime(2029, 10, 15, 18, 16, 40, 875, DateTimeKind.Utc).AddTicks(104), new DateTime(2024, 10, 15, 18, 16, 40, 875, DateTimeKind.Utc).AddTicks(103) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ExpirationDate", "StartDate" },
                values: new object[] { new DateTime(2029, 10, 15, 18, 16, 40, 875, DateTimeKind.Utc).AddTicks(105), new DateTime(2024, 10, 15, 18, 16, 40, 875, DateTimeKind.Utc).AddTicks(105) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Email", "Name" },
                values: new object[] { "usuario1@gmail.com", "Usuario1" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "RefreshToken", "RoleId" },
                values: new object[] { 4, "usuario2@gmail.com", "Usuario2", "Usuario2", null, 1 });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "ExpirationDate", "StartDate", "State", "UserId" },
                values: new object[] { 4, new DateTime(2029, 10, 15, 18, 16, 40, 875, DateTimeKind.Utc).AddTicks(107), new DateTime(2024, 10, 15, 18, 16, 40, 875, DateTimeKind.Utc).AddTicks(107), 1, 4 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Email", "Name" },
                values: new object[] { "usuario@gmail.com", "Usuario" });
        }
    }
}
