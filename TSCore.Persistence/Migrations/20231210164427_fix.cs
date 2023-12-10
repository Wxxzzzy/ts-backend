using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TSCore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "TS",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                schema: "TS",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 10, 19, 44, 27, 193, DateTimeKind.Local).AddTicks(9328));

            migrationBuilder.UpdateData(
                schema: "TS",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 10, 19, 44, 27, 193, DateTimeKind.Local).AddTicks(9414));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "TS",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                schema: "TS",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 10, 18, 53, 47, 31, DateTimeKind.Local).AddTicks(926));

            migrationBuilder.UpdateData(
                schema: "TS",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 10, 18, 53, 47, 31, DateTimeKind.Local).AddTicks(1035));
        }
    }
}
