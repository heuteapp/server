using System;
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
            migrationBuilder.DropTable(
                name: "board_cards");

            migrationBuilder.DropTable(
                name: "boards");

            migrationBuilder.CreateTable(
                name: "dailyboards",
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
                    table.PrimaryKey("PK_dailyboards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dailyboards_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dailyboards_layouts_LayoutId",
                        column: x => x.LayoutId,
                        principalTable: "layouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dailyboards_profiles_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "dailyboard_cards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DailyboardId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Content_Title = table.Column<string>(type: "text", nullable: false),
                    Placement_SectionName = table.Column<string>(type: "text", nullable: true),
                    Placement_ColIndex = table.Column<int>(type: "integer", nullable: true),
                    Placement_RowIndex = table.Column<int>(type: "integer", nullable: true),
                    Placement_ColSpan = table.Column<int>(type: "integer", nullable: true),
                    Placement_RowSpan = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dailyboard_cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dailyboard_cards_dailyboards_DailyboardId",
                        column: x => x.DailyboardId,
                        principalTable: "dailyboards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_dailyboard_cards_DailyboardId",
                table: "dailyboard_cards",
                column: "DailyboardId");

            migrationBuilder.CreateIndex(
                name: "IX_dailyboards_CategoryId",
                table: "dailyboards",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_dailyboards_LayoutId",
                table: "dailyboards",
                column: "LayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_dailyboards_OwnerId_Date",
                table: "dailyboards",
                columns: new[] { "OwnerId", "Date" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dailyboard_cards");

            migrationBuilder.DropTable(
                name: "dailyboards");

            migrationBuilder.CreateTable(
                name: "boards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    LayoutId = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
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
                name: "board_cards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BoardId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Content_Title = table.Column<string>(type: "text", nullable: false),
                    Placement_ColIndex = table.Column<int>(type: "integer", nullable: true),
                    Placement_ColSpan = table.Column<int>(type: "integer", nullable: true),
                    Placement_RowIndex = table.Column<int>(type: "integer", nullable: true),
                    Placement_RowSpan = table.Column<int>(type: "integer", nullable: true),
                    Placement_SectionName = table.Column<string>(type: "text", nullable: true)
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
        }
    }
}
