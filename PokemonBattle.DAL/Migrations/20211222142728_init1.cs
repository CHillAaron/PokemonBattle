using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonBattle.DAL.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaughtPokemons_Users_UserId",
                table: "CaughtPokemons");

            migrationBuilder.DropIndex(
                name: "IX_CaughtPokemons_UserId",
                table: "CaughtPokemons");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CaughtPokemons");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "CaughtPokemons",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CaughtPokemons_UserId",
                table: "CaughtPokemons",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CaughtPokemons_Users_UserId",
                table: "CaughtPokemons",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
