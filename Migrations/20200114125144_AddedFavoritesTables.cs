using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AuthExample.Migrations
{
    public partial class AddedFavoritesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserFavoriteId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserFavorites",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(nullable: false),
                    FavoriteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavorites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fdcId = table.Column<int>(nullable: false),
                    UserFavoriteId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorites_UserFavorites_UserFavoriteId",
                        column: x => x.UserFavoriteId,
                        principalTable: "UserFavorites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserFavoriteId",
                table: "Users",
                column: "UserFavoriteId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserFavoriteId",
                table: "Favorites",
                column: "UserFavoriteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserFavorites_UserFavoriteId",
                table: "Users",
                column: "UserFavoriteId",
                principalTable: "UserFavorites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserFavorites_UserFavoriteId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "UserFavorites");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserFavoriteId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserFavoriteId",
                table: "Users");
        }
    }
}
