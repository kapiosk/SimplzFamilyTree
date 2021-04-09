using Microsoft.EntityFrameworkCore.Migrations;

namespace SimplzFamilyTree.Migrations
{
    public partial class M8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonRelations_Persons_PersonId",
                table: "PersonRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonRelations_Persons_RelatedPersonId",
                table: "PersonRelations");

            migrationBuilder.DropIndex(
                name: "IX_PersonRelations_PersonId",
                table: "PersonRelations");

            migrationBuilder.DropIndex(
                name: "IX_PersonRelations_RelatedPersonId",
                table: "PersonRelations");

            migrationBuilder.AlterColumn<int>(
                name: "RelatedPersonId",
                table: "PersonRelations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "PersonRelations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RelatedPersonId",
                table: "PersonRelations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "PersonRelations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_PersonRelations_PersonId",
                table: "PersonRelations",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonRelations_RelatedPersonId",
                table: "PersonRelations",
                column: "RelatedPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonRelations_Persons_PersonId",
                table: "PersonRelations",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonRelations_Persons_RelatedPersonId",
                table: "PersonRelations",
                column: "RelatedPersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
