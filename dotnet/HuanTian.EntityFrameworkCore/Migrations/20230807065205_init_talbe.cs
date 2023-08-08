using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuanTian.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class init_talbe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_apps",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, comment: "列名字")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    code = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, comment: "编码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    order = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    enable = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "启用"),
                    describe = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, comment: "描述")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "软删除"),
                    create_by = table.Column<long>(type: "bigint", nullable: true, comment: "创建人"),
                    create_on = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "创建时间"),
                    update_by = table.Column<long>(type: "bigint", nullable: true, comment: "修改人"),
                    update_on = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "修改时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_apps", x => x.id);
                },
                comment: "系统应用表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_code_gen",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, comment: "名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    table_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "表格名字")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    menu_id = table.Column<long>(type: "bigint", nullable: false, comment: "所属菜单"),
                    generation_way = table.Column<int>(type: "int", nullable: false, comment: "生成方式"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "软删除"),
                    create_by = table.Column<long>(type: "bigint", nullable: true, comment: "创建人"),
                    create_on = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "创建时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_code_gen", x => x.id);
                },
                comment: "代码生成数据表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_code_gen_detail",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    master_id = table.Column<long>(type: "bigint", nullable: false, comment: "主表ID"),
                    db_column_name = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, comment: "列名字")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    column_description = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, comment: "列备注")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    data_type = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, comment: "列类型")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    frontend_type = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, comment: "前端显示类型")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    drop_down_code = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true, comment: "下拉框绑定字典值 如果没有就是不是下拉框")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    query_type = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true, comment: "查询方式")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    order = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    query_parameters = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否是查询参数"),
                    required = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否必填"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "软删除"),
                    create_by = table.Column<long>(type: "bigint", nullable: true, comment: "创建人"),
                    create_on = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "创建时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_code_gen_detail", x => x.id);
                },
                comment: "代码生成数据详情表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_dept",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    parent_id = table.Column<long>(type: "bigint", nullable: false, comment: "父级部门id"),
                    name = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, comment: "部门名字")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    describe = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, comment: "部门描述")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    enable = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "部门启用"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "软删除"),
                    create_by = table.Column<long>(type: "bigint", nullable: true, comment: "创建人"),
                    create_on = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "创建时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_dept", x => x.id);
                },
                comment: "系统部门表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_dic",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "字典名字")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "系统字典表")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    enable = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否启用"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "软删除")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_dic", x => x.id);
                },
                comment: "系统字典表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_dic_detail",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    master_id = table.Column<long>(type: "bigint", nullable: false, comment: "主表Id"),
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
                    table.PrimaryKey("PK_sys_dic_detail", x => x.id);
                },
                comment: "系统字典详情表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_email_template",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "字典名字")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    file_path = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false, comment: "文件地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    online_path = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false, comment: "文件线上地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "软删除"),
                    create_by = table.Column<long>(type: "bigint", nullable: true, comment: "创建人"),
                    create_on = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "创建时间"),
                    update_by = table.Column<long>(type: "bigint", nullable: true, comment: "修改人"),
                    update_on = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "修改时间"),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false, comment: "租户ID")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_email_template", x => x.id);
                },
                comment: "系统邮箱模板表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_menu",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    parent_id = table.Column<long>(type: "bigint", nullable: false, comment: "菜单父级ID"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "菜单名字")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    path = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "菜单地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    title = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "菜单标题")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    keep_alive = table.Column<bool>(type: "tinyint(1)", nullable: true, comment: "是否缓存"),
                    icon = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "图标")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    show = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否显示菜单"),
                    redirect = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "菜单跳转地址")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hide_children = table.Column<bool>(type: "tinyint(1)", nullable: true, comment: "隐藏子类"),
                    component = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "菜单前端绑定值")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    menu_type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "菜单类型")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    order = table.Column<int>(type: "int", nullable: true, comment: "排序,越大越靠前"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "软删除"),
                    create_by = table.Column<long>(type: "bigint", nullable: true, comment: "创建人"),
                    create_on = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "创建时间")
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
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    menu_id = table.Column<long>(type: "bigint", nullable: false),
                    role_id = table.Column<long>(type: "bigint", nullable: false),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "软删除"),
                    create_by = table.Column<long>(type: "bigint", nullable: true, comment: "创建人"),
                    create_on = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "创建时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_menu_role", x => x.id);
                },
                comment: "系统菜单角色表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_permissions",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    menu_id = table.Column<long>(type: "bigint", nullable: true, comment: "绑定系统菜单Id"),
                    code = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, comment: "权限Code")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true, comment: "权限名字")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type = table.Column<int>(type: "int", nullable: true, comment: "权限类型"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "软删除"),
                    create_by = table.Column<long>(type: "bigint", nullable: true, comment: "创建人"),
                    create_on = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "创建时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_permissions", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_role",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    role_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "角色名字")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    describe = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, comment: "角色描述")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    enable = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "角色启用"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "软删除"),
                    create_by = table.Column<long>(type: "bigint", nullable: true, comment: "创建人"),
                    create_on = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "创建时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_role", x => x.id);
                },
                comment: "系统角色信息表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_role_permissions",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    role_id = table.Column<long>(type: "bigint", nullable: false),
                    permissions_id = table.Column<long>(type: "bigint", nullable: false),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "软删除"),
                    create_by = table.Column<long>(type: "bigint", nullable: true, comment: "创建人"),
                    create_on = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "创建时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_role_permissions", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_user",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    dept_id = table.Column<long>(type: "bigint", nullable: false, comment: "所属部门Id"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "名字")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    avatar = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, comment: "图片路径")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "用户名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "用户密码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type = table.Column<int>(type: "int", nullable: false, comment: "账号类型"),
                    enable = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "启用"),
                    telephone = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "电话")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    last_login_ip = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, comment: "最后登陆IP")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    last_login_time = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "最后登陆时间"),
                    language = table.Column<int>(type: "int", nullable: false, comment: "系统语言"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "软删除"),
                    create_by = table.Column<long>(type: "bigint", nullable: true, comment: "创建人"),
                    create_on = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "创建时间"),
                    update_by = table.Column<long>(type: "bigint", nullable: true, comment: "修改人"),
                    update_on = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "修改时间"),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false, comment: "租户ID")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_user", x => x.id);
                },
                comment: "用户信息表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_user_role",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    role_id = table.Column<long>(type: "bigint", nullable: false),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "软删除"),
                    create_by = table.Column<long>(type: "bigint", nullable: true, comment: "创建人"),
                    create_on = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "创建时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_user_role", x => x.id);
                },
                comment: "系统用户权限表")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sys_apps");

            migrationBuilder.DropTable(
                name: "sys_code_gen");

            migrationBuilder.DropTable(
                name: "sys_code_gen_detail");

            migrationBuilder.DropTable(
                name: "sys_dept");

            migrationBuilder.DropTable(
                name: "sys_dic");

            migrationBuilder.DropTable(
                name: "sys_dic_detail");

            migrationBuilder.DropTable(
                name: "sys_email_template");

            migrationBuilder.DropTable(
                name: "sys_menu");

            migrationBuilder.DropTable(
                name: "sys_menu_role");

            migrationBuilder.DropTable(
                name: "sys_permissions");

            migrationBuilder.DropTable(
                name: "sys_role");

            migrationBuilder.DropTable(
                name: "sys_role_permissions");

            migrationBuilder.DropTable(
                name: "sys_user");

            migrationBuilder.DropTable(
                name: "sys_user_role");
        }
    }
}
