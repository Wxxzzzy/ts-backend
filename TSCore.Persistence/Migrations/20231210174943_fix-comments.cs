using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TSCore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class fixcomments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "TS",
                table: "Comments",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                schema: "TS",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 10, 20, 49, 42, 705, DateTimeKind.Local).AddTicks(3592));

            migrationBuilder.UpdateData(
                schema: "TS",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 10, 20, 49, 42, 705, DateTimeKind.Local).AddTicks(3743));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "TS",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.UpdateData(
                schema: "TS",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 10, 20, 19, 1, 189, DateTimeKind.Local).AddTicks(9980));

            migrationBuilder.UpdateData(
                schema: "TS",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 10, 20, 19, 1, 190, DateTimeKind.Local).AddTicks(53));
        }
    }
}
