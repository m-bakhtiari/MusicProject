using Microsoft.EntityFrameworkCore.Migrations;

namespace TopLearn.DataLayer.Migrations
{
    public partial class Remove_Required : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InstrumentTitle",
                table: "Instruments",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "AcademyTitle",
                table: "Academies",
                maxLength: 800,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 800);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InstrumentTitle",
                table: "Instruments",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AcademyTitle",
                table: "Academies",
                maxLength: 800,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 800,
                oldNullable: true);
        }
    }
}
