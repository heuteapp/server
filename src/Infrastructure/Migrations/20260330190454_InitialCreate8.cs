using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeuteApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categories_profiles_OwnerId",
                table: "categories");

            migrationBuilder.DropForeignKey(
                name: "FK_dailyboards_profiles_OwnerId",
                table: "dailyboards");

            migrationBuilder.DropForeignKey(
                name: "FK_layouts_profiles_OwnerId",
                table: "layouts");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "layouts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_layouts_OwnerId_Name_Version",
                table: "layouts",
                newName: "IX_layouts_UserId_Name_Version");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "dailyboards",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_dailyboards_OwnerId_Date",
                table: "dailyboards",
                newName: "IX_dailyboards_UserId_Date");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "categories",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_categories_OwnerId",
                table: "categories",
                newName: "IX_categories_UserId");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "categories",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_categories_ParentId",
                table: "categories",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_categories_categories_ParentId",
                table: "categories",
                column: "ParentId",
                principalTable: "categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_categories_profiles_UserId",
                table: "categories",
                column: "UserId",
                principalTable: "profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dailyboards_profiles_UserId",
                table: "dailyboards",
                column: "UserId",
                principalTable: "profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_layouts_profiles_UserId",
                table: "layouts",
                column: "UserId",
                principalTable: "profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categories_categories_ParentId",
                table: "categories");

            migrationBuilder.DropForeignKey(
                name: "FK_categories_profiles_UserId",
                table: "categories");

            migrationBuilder.DropForeignKey(
                name: "FK_dailyboards_profiles_UserId",
                table: "dailyboards");

            migrationBuilder.DropForeignKey(
                name: "FK_layouts_profiles_UserId",
                table: "layouts");

            migrationBuilder.DropIndex(
                name: "IX_categories_ParentId",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "categories");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "layouts",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_layouts_UserId_Name_Version",
                table: "layouts",
                newName: "IX_layouts_OwnerId_Name_Version");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "dailyboards",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_dailyboards_UserId_Date",
                table: "dailyboards",
                newName: "IX_dailyboards_OwnerId_Date");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "categories",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_categories_UserId",
                table: "categories",
                newName: "IX_categories_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_categories_profiles_OwnerId",
                table: "categories",
                column: "OwnerId",
                principalTable: "profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_dailyboards_profiles_OwnerId",
                table: "dailyboards",
                column: "OwnerId",
                principalTable: "profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_layouts_profiles_OwnerId",
                table: "layouts",
                column: "OwnerId",
                principalTable: "profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
