using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TSCore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class commetsadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comments",
                schema: "TS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalSchema: "TS",
                        principalTable: "Tickets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Users_SenderId",
                        column: x => x.SenderId,
                        principalSchema: "TS",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Comments_SenderId",
                schema: "TS",
                table: "Comments",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_TicketId",
                schema: "TS",
                table: "Comments",
                column: "TicketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments",
                schema: "TS");

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
    }
}
