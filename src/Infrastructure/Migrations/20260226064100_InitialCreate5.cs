using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeuteApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "layouts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_layouts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "layout_sections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LayoutId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Rect_X = table.Column<int>(type: "integer", nullable: false),
                    Rect_Y = table.Column<int>(type: "integer", nullable: false),
                    Rect_Width = table.Column<int>(type: "integer", nullable: false),
                    Rect_Height = table.Column<int>(type: "integer", nullable: false),
                    Size_RowCount = table.Column<int>(type: "integer", nullable: false),
                    Size_ColCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_layout_sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_layout_sections_layouts_LayoutId",
                        column: x => x.LayoutId,
                        principalTable: "layouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_boards_LayoutId",
                table: "boards",
                column: "LayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_layout_sections_LayoutId",
                table: "layout_sections",
                column: "LayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_layouts_OwnerId_Name_Version",
                table: "layouts",
                columns: new[] { "OwnerId", "Name", "Version" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_boards_layouts_LayoutId",
                table: "boards",
                column: "LayoutId",
                principalTable: "layouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_boards_layouts_LayoutId",
                table: "boards");

            migrationBuilder.DropTable(
                name: "layout_sections");

            migrationBuilder.DropTable(
                name: "layouts");

            migrationBuilder.DropIndex(
                name: "IX_boards_LayoutId",
                table: "boards");
        }
    }
}
