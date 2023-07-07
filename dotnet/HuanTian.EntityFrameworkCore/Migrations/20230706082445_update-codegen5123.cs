using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuanTian.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class updatecodegen5123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "query_type",
                table: "sys_code_gen_detail",
                type: "longtext",
                nullable: true,
                comment: "查询方式",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldComment: "查询方式")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "sys_code_gen_detail",
                keyColumn: "query_type",
                keyValue: null,
                column: "query_type",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "query_type",
                table: "sys_code_gen_detail",
                type: "longtext",
                nullable: false,
                comment: "查询方式",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "查询方式")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
