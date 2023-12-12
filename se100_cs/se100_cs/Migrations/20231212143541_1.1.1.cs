using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace se100_cs.Migrations
{
    public partial class _111 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_attendance_tb_employee_employeeID",
                table: "tb_attendance");

            migrationBuilder.DropIndex(
                name: "IX_tb_attendance_employeeID",
                table: "tb_attendance");

            migrationBuilder.DropColumn(
                name: "employeeID",
                table: "tb_attendance");

            migrationBuilder.DropColumn(
                name: "time",
                table: "tb_attendance");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "tb_attendance",
                newName: "year");

            migrationBuilder.AddColumn<int>(
                name: "day",
                table: "tb_attendance",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "month",
                table: "tb_attendance",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "tb_attendance_detail",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    attendanceID = table.Column<long>(type: "bigint", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    employeeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_attendance_detail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tb_attendance_detail_tb_attendance_attendanceID",
                        column: x => x.attendanceID,
                        principalTable: "tb_attendance",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_attendance_detail_attendanceID",
                table: "tb_attendance_detail",
                column: "attendanceID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_attendance_detail");

            migrationBuilder.DropColumn(
                name: "day",
                table: "tb_attendance");

            migrationBuilder.DropColumn(
                name: "month",
                table: "tb_attendance");

            migrationBuilder.RenameColumn(
                name: "year",
                table: "tb_attendance",
                newName: "status");

            migrationBuilder.AddColumn<long>(
                name: "employeeID",
                table: "tb_attendance",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "time",
                table: "tb_attendance",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_tb_attendance_employeeID",
                table: "tb_attendance",
                column: "employeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_attendance_tb_employee_employeeID",
                table: "tb_attendance",
                column: "employeeID",
                principalTable: "tb_employee",
                principalColumn: "ID");
        }
    }
}
