using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuanTian.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class add_systenant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "describe",
                table: "sys_role",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "角色描述",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldComment: "角色描述")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_tenant",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tenant_admin = table.Column<long>(type: "bigint", nullable: false, comment: "租户管理员"),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "租户名字")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email_config = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, comment: "邮件配置")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    enable = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否启用"),
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
                    table.PrimaryKey("PK_sys_tenant", x => x.id);
                },
                comment: "系统租户表")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sys_tenant");

            migrationBuilder.AlterColumn<string>(
                name: "describe",
                table: "sys_role",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                comment: "角色描述",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldComment: "角色描述")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
