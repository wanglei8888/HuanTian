using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuanTian.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class addcodegen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "sys_code_gen_detail",
                comment: "代码生成数据详情表",
                oldComment: "代码生成数据表格")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sys_code_gen");

            migrationBuilder.AlterTable(
                name: "sys_code_gen_detail",
                comment: "代码生成数据表格",
                oldComment: "代码生成数据详情表")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
