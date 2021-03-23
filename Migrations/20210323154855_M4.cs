using Microsoft.EntityFrameworkCore.Migrations;

namespace SimplzFamilyTree.Migrations
{
    public partial class M4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserClaims_ApplicationUsers_UserId",
                table: "ApplicationUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserClaims",
                table: "ApplicationUserClaims");

            migrationBuilder.RenameTable(
                name: "ApplicationUserClaims",
                newName: "IdentityUserClaim<string>");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserClaims_UserId",
                table: "IdentityUserClaim<string>",
                newName: "IX_IdentityUserClaim<string>_UserId");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "IdentityUserClaim<string>",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "IdentityUserClaim<string>",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentityUserClaim<string>",
                table: "IdentityUserClaim<string>",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserClaim<string>_ApplicationUserId",
                table: "IdentityUserClaim<string>",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserClaim<string>_ApplicationUsers_ApplicationUserId",
                table: "IdentityUserClaim<string>",
                column: "ApplicationUserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserClaim<string>_ApplicationUsers_UserId",
                table: "IdentityUserClaim<string>",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IdentityUserClaim<string>_ApplicationUsers_ApplicationUserId",
                table: "IdentityUserClaim<string>");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityUserClaim<string>_ApplicationUsers_UserId",
                table: "IdentityUserClaim<string>");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentityUserClaim<string>",
                table: "IdentityUserClaim<string>");

            migrationBuilder.DropIndex(
                name: "IX_IdentityUserClaim<string>_ApplicationUserId",
                table: "IdentityUserClaim<string>");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "IdentityUserClaim<string>");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "IdentityUserClaim<string>");

            migrationBuilder.RenameTable(
                name: "IdentityUserClaim<string>",
                newName: "ApplicationUserClaims");

            migrationBuilder.RenameIndex(
                name: "IX_IdentityUserClaim<string>_UserId",
                table: "ApplicationUserClaims",
                newName: "IX_ApplicationUserClaims_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserClaims",
                table: "ApplicationUserClaims",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserClaims_ApplicationUsers_UserId",
                table: "ApplicationUserClaims",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
