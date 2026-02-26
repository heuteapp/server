using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeuteApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "size_row_count",
                table: "layout_sections",
                newName: "size_rowCount");

            migrationBuilder.RenameColumn(
                name: "size_col_count",
                table: "layout_sections",
                newName: "size_colCount");

            migrationBuilder.RenameColumn(
                name: "position_row_span",
                table: "board_cards",
                newName: "position_rowSpan");

            migrationBuilder.RenameColumn(
                name: "position_col_span",
                table: "board_cards",
                newName: "position_colSpan");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "size_rowCount",
                table: "layout_sections",
                newName: "size_row_count");

            migrationBuilder.RenameColumn(
                name: "size_colCount",
                table: "layout_sections",
                newName: "size_col_count");

            migrationBuilder.RenameColumn(
                name: "position_rowSpan",
                table: "board_cards",
                newName: "position_row_span");

            migrationBuilder.RenameColumn(
                name: "position_colSpan",
                table: "board_cards",
                newName: "position_col_span");
        }
    }
}
