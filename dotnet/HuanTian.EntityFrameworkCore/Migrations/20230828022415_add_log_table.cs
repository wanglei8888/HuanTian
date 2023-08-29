using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuanTian.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class add_log_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "create_on",
                table: "sys_log_info",
                type: "longtext",
                nullable: false,
                comment: "日志时间")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "create_on",
                table: "sys_log_error",
                type: "longtext",
                nullable: false,
                comment: "日志时间")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "create_on",
                table: "sys_log_info");

            migrationBuilder.DropColumn(
                name: "create_on",
                table: "sys_log_error");
        }
    }
}
