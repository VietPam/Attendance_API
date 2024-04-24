using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class _7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_Accounts_userId",
                table: "Tokens");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Tokens",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Tokens_userId",
                table: "Tokens",
                newName: "IX_Tokens_AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_Accounts_AccountId",
                table: "Tokens",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_Accounts_AccountId",
                table: "Tokens");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Tokens",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Tokens_AccountId",
                table: "Tokens",
                newName: "IX_Tokens_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_Accounts_userId",
                table: "Tokens",
                column: "userId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
