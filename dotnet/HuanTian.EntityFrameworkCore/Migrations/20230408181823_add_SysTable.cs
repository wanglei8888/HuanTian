using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuanTian.EntityFrameworkCore.Migrations
{
    public partial class add_SysTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_menu",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    parent_id = table.Column<int>(type: "int", nullable: false, comment: "菜单父级ID"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "菜单名字")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    path = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "菜单地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    title = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "菜单标题")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    keep_alive = table.Column<bool>(type: "tinyint(1)", nullable: true, comment: "是否缓存"),
                    icon = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "图标")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    creater = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "创建人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    create_time = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    updater = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "修改人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    update_time = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_menu", x => x.id);
                },
                comment: "系统菜单表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_menu_role",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    menu_id = table.Column<int>(type: "int", nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    creater = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "创建人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    create_time = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    updater = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "修改人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    update_time = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_menu_role", x => x.id);
                },
                comment: "系统菜单权限表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_role_info",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    role_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "角色名字")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    creater = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "创建人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    create_time = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    updater = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "修改人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    update_time = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_role_info", x => x.id);
                },
                comment: "系统角色信息表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_user_info",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "名字")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    avatar = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, comment: "图片路径")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "用户名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "用户密码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<int>(type: "int", nullable: false, comment: "状态"),
                    telephone = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "电话")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    last_login_ip = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, comment: "最后登陆IP")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    last_login_time = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "最后登陆时间"),
                    creator_id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, comment: "创建人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    create_time = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    deleted = table.Column<int>(type: "int", nullable: false, comment: "是否删除"),
                    role_id = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, comment: "权限ID")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    langguage = table.Column<int>(type: "int", nullable: false, comment: "系统语言")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_user_info", x => x.id);
                },
                comment: "用户信息表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_user_role",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    creater = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "创建人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    create_time = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间"),
                    updater = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "修改人")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    update_time = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "修改时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_user_role", x => x.id);
                },
                comment: "系统用户权限表")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sys_menu");

            migrationBuilder.DropTable(
                name: "sys_menu_role");

            migrationBuilder.DropTable(
                name: "sys_role_info");

            migrationBuilder.DropTable(
                name: "sys_user_info");

            migrationBuilder.DropTable(
                name: "sys_user_role");
        }
    }
}
