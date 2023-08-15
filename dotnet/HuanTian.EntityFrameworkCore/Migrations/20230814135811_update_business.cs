using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuanTian.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class update_business : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "tenant_id",
                table: "sys_menu",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "租户ID");

            migrationBuilder.AddColumn<long>(
                name: "update_by",
                table: "sys_menu",
                type: "bigint",
                nullable: true,
                comment: "修改人");

            migrationBuilder.AddColumn<DateTime>(
                name: "update_on",
                table: "sys_menu",
                type: "datetime(6)",
                nullable: true,
                comment: "修改时间");

            migrationBuilder.AddColumn<long>(
                name: "tenant_id",
                table: "sys_dept",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "租户ID");

            migrationBuilder.AddColumn<long>(
                name: "update_by",
                table: "sys_dept",
                type: "bigint",
                nullable: true,
                comment: "修改人");

            migrationBuilder.AddColumn<DateTime>(
                name: "update_on",
                table: "sys_dept",
                type: "datetime(6)",
                nullable: true,
                comment: "修改时间");

            migrationBuilder.AddColumn<long>(
                name: "tenant_id",
                table: "sys_apps",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "租户ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tenant_id",
                table: "sys_menu");

            migrationBuilder.DropColumn(
                name: "update_by",
                table: "sys_menu");

            migrationBuilder.DropColumn(
                name: "update_on",
                table: "sys_menu");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                table: "sys_dept");

            migrationBuilder.DropColumn(
                name: "update_by",
                table: "sys_dept");

            migrationBuilder.DropColumn(
                name: "update_on",
                table: "sys_dept");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                table: "sys_apps");
        }
    }
}
