using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuanTian.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class update_user_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "language",
                table: "sys_user",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "zh_CN",
                comment: "系统语言 (zh_CN 中文, en_US 英文)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "系统语言 (0 中文, 1 英文)")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "language",
                table: "sys_user",
                type: "int",
                nullable: false,
                comment: "系统语言 (0 中文, 1 英文)",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldDefaultValue: "zh_CN",
                oldComment: "系统语言 (zh_CN 中文, en_US 英文)")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
