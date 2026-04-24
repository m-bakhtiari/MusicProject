using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TopLearn.DataLayer.Migrations
{
    public partial class Newtemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HavanaYear",
                table: "Students",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HistoryYear",
                table: "Students",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstagramUrl",
                table: "Students",
                maxLength: 1500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TelegramUrl",
                table: "Students",
                maxLength: 1500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "Students",
                maxLength: 1500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YoutubeUrl",
                table: "Students",
                maxLength: 1500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "StudentConcerts",
                maxLength: 3500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "MusicNotes",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageName",
                table: "Instruments",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IconImage",
                table: "Instruments",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Instruments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "Instruments",
                maxLength: 3500,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalePrice",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Courses",
                maxLength: 1500,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    SubscriberId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Mobile = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.SubscriberId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscribers");

            migrationBuilder.DropColumn(
                name: "HavanaYear",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "HistoryYear",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "InstagramUrl",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "TelegramUrl",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "YoutubeUrl",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "StudentConcerts");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "MusicNotes");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "Instruments");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Courses");

            migrationBuilder.AlterColumn<string>(
                name: "ImageName",
                table: "Instruments",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IconImage",
                table: "Instruments",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
