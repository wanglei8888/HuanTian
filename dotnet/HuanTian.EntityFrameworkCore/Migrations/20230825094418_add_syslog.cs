using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuanTian.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class add_syslog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sys_log_error",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false, comment: "租户ID"),
                    level = table.Column<int>(type: "int", nullable: false, comment: "日志等级 trace0、Debug1、Information2、Warning3、Error4、Critical5、None6"),
                    msg = table.Column<string>(type: "longtext", nullable: false, comment: "日志信息")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    path = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "错误地址")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_log_error", x => x.id);
                },
                comment: "错误日志")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "sys_log_info",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false, comment: "租户ID"),
                    level = table.Column<int>(type: "int", nullable: false, comment: "日志等级 trace0、Debug1、Information2、Warning3、Error4、Critical5、None6"),
                    msg = table.Column<string>(type: "longtext", nullable: false, comment: "日志信息")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_log_info", x => x.id);
                },
                comment: "普通日志")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sys_log_error");

            migrationBuilder.DropTable(
                name: "sys_log_info");
        }
    }
}
