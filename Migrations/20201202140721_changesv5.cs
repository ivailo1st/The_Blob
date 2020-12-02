using Microsoft.EntityFrameworkCore.Migrations;

namespace The_Blob.Migrations
{
    public partial class changesv5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Fridge",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Fridge");
        }
    }
}
