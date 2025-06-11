using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TopLearn.DataLayer.Migrations
{
    public partial class Add_StudentConcert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentConcerts",
                columns: table => new
                {
                    StudentConcertId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 800, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ConcertDate = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentConcerts", x => x.StudentConcertId);
                });

            migrationBuilder.CreateTable(
                name: "StudentConcertImages",
                columns: table => new
                {
                    StudentConcertImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImageName = table.Column<string>(maxLength: 500, nullable: false),
                    StudentConcertId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentConcertImages", x => x.StudentConcertImageId);
                    table.ForeignKey(
                        name: "FK_StudentConcertImages_StudentConcerts_StudentConcertId",
                        column: x => x.StudentConcertId,
                        principalTable: "StudentConcerts",
                        principalColumn: "StudentConcertId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentConcertImages_StudentConcertId",
                table: "StudentConcertImages",
                column: "StudentConcertId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentConcertImages");

            migrationBuilder.DropTable(
                name: "StudentConcerts");
        }
    }
}
