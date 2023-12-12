using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace se100_cs.Migrations
{
    public partial class _109 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "departmentID",
                table: "tb_employee",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_employee_departmentID",
                table: "tb_employee",
                column: "departmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_employee_tb_department_departmentID",
                table: "tb_employee",
                column: "departmentID",
                principalTable: "tb_department",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_employee_tb_department_departmentID",
                table: "tb_employee");

            migrationBuilder.DropIndex(
                name: "IX_tb_employee_departmentID",
                table: "tb_employee");

            migrationBuilder.DropColumn(
                name: "departmentID",
                table: "tb_employee");
        }
    }
}
