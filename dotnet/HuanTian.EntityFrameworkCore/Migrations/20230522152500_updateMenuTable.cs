using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuanTian.EntityFrameworkCore.Migrations
{
    public partial class updateMenuTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "component",
                table: "sys_menu",
                type: "longtext",
                nullable: true,
                comment: "菜单前端绑定值",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "菜单类型")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "menu_type",
                table: "sys_menu",
                type: "nvarchar(50)",
                nullable: true,
                comment: "排序,越大越靠前");

            migrationBuilder.AddColumn<int>(
                name: "order",
                table: "sys_menu",
                type: "int",
                nullable: true,
                comment: "排序,越大越靠前");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "menu_type",
                table: "sys_menu");

            migrationBuilder.DropColumn(
                name: "order",
                table: "sys_menu");

            migrationBuilder.AlterColumn<string>(
                name: "component",
                table: "sys_menu",
                type: "longtext",
                nullable: true,
                comment: "菜单类型",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "菜单前端绑定值")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
