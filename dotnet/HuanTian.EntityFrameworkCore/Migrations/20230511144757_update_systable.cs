using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuanTian.EntityFrameworkCore.Migrations
{
    public partial class update_systable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "creater",
                table: "sys_user_role");

            migrationBuilder.DropColumn(
                name: "update_time",
                table: "sys_user_role");

            migrationBuilder.DropColumn(
                name: "updater",
                table: "sys_user_role");

            migrationBuilder.DropColumn(
                name: "creator_id",
                table: "sys_user_info");

            migrationBuilder.DropColumn(
                name: "deleted",
                table: "sys_user_info");

            migrationBuilder.DropColumn(
                name: "creater",
                table: "sys_role_info");

            migrationBuilder.DropColumn(
                name: "deleted",
                table: "sys_role_info");

            migrationBuilder.DropColumn(
                name: "update_time",
                table: "sys_role_info");

            migrationBuilder.DropColumn(
                name: "updater",
                table: "sys_role_info");

            migrationBuilder.DropColumn(
                name: "creater",
                table: "sys_menu_role");

            migrationBuilder.DropColumn(
                name: "update_time",
                table: "sys_menu_role");

            migrationBuilder.DropColumn(
                name: "updater",
                table: "sys_menu_role");

            migrationBuilder.DropColumn(
                name: "creater",
                table: "sys_menu");

            migrationBuilder.DropColumn(
                name: "update_time",
                table: "sys_menu");

            migrationBuilder.DropColumn(
                name: "updater",
                table: "sys_menu");

            migrationBuilder.RenameColumn(
                name: "create_time",
                table: "sys_user_role",
                newName: "create_on");

            migrationBuilder.RenameColumn(
                name: "create_time",
                table: "sys_user_info",
                newName: "create_on");

            migrationBuilder.RenameColumn(
                name: "create_time",
                table: "sys_role_info",
                newName: "create_on");

            migrationBuilder.RenameColumn(
                name: "create_time",
                table: "sys_menu_role",
                newName: "create_on");

            migrationBuilder.RenameColumn(
                name: "create_time",
                table: "sys_menu",
                newName: "create_on");

            migrationBuilder.AlterColumn<long>(
                name: "user_id",
                table: "sys_user_role",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "role_id",
                table: "sys_user_role",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "id",
                table: "sys_user_role",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<long>(
                name: "create_by",
                table: "sys_user_role",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "创建人");

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "sys_user_role",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "软删除");

            migrationBuilder.AlterColumn<string>(
                name: "telephone",
                table: "sys_user_info",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "电话",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "电话")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "role_id",
                table: "sys_user_info",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "权限ID",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldComment: "权限ID")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "last_login_ip",
                table: "sys_user_info",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "最后登陆IP",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldComment: "最后登陆IP")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "avatar",
                table: "sys_user_info",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                comment: "图片路径",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldComment: "图片路径")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "id",
                table: "sys_user_info",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<long>(
                name: "create_by",
                table: "sys_user_info",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "创建人");

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "sys_user_info",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "软删除");

            migrationBuilder.AddColumn<long>(
                name: "tenant_id",
                table: "sys_user_info",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "租户ID");

            migrationBuilder.AddColumn<long>(
                name: "update_by",
                table: "sys_user_info",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "修改人");

            migrationBuilder.AddColumn<DateTime>(
                name: "update_on",
                table: "sys_user_info",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "修改时间");

            migrationBuilder.AlterColumn<long>(
                name: "id",
                table: "sys_role_info",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<long>(
                name: "create_by",
                table: "sys_role_info",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "创建人");

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "sys_role_info",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "软删除");

            migrationBuilder.AlterColumn<long>(
                name: "role_id",
                table: "sys_menu_role",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "menu_id",
                table: "sys_menu_role",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "id",
                table: "sys_menu_role",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<long>(
                name: "create_by",
                table: "sys_menu_role",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "创建人");

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "sys_menu_role",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "软删除");

            migrationBuilder.AlterColumn<long>(
                name: "id",
                table: "sys_menu",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<long>(
                name: "create_by",
                table: "sys_menu",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "创建人");

            migrationBuilder.AddColumn<bool>(
                name: "hide_children",
                table: "sys_menu",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "隐藏子类");

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "sys_menu",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "软删除");

            migrationBuilder.AddColumn<bool>(
                name: "show",
                table: "sys_menu",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "是否显示菜单");

            migrationBuilder.AddColumn<string>(
                name: "target",
                table: "sys_menu",
                type: "longtext",
                nullable: false,
                comment: "菜单跳转方式")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "create_by",
                table: "sys_user_role");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "sys_user_role");

            migrationBuilder.DropColumn(
                name: "create_by",
                table: "sys_user_info");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "sys_user_info");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                table: "sys_user_info");

            migrationBuilder.DropColumn(
                name: "update_by",
                table: "sys_user_info");

            migrationBuilder.DropColumn(
                name: "update_on",
                table: "sys_user_info");

            migrationBuilder.DropColumn(
                name: "create_by",
                table: "sys_role_info");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "sys_role_info");

            migrationBuilder.DropColumn(
                name: "create_by",
                table: "sys_menu_role");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "sys_menu_role");

            migrationBuilder.DropColumn(
                name: "create_by",
                table: "sys_menu");

            migrationBuilder.DropColumn(
                name: "hide_children",
                table: "sys_menu");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "sys_menu");

            migrationBuilder.DropColumn(
                name: "show",
                table: "sys_menu");

            migrationBuilder.DropColumn(
                name: "target",
                table: "sys_menu");

            migrationBuilder.RenameColumn(
                name: "create_on",
                table: "sys_user_role",
                newName: "create_time");

            migrationBuilder.RenameColumn(
                name: "create_on",
                table: "sys_user_info",
                newName: "create_time");

            migrationBuilder.RenameColumn(
                name: "create_on",
                table: "sys_role_info",
                newName: "create_time");

            migrationBuilder.RenameColumn(
                name: "create_on",
                table: "sys_menu_role",
                newName: "create_time");

            migrationBuilder.RenameColumn(
                name: "create_on",
                table: "sys_menu",
                newName: "create_time");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "sys_user_role",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "role_id",
                table: "sys_user_role",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "sys_user_role",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "creater",
                table: "sys_user_role",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "创建人")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "update_time",
                table: "sys_user_role",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "修改时间");

            migrationBuilder.AddColumn<string>(
                name: "updater",
                table: "sys_user_role",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "修改人")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "sys_user_info",
                keyColumn: "telephone",
                keyValue: null,
                column: "telephone",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "telephone",
                table: "sys_user_info",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "电话",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "电话")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "sys_user_info",
                keyColumn: "role_id",
                keyValue: null,
                column: "role_id",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "role_id",
                table: "sys_user_info",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                comment: "权限ID",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "权限ID")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "sys_user_info",
                keyColumn: "last_login_ip",
                keyValue: null,
                column: "last_login_ip",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "last_login_ip",
                table: "sys_user_info",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "最后登陆IP",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "最后登陆IP")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "sys_user_info",
                keyColumn: "avatar",
                keyValue: null,
                column: "avatar",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "avatar",
                table: "sys_user_info",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                comment: "图片路径",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldNullable: true,
                oldComment: "图片路径")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "sys_user_info",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "creator_id",
                table: "sys_user_info",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "创建人")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "deleted",
                table: "sys_user_info",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "是否删除");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "sys_role_info",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "creater",
                table: "sys_role_info",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "创建人")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "sys_role_info",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                comment: "是否删除");

            migrationBuilder.AddColumn<DateTime>(
                name: "update_time",
                table: "sys_role_info",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "修改时间");

            migrationBuilder.AddColumn<string>(
                name: "updater",
                table: "sys_role_info",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "修改人")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "role_id",
                table: "sys_menu_role",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "menu_id",
                table: "sys_menu_role",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "sys_menu_role",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "creater",
                table: "sys_menu_role",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "创建人")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "update_time",
                table: "sys_menu_role",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "修改时间");

            migrationBuilder.AddColumn<string>(
                name: "updater",
                table: "sys_menu_role",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "修改人")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "sys_menu",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "creater",
                table: "sys_menu",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "创建人")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "update_time",
                table: "sys_menu",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "修改时间");

            migrationBuilder.AddColumn<string>(
                name: "updater",
                table: "sys_menu",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "修改人")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
