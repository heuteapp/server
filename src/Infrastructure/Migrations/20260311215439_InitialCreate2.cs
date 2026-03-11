using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeuteApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "board_cards",
                newName: "Content_Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content_Title",
                table: "board_cards",
                newName: "Title");
        }
    }
}
