using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuanTian.EntityFrameworkCore.Migrations
{
    public partial class updateMenuTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "menu_type",
                table: "sys_menu",
                type: "int",
                nullable: true,
                comment: "菜单类型,0系统菜单、1业务菜单",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true,
                oldComment: "排序,越大越靠前");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "menu_type",
                table: "sys_menu",
                type: "nvarchar(50)",
                nullable: true,
                comment: "排序,越大越靠前",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "菜单类型,0系统菜单、1业务菜单");
        }
    }
}
