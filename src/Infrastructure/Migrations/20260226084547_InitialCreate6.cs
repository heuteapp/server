using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeuteApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rect_Y",
                table: "layout_sections",
                newName: "rect_y");

            migrationBuilder.RenameColumn(
                name: "Rect_X",
                table: "layout_sections",
                newName: "rect_x");

            migrationBuilder.RenameColumn(
                name: "Rect_Width",
                table: "layout_sections",
                newName: "rect_width");

            migrationBuilder.RenameColumn(
                name: "Rect_Height",
                table: "layout_sections",
                newName: "rect_height");

            migrationBuilder.RenameColumn(
                name: "Size_RowCount",
                table: "layout_sections",
                newName: "size_row_count");

            migrationBuilder.RenameColumn(
                name: "Size_ColCount",
                table: "layout_sections",
                newName: "size_col_count");

            migrationBuilder.RenameColumn(
                name: "Position_Row",
                table: "board_cards",
                newName: "position_row");

            migrationBuilder.RenameColumn(
                name: "Position_Col",
                table: "board_cards",
                newName: "position_col");

            migrationBuilder.RenameColumn(
                name: "Position_RowSpan",
                table: "board_cards",
                newName: "position_row_span");

            migrationBuilder.RenameColumn(
                name: "Position_ColSpan",
                table: "board_cards",
                newName: "position_col_span");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "boards",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "boards");

            migrationBuilder.RenameColumn(
                name: "rect_y",
                table: "layout_sections",
                newName: "Rect_Y");

            migrationBuilder.RenameColumn(
                name: "rect_x",
                table: "layout_sections",
                newName: "Rect_X");

            migrationBuilder.RenameColumn(
                name: "rect_width",
                table: "layout_sections",
                newName: "Rect_Width");

            migrationBuilder.RenameColumn(
                name: "rect_height",
                table: "layout_sections",
                newName: "Rect_Height");

            migrationBuilder.RenameColumn(
                name: "size_row_count",
                table: "layout_sections",
                newName: "Size_RowCount");

            migrationBuilder.RenameColumn(
                name: "size_col_count",
                table: "layout_sections",
                newName: "Size_ColCount");

            migrationBuilder.RenameColumn(
                name: "position_row",
                table: "board_cards",
                newName: "Position_Row");

            migrationBuilder.RenameColumn(
                name: "position_col",
                table: "board_cards",
                newName: "Position_Col");

            migrationBuilder.RenameColumn(
                name: "position_row_span",
                table: "board_cards",
                newName: "Position_RowSpan");

            migrationBuilder.RenameColumn(
                name: "position_col_span",
                table: "board_cards",
                newName: "Position_ColSpan");
        }
    }
}
