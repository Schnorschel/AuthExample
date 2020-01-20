using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthExample.Migrations
{
    public partial class AddedToFavorites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "additionalDescriptions",
                table: "Favorites",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "brandOwner",
                table: "Favorites",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Favorites",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "gtinUpc",
                table: "Favorites",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ingredients",
                table: "Favorites",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "additionalDescriptions",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "brandOwner",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "description",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "gtinUpc",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "ingredients",
                table: "Favorites");
        }
    }
}
