using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuanTian.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class updatecodegen415 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "frontend_type",
                table: "sys_code_gen_detail",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                comment: "前端显示类型")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "frontend_type",
                table: "sys_code_gen_detail");
        }
    }
}
