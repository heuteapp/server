using System;
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
            migrationBuilder.CreateTable(
                name: "board_cards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BoardId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    SectionId = table.Column<Guid>(type: "uuid", nullable: true),
                    Position_Row = table.Column<int>(type: "integer", nullable: true),
                    Position_Col = table.Column<int>(type: "integer", nullable: true),
                    Position_RowSpan = table.Column<int>(type: "integer", nullable: true),
                    Position_ColSpan = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_board_cards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "boards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    LayoutId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_boards", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "board_cards");

            migrationBuilder.DropTable(
                name: "boards");
        }
    }
}
