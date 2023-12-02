using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace se100_cs.Migrations
{
    public partial class _104 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_employee_tb_role_roleID",
                table: "tb_employee");

            migrationBuilder.DropIndex(
                name: "IX_tb_employee_roleID",
                table: "tb_employee");

            migrationBuilder.DropColumn(
                name: "roleID",
                table: "tb_employee");

            migrationBuilder.AddColumn<int>(
                name: "role",
                table: "tb_employee",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "role",
                table: "tb_employee");

            migrationBuilder.AddColumn<long>(
                name: "roleID",
                table: "tb_employee",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_employee_roleID",
                table: "tb_employee",
                column: "roleID");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_employee_tb_role_roleID",
                table: "tb_employee",
                column: "roleID",
                principalTable: "tb_role",
                principalColumn: "ID");
        }
    }
}
