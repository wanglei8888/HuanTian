using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuanTian.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class update_emailTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "file_path",
                table: "sys_email_template");

            migrationBuilder.DropColumn(
                name: "online_path",
                table: "sys_email_template");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "sys_email_template",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "名称",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "字典名字")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "enable",
                table: "sys_email_template",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "是否启用");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "enable",
                table: "sys_email_template");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "sys_email_template",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "字典名字",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "名称")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "file_path",
                table: "sys_email_template",
                type: "varchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "",
                comment: "文件地址")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "online_path",
                table: "sys_email_template",
                type: "varchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "",
                comment: "文件线上地址")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
