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
            migrationBuilder.RenameColumn(
                name: "Size_RowCount",
                table: "layouts",
                newName: "Dimensions_RowCount");

            migrationBuilder.RenameColumn(
                name: "Size_ColCount",
                table: "layouts",
                newName: "Dimensions_ColCount");

            migrationBuilder.RenameColumn(
                name: "Placement_Row",
                table: "board_cards",
                newName: "Placement_RowIndex");

            migrationBuilder.RenameColumn(
                name: "Placement_Col",
                table: "board_cards",
                newName: "Placement_ColIndex");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Dimensions_RowCount",
                table: "layouts",
                newName: "Size_RowCount");

            migrationBuilder.RenameColumn(
                name: "Dimensions_ColCount",
                table: "layouts",
                newName: "Size_ColCount");

            migrationBuilder.RenameColumn(
                name: "Placement_RowIndex",
                table: "board_cards",
                newName: "Placement_Row");

            migrationBuilder.RenameColumn(
                name: "Placement_ColIndex",
                table: "board_cards",
                newName: "Placement_Col");
        }
    }
}
