using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimplzFamilyTree.Migrations
{
    public partial class M11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductImage",
                table: "PersonImages");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "PersonImages",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "PersonImages");

            migrationBuilder.AddColumn<byte[]>(
                name: "ProductImage",
                table: "PersonImages",
                type: "image",
                nullable: true);
        }
    }
}
