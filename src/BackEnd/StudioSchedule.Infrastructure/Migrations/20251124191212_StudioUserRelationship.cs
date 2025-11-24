using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudioSchedule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StudioUserRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Studios",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Studios_UserId",
                table: "Studios",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Studios_Users_UserId",
                table: "Studios",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Studios_Users_UserId",
                table: "Studios");

            migrationBuilder.DropIndex(
                name: "IX_Studios_UserId",
                table: "Studios");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Studios");
        }
    }
}
