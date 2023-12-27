using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace se100_cs.Migrations
{
    public partial class _101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_attendance_tb_atd_status_stateID",
                table: "tb_attendance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_atd_status",
                table: "tb_atd_status");

            migrationBuilder.RenameTable(
                name: "tb_atd_status",
                newName: "tb_state");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_state",
                table: "tb_state",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_attendance_tb_state_stateID",
                table: "tb_attendance",
                column: "stateID",
                principalTable: "tb_state",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_attendance_tb_state_stateID",
                table: "tb_attendance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_state",
                table: "tb_state");

            migrationBuilder.RenameTable(
                name: "tb_state",
                newName: "tb_atd_status");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_atd_status",
                table: "tb_atd_status",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_attendance_tb_atd_status_stateID",
                table: "tb_attendance",
                column: "stateID",
                principalTable: "tb_atd_status",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
