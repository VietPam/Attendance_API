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
                name: "tb_atd_status",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_atd_status", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tb_department",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_department", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tb_role",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_role", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tb_setting",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    company_name = table.Column<string>(type: "text", nullable: false),
                    company_code = table.Column<string>(type: "text", nullable: false),
                    start_time_hour = table.Column<int>(type: "integer", nullable: false),
                    start_time_minute = table.Column<int>(type: "integer", nullable: false),
                    salary_per_coef = table.Column<int>(type: "integer", nullable: false),
                    payment_date = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_setting", x => x.ID);
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
                    departmentID = table.Column<long>(type: "bigint", nullable: true),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: false)
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
                    isDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    phoneNumber = table.Column<string>(type: "text", nullable: false),
                    avatar = table.Column<string>(type: "text", nullable: false),
                    birth_day = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    gender = table.Column<bool>(type: "boolean", nullable: false),
                    cmnd = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<int>(type: "integer", nullable: true),
                    positionID = table.Column<long>(type: "bigint", nullable: true),
                    departmentID = table.Column<long>(type: "bigint", nullable: true),
                    IdHub = table.Column<string>(type: "text", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "tb_attendance",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    employeeID = table.Column<long>(type: "bigint", nullable: false),
                    stateID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_attendance", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tb_attendance_tb_atd_status_stateID",
                        column: x => x.stateID,
                        principalTable: "tb_atd_status",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_attendance_tb_employee_employeeID",
                        column: x => x.employeeID,
                        principalTable: "tb_employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_payroll",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    salary = table.Column<long>(type: "bigint", nullable: false),
                    receive_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    employeeID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_payroll", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tb_payroll_tb_employee_employeeID",
                        column: x => x.employeeID,
                        principalTable: "tb_employee",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_attendance_employeeID",
                table: "tb_attendance",
                column: "employeeID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_attendance_stateID",
                table: "tb_attendance",
                column: "stateID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_employee_departmentID",
                table: "tb_employee",
                column: "departmentID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_employee_positionID",
                table: "tb_employee",
                column: "positionID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_payroll_employeeID",
                table: "tb_payroll",
                column: "employeeID");

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
                name: "tb_role");

            migrationBuilder.DropTable(
                name: "tb_setting");

            migrationBuilder.DropTable(
                name: "tb_atd_status");

            migrationBuilder.DropTable(
                name: "tb_employee");

            migrationBuilder.DropTable(
                name: "tb_position");

            migrationBuilder.DropTable(
                name: "tb_department");
        }
    }
}
