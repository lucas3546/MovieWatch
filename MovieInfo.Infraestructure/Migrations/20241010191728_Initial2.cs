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
            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Films");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "TEXT",
                nullable: true);

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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "RefreshToken",
                value: null);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "RefreshToken",
                value: null);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "RefreshToken",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "Films",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Duration = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Films", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FilmId = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ExpirationDate", "StartDate" },
                values: new object[] { new DateTime(2029, 10, 9, 22, 28, 55, 597, DateTimeKind.Utc).AddTicks(1632), new DateTime(2024, 10, 9, 22, 28, 55, 597, DateTimeKind.Utc).AddTicks(1629) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ExpirationDate", "StartDate" },
                values: new object[] { new DateTime(2029, 10, 9, 22, 28, 55, 597, DateTimeKind.Utc).AddTicks(1639), new DateTime(2024, 10, 9, 22, 28, 55, 597, DateTimeKind.Utc).AddTicks(1638) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ExpirationDate", "StartDate" },
                values: new object[] { new DateTime(2029, 10, 9, 22, 28, 55, 597, DateTimeKind.Utc).AddTicks(1640), new DateTime(2024, 10, 9, 22, 28, 55, 597, DateTimeKind.Utc).AddTicks(1640) });

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_FilmId",
                table: "Ratings",
                column: "FilmId");
        }
    }
}
