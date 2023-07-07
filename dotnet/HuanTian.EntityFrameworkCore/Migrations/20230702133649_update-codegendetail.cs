using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuanTian.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class updatecodegendetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_sys_code_gen",
                table: "sys_code_gen");

            migrationBuilder.DropColumn(
                name: "table_name",
                table: "sys_code_gen");

            migrationBuilder.RenameTable(
                name: "sys_code_gen",
                newName: "sys_code_gen_detail");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sys_code_gen_detail",
                table: "sys_code_gen_detail",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_sys_code_gen_detail",
                table: "sys_code_gen_detail");

            migrationBuilder.RenameTable(
                name: "sys_code_gen_detail",
                newName: "sys_code_gen");

            migrationBuilder.AddColumn<string>(
                name: "table_name",
                table: "sys_code_gen",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "表格名字")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sys_code_gen",
                table: "sys_code_gen",
                column: "id");
        }
    }
}
