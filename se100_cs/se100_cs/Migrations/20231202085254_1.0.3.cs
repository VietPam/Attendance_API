using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace se100_cs.Migrations
{
    public partial class _103 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isdeleted",
                table: "tb_role",
                newName: "isDeleted");

            migrationBuilder.RenameColumn(
                name: "is_deleted",
                table: "tb_employee",
                newName: "isDeleted");

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "tb_position",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "tb_position");

            migrationBuilder.RenameColumn(
                name: "isDeleted",
                table: "tb_role",
                newName: "isdeleted");

            migrationBuilder.RenameColumn(
                name: "isDeleted",
                table: "tb_employee",
                newName: "is_deleted");
        }
    }
}
