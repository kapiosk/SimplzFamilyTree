using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimplzFamilyTree.Migrations
{
    public partial class M7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Persons",
                newName: "Nickname");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DoB",
                table: "Persons",
                type: "Date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DoD",
                table: "Persons",
                type: "Date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoD",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Persons");

            migrationBuilder.RenameColumn(
                name: "Nickname",
                table: "Persons",
                newName: "Name");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DoB",
                table: "Persons",
                type: "Date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "Date");
        }
    }
}
