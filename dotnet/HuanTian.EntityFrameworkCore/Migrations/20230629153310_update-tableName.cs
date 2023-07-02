using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuanTian.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class updatetableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_sys_dictionary_detail",
                table: "sys_dictionary_detail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sys_dictionary",
                table: "sys_dictionary");

            migrationBuilder.RenameTable(
                name: "sys_dictionary_detail",
                newName: "sys_dic_detail");

            migrationBuilder.RenameTable(
                name: "sys_dictionary",
                newName: "sys_dic");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sys_dic_detail",
                table: "sys_dic_detail",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sys_dic",
                table: "sys_dic",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_sys_dic_detail",
                table: "sys_dic_detail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sys_dic",
                table: "sys_dic");

            migrationBuilder.RenameTable(
                name: "sys_dic_detail",
                newName: "sys_dictionary_detail");

            migrationBuilder.RenameTable(
                name: "sys_dic",
                newName: "sys_dictionary");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sys_dictionary_detail",
                table: "sys_dictionary_detail",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sys_dictionary",
                table: "sys_dictionary",
                column: "id");
        }
    }
}
