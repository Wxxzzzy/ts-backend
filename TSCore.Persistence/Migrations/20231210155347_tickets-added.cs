using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TSCore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ticketsadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tickets",
                schema: "TS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketTitle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    TicketStatus = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    TicketCreatorId = table.Column<int>(type: "int", nullable: false),
                    AssignedToId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Teams_TeamId",
                        column: x => x.TeamId,
                        principalSchema: "TS",
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Users_AssignedToId",
                        column: x => x.AssignedToId,
                        principalSchema: "TS",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_Users_TicketCreatorId",
                        column: x => x.TicketCreatorId,
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
                value: new DateTime(2023, 12, 10, 18, 53, 47, 31, DateTimeKind.Local).AddTicks(926));

            migrationBuilder.UpdateData(
                schema: "TS",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 10, 18, 53, 47, 31, DateTimeKind.Local).AddTicks(1035));

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AssignedToId",
                schema: "TS",
                table: "Tickets",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TeamId",
                schema: "TS",
                table: "Tickets",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketCreatorId",
                schema: "TS",
                table: "Tickets",
                column: "TicketCreatorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets",
                schema: "TS");

            migrationBuilder.UpdateData(
                schema: "TS",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 10, 15, 32, 39, 841, DateTimeKind.Local).AddTicks(3534));

            migrationBuilder.UpdateData(
                schema: "TS",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 12, 10, 15, 32, 39, 841, DateTimeKind.Local).AddTicks(3612));
        }
    }
}
