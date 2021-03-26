using Microsoft.EntityFrameworkCore.Migrations;

namespace SimplzFamilyTree.Migrations
{
    public partial class M3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "PersonEvents",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonEvents_PersonId",
                table: "PersonEvents",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonEvents_Persons_PersonId",
                table: "PersonEvents",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonEvents_Persons_PersonId",
                table: "PersonEvents");

            migrationBuilder.DropIndex(
                name: "IX_PersonEvents_PersonId",
                table: "PersonEvents");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "PersonEvents");
        }
    }
}
