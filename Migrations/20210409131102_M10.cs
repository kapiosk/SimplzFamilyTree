using Microsoft.EntityFrameworkCore.Migrations;

namespace SimplzFamilyTree.Migrations
{
    public partial class M10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_AspNetUsers_ApplicationUserId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_ApplicationUserId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Persons");

            migrationBuilder.CreateIndex(
                name: "IX_PersonRelations_PersonId",
                table: "PersonRelations",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonRelations_Persons_PersonId",
                table: "PersonRelations",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonRelations_Persons_PersonId",
                table: "PersonRelations");

            migrationBuilder.DropIndex(
                name: "IX_PersonRelations_PersonId",
                table: "PersonRelations");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Persons",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_ApplicationUserId",
                table: "Persons",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_AspNetUsers_ApplicationUserId",
                table: "Persons",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
