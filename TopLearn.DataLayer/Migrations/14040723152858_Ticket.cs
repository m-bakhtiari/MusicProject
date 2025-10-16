using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TopLearn.DataLayer.Migrations
{
    public partial class Ticket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConcertTickets",
                columns: table => new
                {
                    ConcertTicketId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 500, nullable: false),
                    LastName = table.Column<string>(maxLength: 500, nullable: false),
                    Mobile = table.Column<string>(maxLength: 15, nullable: false),
                    TicketCount = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConcertTickets", x => x.ConcertTicketId);
                });

            migrationBuilder.CreateTable(
                name: "ConcertTicketSeats",
                columns: table => new
                {
                    ConcertTicketSeatId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SeatNumber = table.Column<string>(maxLength: 10, nullable: false),
                    ConcertTicketId = table.Column<int>(nullable: false),
                    IsPay = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(maxLength: 1200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConcertTicketSeats", x => x.ConcertTicketSeatId);
                    table.ForeignKey(
                        name: "FK_ConcertTicketSeats_ConcertTickets_ConcertTicketId",
                        column: x => x.ConcertTicketId,
                        principalTable: "ConcertTickets",
                        principalColumn: "ConcertTicketId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConcertTicketSeats_ConcertTicketId",
                table: "ConcertTicketSeats",
                column: "ConcertTicketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConcertTicketSeats");

            migrationBuilder.DropTable(
                name: "ConcertTickets");
        }
    }
}
