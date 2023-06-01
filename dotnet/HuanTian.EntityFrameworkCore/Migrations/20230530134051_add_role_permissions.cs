using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuanTian.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class add_role_permissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "sys_code_gen",
                comment: "代码生成数据表格")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

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
                name: "sys_permissions",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, comment: "权限Code")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true, comment: "权限名字")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    enable = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否启用"),
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sys_dept");

            migrationBuilder.DropTable(
                name: "sys_permissions");

            migrationBuilder.DropTable(
                name: "sys_role_permissions");

            migrationBuilder.AlterTable(
                name: "sys_code_gen",
                oldComment: "代码生成数据表格")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
