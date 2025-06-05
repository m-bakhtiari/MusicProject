using Microsoft.EntityFrameworkCore.Migrations;

namespace TopLearn.DataLayer.Migrations
{
    public partial class Add_AcademyLogo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogoImageName",
                table: "Academies",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Academies",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "Academies",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoImageName",
                table: "Academies");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Academies");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "Academies");
        }
    }
}
