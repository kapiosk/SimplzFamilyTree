using Microsoft.EntityFrameworkCore.Migrations;

namespace SimplzFamilyTree.Migrations
{
    public partial class M6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonRelations_Persons_RelatedPersonPersonId",
                table: "PersonRelations");

            migrationBuilder.RenameColumn(
                name: "RelatedPersonPersonId",
                table: "PersonRelations",
                newName: "RelatedPersonId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonRelations_RelatedPersonPersonId",
                table: "PersonRelations",
                newName: "IX_PersonRelations_RelatedPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonRelations_Persons_RelatedPersonId",
                table: "PersonRelations",
                column: "RelatedPersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonRelations_Persons_RelatedPersonId",
                table: "PersonRelations");

            migrationBuilder.RenameColumn(
                name: "RelatedPersonId",
                table: "PersonRelations",
                newName: "RelatedPersonPersonId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonRelations_RelatedPersonId",
                table: "PersonRelations",
                newName: "IX_PersonRelations_RelatedPersonPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonRelations_Persons_RelatedPersonPersonId",
                table: "PersonRelations",
                column: "RelatedPersonPersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
