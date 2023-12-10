using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TSCore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TS003addedsalttousers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Salt",
                schema: "TS",
                table: "Users",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "TS",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 10, 14, 10, 20, 107, DateTimeKind.Local).AddTicks(4135));

            migrationBuilder.UpdateData(
                schema: "TS",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 10, 14, 10, 20, 107, DateTimeKind.Local).AddTicks(4188));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                schema: "TS",
                table: "Users");

            migrationBuilder.UpdateData(
                schema: "TS",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 10, 13, 56, 3, 208, DateTimeKind.Local).AddTicks(7203));

            migrationBuilder.UpdateData(
                schema: "TS",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 10, 13, 56, 3, 208, DateTimeKind.Local).AddTicks(7244));
        }
    }
}
