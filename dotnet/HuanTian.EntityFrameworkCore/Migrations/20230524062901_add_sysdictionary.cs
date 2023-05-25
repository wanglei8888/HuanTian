using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuanTian.EntityFrameworkCore.Migrations
{
    public partial class add_sysdictionary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sys_dictionary",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "系统字典表")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "字典值")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false, comment: "字典名字")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "软删除"),
                    create_by = table.Column<long>(type: "bigint", nullable: true, comment: "创建人"),
                    create_on = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "创建时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_dictionary", x => x.id);
                },
                comment: "系统字典表")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sys_dictionary");
        }
    }
}
