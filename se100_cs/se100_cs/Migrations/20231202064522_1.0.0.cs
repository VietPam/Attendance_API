using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace se100_cs.Migrations
{
    public partial class _100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_department",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_department", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tb_payroll",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    salary = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_payroll", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tb_role",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_role", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tb_position",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    salary_coeffcient = table.Column<long>(type: "bigint", nullable: false),
                    departmentID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_position", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tb_position_tb_department_departmentID",
                        column: x => x.departmentID,
                        principalTable: "tb_department",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "tb_employee",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    token = table.Column<string>(type: "text", nullable: false),
                    fullName = table.Column<string>(type: "text", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    phoneNumber = table.Column<string>(type: "text", nullable: false),
                    avatar = table.Column<string>(type: "text", nullable: false),
                    birth_day = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    gender = table.Column<bool>(type: "boolean", nullable: false),
                    cmnd = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    roleID = table.Column<long>(type: "bigint", nullable: true),
                    positionID = table.Column<long>(type: "bigint", nullable: true),
                    departmentID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_employee", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tb_employee_tb_department_departmentID",
                        column: x => x.departmentID,
                        principalTable: "tb_department",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_tb_employee_tb_position_positionID",
                        column: x => x.positionID,
                        principalTable: "tb_position",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_tb_employee_tb_role_roleID",
                        column: x => x.roleID,
                        principalTable: "tb_role",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "tb_attendance",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    employeeID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_attendance", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tb_attendance_tb_employee_employeeID",
                        column: x => x.employeeID,
                        principalTable: "tb_employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_attendance_employeeID",
                table: "tb_attendance",
                column: "employeeID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_employee_departmentID",
                table: "tb_employee",
                column: "departmentID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_employee_positionID",
                table: "tb_employee",
                column: "positionID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_employee_roleID",
                table: "tb_employee",
                column: "roleID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_position_departmentID",
                table: "tb_position",
                column: "departmentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_attendance");

            migrationBuilder.DropTable(
                name: "tb_payroll");

            migrationBuilder.DropTable(
                name: "tb_employee");

            migrationBuilder.DropTable(
                name: "tb_position");

            migrationBuilder.DropTable(
                name: "tb_role");

            migrationBuilder.DropTable(
                name: "tb_department");
        }
    }
}
