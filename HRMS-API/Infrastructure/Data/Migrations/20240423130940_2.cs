using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "CompanySettings");

            migrationBuilder.RenameColumn(
                name: "salaryCoeffcient",
                table: "Positions",
                newName: "SalaryCoeffcient");

            migrationBuilder.AlterColumn<DateTime>(
                name: "HourStartWorking",
                table: "CompanySettings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SalaryCoeffcient",
                table: "Positions",
                newName: "salaryCoeffcient");

            migrationBuilder.AlterColumn<DateTime>(
                name: "HourStartWorking",
                table: "CompanySettings",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "CompanySettings",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
