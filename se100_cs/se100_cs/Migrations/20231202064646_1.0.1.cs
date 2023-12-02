using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace se100_cs.Migrations
{
    public partial class _101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "employeeID",
                table: "tb_payroll",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "receive_date",
                table: "tb_payroll",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_tb_payroll_employeeID",
                table: "tb_payroll",
                column: "employeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_payroll_tb_employee_employeeID",
                table: "tb_payroll",
                column: "employeeID",
                principalTable: "tb_employee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_payroll_tb_employee_employeeID",
                table: "tb_payroll");

            migrationBuilder.DropIndex(
                name: "IX_tb_payroll_employeeID",
                table: "tb_payroll");

            migrationBuilder.DropColumn(
                name: "employeeID",
                table: "tb_payroll");

            migrationBuilder.DropColumn(
                name: "receive_date",
                table: "tb_payroll");
        }
    }
}
