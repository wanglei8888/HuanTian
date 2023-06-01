using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuanTian.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class update_permssionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "menu_id",
                table: "sys_permissions",
                type: "bigint",
                nullable: true,
                comment: "绑定系统菜单Id");

            migrationBuilder.AddColumn<int>(
                name: "type",
                table: "sys_permissions",
                type: "int",
                nullable: true,
                comment: "权限类型");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "menu_id",
                table: "sys_permissions");

            migrationBuilder.DropColumn(
                name: "type",
                table: "sys_permissions");
        }
    }
}
