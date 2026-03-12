using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeuteApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Area_RowSpan",
                table: "layout_sections",
                newName: "Position_RowSpan");

            migrationBuilder.RenameColumn(
                name: "Area_Row",
                table: "layout_sections",
                newName: "Position_RowIndex");

            migrationBuilder.RenameColumn(
                name: "Area_ColSpan",
                table: "layout_sections",
                newName: "Position_ColSpan");

            migrationBuilder.RenameColumn(
                name: "Area_Col",
                table: "layout_sections",
                newName: "Position_ColIndex");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Position_RowSpan",
                table: "layout_sections",
                newName: "Area_RowSpan");

            migrationBuilder.RenameColumn(
                name: "Position_RowIndex",
                table: "layout_sections",
                newName: "Area_Row");

            migrationBuilder.RenameColumn(
                name: "Position_ColSpan",
                table: "layout_sections",
                newName: "Area_ColSpan");

            migrationBuilder.RenameColumn(
                name: "Position_ColIndex",
                table: "layout_sections",
                newName: "Area_Col");
        }
    }
}
