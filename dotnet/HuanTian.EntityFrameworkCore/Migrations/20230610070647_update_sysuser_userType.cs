using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuanTian.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class update_sysuser_userType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "type",
                table: "sys_user",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "账号类型");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                table: "sys_user");
        }
    }
}
