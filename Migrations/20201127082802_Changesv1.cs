using Microsoft.EntityFrameworkCore.Migrations;

namespace The_Blob.Migrations
{
    public partial class Changesv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Character_CharacterId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_CharacterId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Character",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Character_UserId",
                table: "Character",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_User_UserId",
                table: "Character",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Character_User_UserId",
                table: "Character");

            migrationBuilder.DropIndex(
                name: "IX_Character_UserId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Character");

            migrationBuilder.AddColumn<int>(
                name: "CharacterId",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_User_CharacterId",
                table: "User",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Character_CharacterId",
                table: "User",
                column: "CharacterId",
                principalTable: "Character",
                principalColumn: "CharacterId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
