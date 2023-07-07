using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuanTian.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class addcodegen2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "master_id",
                table: "sys_code_gen_detail",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "主表ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "master_id",
                table: "sys_code_gen_detail");
        }
    }
}
