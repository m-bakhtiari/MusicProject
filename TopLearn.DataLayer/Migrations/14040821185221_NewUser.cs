using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TopLearn.DataLayer.Migrations
{
    public partial class NewUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "IsActive", "IsDelete", "Password", "RegisterDate", "UserName" },
                values: new object[] { 2, "vahidnajafizadeh@gmail.com", true, false, "CB-5C-10-C0-B4-88-ED-4F-38-26-BA-62-8D-18-CA-6A", new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vahid Najafizadeh" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "IsActive", "IsDelete", "Password", "RegisterDate", "UserName" },
                values: new object[] { 1, "vahidnajafizadeh@gmail.com", true, false, "E1-0A-DC-39-49-BA-59-AB-BE-56-E0-57-F2-0F-88-3E", new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vahid Najafizadeh" });
        }
    }
}
