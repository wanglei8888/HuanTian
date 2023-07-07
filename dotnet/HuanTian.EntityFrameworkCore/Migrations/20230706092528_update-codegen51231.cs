using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuanTian.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class updatecodegen51231 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "query_type",
                table: "sys_code_gen_detail",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                comment: "查询方式",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "查询方式")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "order",
                table: "sys_code_gen_detail",
                type: "int",
                maxLength: 30,
                nullable: false,
                defaultValue: 0,
                comment: "排序");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "order",
                table: "sys_code_gen_detail");

            migrationBuilder.AlterColumn<string>(
                name: "query_type",
                table: "sys_code_gen_detail",
                type: "longtext",
                nullable: true,
                comment: "查询方式",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldNullable: true,
                oldComment: "查询方式")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
