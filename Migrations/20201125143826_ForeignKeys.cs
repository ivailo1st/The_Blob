using Microsoft.EntityFrameworkCore.Migrations;

namespace The_Blob.Migrations
{
    public partial class ForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CharacterFridge_CharacterId",
                table: "CharacterFridge",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterFridge_FridgeId",
                table: "CharacterFridge",
                column: "FridgeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterFridge_Character_CharacterId",
                table: "CharacterFridge",
                column: "CharacterId",
                principalTable: "Character",
                principalColumn: "CharacterId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterFridge_Fridge_FridgeId",
                table: "CharacterFridge",
                column: "FridgeId",
                principalTable: "Fridge",
                principalColumn: "FridgeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterFridge_Character_CharacterId",
                table: "CharacterFridge");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacterFridge_Fridge_FridgeId",
                table: "CharacterFridge");

            migrationBuilder.DropIndex(
                name: "IX_CharacterFridge_CharacterId",
                table: "CharacterFridge");

            migrationBuilder.DropIndex(
                name: "IX_CharacterFridge_FridgeId",
                table: "CharacterFridge");
        }
    }
}
