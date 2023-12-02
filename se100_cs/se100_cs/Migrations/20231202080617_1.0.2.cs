using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace se100_cs.Migrations
{
    public partial class _102 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_payroll_tb_employee_employeeID",
                table: "tb_payroll");

            migrationBuilder.AlterColumn<long>(
                name: "employeeID",
                table: "tb_payroll",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "tb_department",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_payroll_tb_employee_employeeID",
                table: "tb_payroll",
                column: "employeeID",
                principalTable: "tb_employee",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_payroll_tb_employee_employeeID",
                table: "tb_payroll");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "tb_department");

            migrationBuilder.AlterColumn<long>(
                name: "employeeID",
                table: "tb_payroll",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_payroll_tb_employee_employeeID",
                table: "tb_payroll",
                column: "employeeID",
                principalTable: "tb_employee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
