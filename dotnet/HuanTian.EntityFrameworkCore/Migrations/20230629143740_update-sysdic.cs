using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuanTian.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class updatesysdic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "code",
                table: "sys_dictionary");

            migrationBuilder.DropColumn(
                name: "create_by",
                table: "sys_dictionary");

            migrationBuilder.DropColumn(
                name: "create_on",
                table: "sys_dictionary");

            migrationBuilder.DropColumn(
                name: "value",
                table: "sys_dictionary");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "sys_dictionary",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "字典名字",
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldMaxLength: 60,
                oldComment: "字典名字")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "enable",
                table: "sys_dictionary",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "是否启用");

            migrationBuilder.CreateTable(
                name: "sys_dictionary_detail",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    master_id = table.Column<long>(type: "bigint", nullable: false, comment: "主表Id"),
                    code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "系统字典表")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "字典值")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "字典名字")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    order = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    enable = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否启用"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "软删除"),
                    create_by = table.Column<long>(type: "bigint", nullable: true, comment: "创建人"),
                    create_on = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "创建时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_dictionary_detail", x => x.id);
                },
                comment: "系统字典详情表")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sys_dictionary_detail");

            migrationBuilder.DropColumn(
                name: "enable",
                table: "sys_dictionary");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "sys_dictionary",
                type: "varchar(60)",
                maxLength: 60,
                nullable: false,
                comment: "字典名字",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "字典名字")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "sys_dictionary",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "系统字典表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<long>(
                name: "create_by",
                table: "sys_dictionary",
                type: "bigint",
                nullable: true,
                comment: "创建人");

            migrationBuilder.AddColumn<DateTime>(
                name: "create_on",
                table: "sys_dictionary",
                type: "datetime(6)",
                nullable: true,
                comment: "创建时间");

            migrationBuilder.AddColumn<string>(
                name: "value",
                table: "sys_dictionary",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "字典值")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
