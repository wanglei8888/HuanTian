using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuanTian.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class updatecodegen4159 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "drop_down_list",
                table: "sys_code_gen_detail",
                newName: "drop_down_code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "drop_down_code",
                table: "sys_code_gen_detail",
                newName: "drop_down_list");
        }
    }
}
