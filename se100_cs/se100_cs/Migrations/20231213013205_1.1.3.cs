using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace se100_cs.Migrations
{
    public partial class _113 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "employeeId",
                table: "tb_attendance_detail",
                newName: "employeeID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_attendance_detail_employeeID",
                table: "tb_attendance_detail",
                column: "employeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_attendance_detail_tb_employee_employeeID",
                table: "tb_attendance_detail",
                column: "employeeID",
                principalTable: "tb_employee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_attendance_detail_tb_employee_employeeID",
                table: "tb_attendance_detail");

            migrationBuilder.DropIndex(
                name: "IX_tb_attendance_detail_employeeID",
                table: "tb_attendance_detail");

            migrationBuilder.RenameColumn(
                name: "employeeID",
                table: "tb_attendance_detail",
                newName: "employeeId");
        }
    }
}
