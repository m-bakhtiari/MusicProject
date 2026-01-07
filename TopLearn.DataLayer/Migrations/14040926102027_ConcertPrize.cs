using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TopLearn.DataLayer.Migrations
{
    public partial class ConcertPrize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConcertPrizeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConcertPrizeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConcertPrizes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserIp = table.Column<string>(nullable: false),
                    PrizeTypeId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConcertPrizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConcertPrizes_ConcertPrizeTypes_PrizeTypeId",
                        column: x => x.PrizeTypeId,
                        principalTable: "ConcertPrizeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConcertPrizes_PrizeTypeId",
                table: "ConcertPrizes",
                column: "PrizeTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConcertPrizes");

            migrationBuilder.DropTable(
                name: "ConcertPrizeTypes");
        }
    }
}
