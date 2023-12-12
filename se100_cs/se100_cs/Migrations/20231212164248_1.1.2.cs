using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace se100_cs.Migrations
{
    public partial class _112 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "time",
                table: "tb_attendance_detail",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "time",
                table: "tb_attendance_detail");
        }
    }
}
