using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class _5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Positions_PositionId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "PositionId",
                table: "Users",
                newName: "SqlPositionId");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "Users",
                newName: "SqlDepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_PositionId",
                table: "Users",
                newName: "IX_Users_SqlPositionId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                newName: "IX_Users_SqlDepartmentId");

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordReset",
                table: "Accounts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResetToken",
                table: "Accounts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetTokenExpires",
                table: "Accounts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerificationToken",
                table: "Accounts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Verified",
                table: "Accounts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    accessToken = table.Column<string>(type: "text", nullable: false),
                    refreshToken = table.Column<string>(type: "text", nullable: false),
                    isExpired = table.Column<bool>(type: "boolean", nullable: false),
                    userId = table.Column<int>(type: "integer", nullable: true),
                    createTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    expiredTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SqlUserId = table.Column<int>(type: "integer", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Deleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tokens_Accounts_userId",
                        column: x => x.userId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tokens_Users_SqlUserId",
                        column: x => x.SqlUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_SqlUserId",
                table: "Tokens",
                column: "SqlUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_userId",
                table: "Tokens",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departments_SqlDepartmentId",
                table: "Users",
                column: "SqlDepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Positions_SqlPositionId",
                table: "Users",
                column: "SqlPositionId",
                principalTable: "Positions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departments_SqlDepartmentId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Positions_SqlPositionId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropColumn(
                name: "PasswordReset",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ResetToken",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ResetTokenExpires",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "VerificationToken",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Verified",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "SqlPositionId",
                table: "Users",
                newName: "PositionId");

            migrationBuilder.RenameColumn(
                name: "SqlDepartmentId",
                table: "Users",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_SqlPositionId",
                table: "Users",
                newName: "IX_Users_PositionId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_SqlDepartmentId",
                table: "Users",
                newName: "IX_Users_DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Positions_PositionId",
                table: "Users",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id");
        }
    }
}
