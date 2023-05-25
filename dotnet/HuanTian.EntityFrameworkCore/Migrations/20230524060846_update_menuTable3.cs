using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuanTian.EntityFrameworkCore.Migrations
{
    public partial class update_menuTable3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "redirect",
                table: "sys_menu",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "菜单跳转地址",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "菜单跳转地址")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "menu_type",
                table: "sys_menu",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "菜单类型",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "菜单类型,0系统菜单、1业务菜单")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "component",
                table: "sys_menu",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "菜单前端绑定值",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "菜单前端绑定值")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "redirect",
                table: "sys_menu",
                type: "longtext",
                nullable: true,
                comment: "菜单跳转地址",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "菜单跳转地址")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "menu_type",
                table: "sys_menu",
                type: "int",
                nullable: true,
                comment: "菜单类型,0系统菜单、1业务菜单",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "菜单类型")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "component",
                table: "sys_menu",
                type: "longtext",
                nullable: true,
                comment: "菜单前端绑定值",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "菜单前端绑定值")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
