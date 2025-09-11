using Microsoft.EntityFrameworkCore.Migrations;

namespace TopLearn.DataLayer.Migrations
{
    public partial class TblStudentConcert_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "StudentConcerts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "StudentConcerts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "StudentConcerts");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "StudentConcerts");
        }
    }
}
