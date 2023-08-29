using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuanTian.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class add_log_table2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "create_on",
                table: "sys_log_info",
                type: "datetime(6)",
                nullable: false,
                comment: "日志时间",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldComment: "日志时间")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<long>(
                name: "user_id",
                table: "sys_log_info",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "用户ID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "create_on",
                table: "sys_log_error",
                type: "datetime(6)",
                nullable: false,
                comment: "日志时间",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldComment: "日志时间")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<long>(
                name: "user_id",
                table: "sys_log_error",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "用户ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_id",
                table: "sys_log_info");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "sys_log_error");

            migrationBuilder.AlterColumn<string>(
                name: "create_on",
                table: "sys_log_info",
                type: "longtext",
                nullable: false,
                comment: "日志时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "日志时间")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "create_on",
                table: "sys_log_error",
                type: "longtext",
                nullable: false,
                comment: "日志时间",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "日志时间")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
