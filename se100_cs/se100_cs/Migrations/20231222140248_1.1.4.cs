using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace se100_cs.Migrations
{
    public partial class _114 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdHub",
                table: "tb_employee",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdHub",
                table: "tb_employee");
        }
    }
}
