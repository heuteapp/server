using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeuteApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_board_cards_BoardId",
                table: "board_cards",
                column: "BoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_board_cards_boards_BoardId",
                table: "board_cards",
                column: "BoardId",
                principalTable: "boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_board_cards_boards_BoardId",
                table: "board_cards");

            migrationBuilder.DropIndex(
                name: "IX_board_cards_BoardId",
                table: "board_cards");
        }
    }
}
