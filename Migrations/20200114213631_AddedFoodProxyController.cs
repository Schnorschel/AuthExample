using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthExample.Migrations
{
    public partial class AddedFoodProxyController : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_UserFavorites_UserFavoriteId",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserFavorites_UserFavoriteId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserFavoriteId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_UserFavoriteId",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "UserFavoriteId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserFavoriteId",
                table: "Favorites");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavorites_FavoriteId",
                table: "UserFavorites",
                column: "FavoriteId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavorites_UserId",
                table: "UserFavorites",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavorites_Favorites_FavoriteId",
                table: "UserFavorites",
                column: "FavoriteId",
                principalTable: "Favorites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavorites_Users_UserId",
                table: "UserFavorites",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFavorites_Favorites_FavoriteId",
                table: "UserFavorites");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFavorites_Users_UserId",
                table: "UserFavorites");

            migrationBuilder.DropIndex(
                name: "IX_UserFavorites_FavoriteId",
                table: "UserFavorites");

            migrationBuilder.DropIndex(
                name: "IX_UserFavorites_UserId",
                table: "UserFavorites");

            migrationBuilder.AddColumn<int>(
                name: "UserFavoriteId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserFavoriteId",
                table: "Favorites",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserFavoriteId",
                table: "Users",
                column: "UserFavoriteId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserFavoriteId",
                table: "Favorites",
                column: "UserFavoriteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_UserFavorites_UserFavoriteId",
                table: "Favorites",
                column: "UserFavoriteId",
                principalTable: "UserFavorites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserFavorites_UserFavoriteId",
                table: "Users",
                column: "UserFavoriteId",
                principalTable: "UserFavorites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
