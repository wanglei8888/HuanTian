using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuanTian.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class add_permissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status",
                table: "sys_user",
                newName: "enable");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "sys_role",
                newName: "enable");

            migrationBuilder.AlterTable(
                name: "sys_menu_role",
                comment: "系统菜单角色表",
                oldComment: "系统菜单权限表")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "enable",
                table: "sys_user",
                type: "tinyint(1)",
                nullable: false,
                comment: "启用",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "状态");

            migrationBuilder.AddColumn<long>(
                name: "dept_id",
                table: "sys_user",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "所属部门Id");

            migrationBuilder.AlterColumn<bool>(
                name: "enable",
                table: "sys_role",
                type: "tinyint(1)",
                nullable: false,
                comment: "角色启用",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "角色状态");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dept_id",
                table: "sys_user");

            migrationBuilder.RenameColumn(
                name: "enable",
                table: "sys_user",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "enable",
                table: "sys_role",
                newName: "status");

            migrationBuilder.AlterTable(
                name: "sys_menu_role",
                comment: "系统菜单权限表",
                oldComment: "系统菜单角色表")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "sys_user",
                type: "int",
                nullable: false,
                comment: "状态",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "启用");

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "sys_role",
                type: "int",
                nullable: false,
                comment: "角色状态",
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldComment: "角色启用");
        }
    }
}
