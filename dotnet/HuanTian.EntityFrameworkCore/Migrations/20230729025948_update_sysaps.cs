using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuanTian.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class update_sysaps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sys_app");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sys_apps");

            migrationBuilder.CreateTable(
                name: "sys_app",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, comment: "编码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    create_by = table.Column<long>(type: "bigint", nullable: true, comment: "创建人"),
                    create_on = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "创建时间"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "软删除"),
                    describe = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, comment: "描述")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    enable = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "启用"),
                    name = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, comment: "列名字")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    order = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    update_by = table.Column<long>(type: "bigint", nullable: true, comment: "修改人"),
                    update_on = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "修改时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_app", x => x.id);
                },
                comment: "系统应用表")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
