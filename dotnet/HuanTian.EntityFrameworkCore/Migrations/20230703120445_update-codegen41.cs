using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuanTian.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class updatecodegen41 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "query_type",
                table: "sys_code_gen_detail",
                type: "longtext",
                nullable: false,
                comment: "查询方式")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "required",
                table: "sys_code_gen_detail",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "是否必填");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "query_type",
                table: "sys_code_gen_detail");

            migrationBuilder.DropColumn(
                name: "required",
                table: "sys_code_gen_detail");
        }
    }
}
