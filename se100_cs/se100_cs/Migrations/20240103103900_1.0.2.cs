using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace se100_cs.Migrations
{
    public partial class _102 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_attendance_tb_employee_employeeID",
                table: "tb_attendance");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_attendance_tb_state_stateID",
                table: "tb_attendance");

            migrationBuilder.AlterColumn<long>(
                name: "stateID",
                table: "tb_attendance",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "employeeID",
                table: "tb_attendance",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "departmentID",
                table: "tb_attendance",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_attendance_departmentID",
                table: "tb_attendance",
                column: "departmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_attendance_tb_department_departmentID",
                table: "tb_attendance",
                column: "departmentID",
                principalTable: "tb_department",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_attendance_tb_employee_employeeID",
                table: "tb_attendance",
                column: "employeeID",
                principalTable: "tb_employee",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_attendance_tb_state_stateID",
                table: "tb_attendance",
                column: "stateID",
                principalTable: "tb_state",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_attendance_tb_department_departmentID",
                table: "tb_attendance");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_attendance_tb_employee_employeeID",
                table: "tb_attendance");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_attendance_tb_state_stateID",
                table: "tb_attendance");

            migrationBuilder.DropIndex(
                name: "IX_tb_attendance_departmentID",
                table: "tb_attendance");

            migrationBuilder.DropColumn(
                name: "departmentID",
                table: "tb_attendance");

            migrationBuilder.AlterColumn<long>(
                name: "stateID",
                table: "tb_attendance",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "employeeID",
                table: "tb_attendance",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_attendance_tb_employee_employeeID",
                table: "tb_attendance",
                column: "employeeID",
                principalTable: "tb_employee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_attendance_tb_state_stateID",
                table: "tb_attendance",
                column: "stateID",
                principalTable: "tb_state",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
