using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace se100_cs.Migrations
{
    public partial class _105 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_attendance_tb_employee_employeeID",
                table: "tb_attendance");

            migrationBuilder.AlterColumn<long>(
                name: "employeeID",
                table: "tb_attendance",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateTable(
                name: "tb_setting",
                columns: table => new
                {
                    company_name = table.Column<string>(type: "text", nullable: false),
                    company_code = table.Column<string>(type: "text", nullable: false),
                    start_time_hour = table.Column<int>(type: "integer", nullable: false),
                    start_time_minute = table.Column<int>(type: "integer", nullable: false),
                    salary_per_coef = table.Column<int>(type: "integer", nullable: false),
                    payment_date = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.AddForeignKey(
                name: "FK_tb_attendance_tb_employee_employeeID",
                table: "tb_attendance",
                column: "employeeID",
                principalTable: "tb_employee",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_attendance_tb_employee_employeeID",
                table: "tb_attendance");

            migrationBuilder.DropTable(
                name: "tb_setting");

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
        }
    }
}
