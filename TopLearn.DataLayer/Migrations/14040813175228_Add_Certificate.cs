using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TopLearn.DataLayer.Migrations
{
    public partial class Add_Certificate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    CertificateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 255, nullable: false),
                    LastName = table.Column<string>(maxLength: 255, nullable: false),
                    NationalCode = table.Column<string>(maxLength: 255, nullable: false),
                    Mobile = table.Column<string>(maxLength: 25, nullable: false),
                    Academy = table.Column<string>(maxLength: 800, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Instrument = table.Column<string>(maxLength: 1500, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsPay = table.Column<bool>(nullable: false),
                    IsDone = table.Column<bool>(nullable: false),
                    FileName = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.CertificateId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Certificates");
        }
    }
}
