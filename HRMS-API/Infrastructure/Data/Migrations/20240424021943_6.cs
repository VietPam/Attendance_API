using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class _6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_Users_SqlUserId",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_SqlUserId",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "SqlUserId",
                table: "Tokens");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SqlUserId",
                table: "Tokens",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_SqlUserId",
                table: "Tokens",
                column: "SqlUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_Users_SqlUserId",
                table: "Tokens",
                column: "SqlUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
