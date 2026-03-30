using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeuteApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_dailyboards_UserId_Date",
                table: "dailyboards");

            migrationBuilder.CreateIndex(
                name: "IX_dailyboards_UserId_CategoryId_Date",
                table: "dailyboards",
                columns: new[] { "UserId", "CategoryId", "Date" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_dailyboards_UserId_CategoryId_Date",
                table: "dailyboards");

            migrationBuilder.CreateIndex(
                name: "IX_dailyboards_UserId_Date",
                table: "dailyboards",
                columns: new[] { "UserId", "Date" },
                unique: true);
        }
    }
}
