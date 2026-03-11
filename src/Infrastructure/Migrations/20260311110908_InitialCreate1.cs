using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeuteApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "profiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_categories_profiles_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "layouts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    Size_RowCount = table.Column<int>(type: "integer", nullable: false),
                    Size_ColCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_layouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_layouts_profiles_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "boards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    LayoutId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_boards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_boards_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_boards_layouts_LayoutId",
                        column: x => x.LayoutId,
                        principalTable: "layouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_boards_profiles_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "layout_sections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LayoutId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Area_Row = table.Column<int>(type: "integer", nullable: false),
                    Area_Col = table.Column<int>(type: "integer", nullable: false),
                    Area_RowSpan = table.Column<int>(type: "integer", nullable: false),
                    Area_ColSpan = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "board_cards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BoardId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Placement_SectionName = table.Column<string>(type: "text", nullable: true),
                    Placement_Col = table.Column<int>(type: "integer", nullable: true),
                    Placement_Row = table.Column<int>(type: "integer", nullable: true),
                    Placement_ColSpan = table.Column<int>(type: "integer", nullable: true),
                    Placement_RowSpan = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_board_cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_board_cards_boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_board_cards_BoardId",
                table: "board_cards",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_boards_CategoryId",
                table: "boards",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_boards_LayoutId",
                table: "boards",
                column: "LayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_boards_OwnerId_Date",
                table: "boards",
                columns: new[] { "OwnerId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_categories_OwnerId",
                table: "categories",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_layout_sections_LayoutId_Name",
                table: "layout_sections",
                columns: new[] { "LayoutId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_layouts_OwnerId_Name_Version",
                table: "layouts",
                columns: new[] { "OwnerId", "Name", "Version" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "board_cards");

            migrationBuilder.DropTable(
                name: "layout_sections");

            migrationBuilder.DropTable(
                name: "boards");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "layouts");

            migrationBuilder.DropTable(
                name: "profiles");
        }
    }
}
