﻿// <auto-generated />
using System;
using HuanTian.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HuanTian.EntityFrameworkCore.Migrations
{
    [DbContext(typeof(EfSqlContext))]
    partial class EfSqlContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("HuanTian.Infrastructure.SysAppsDO", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("code")
                        .HasComment("编码");

                    b.Property<long?>("CreateBy")
                        .HasColumnType("bigint")
                        .HasColumnName("create_by")
                        .HasComment("创建人");

                    b.Property<DateTime?>("CreateOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_on")
                        .HasComment("创建时间");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("deleted")
                        .HasComment("软删除");

                    b.Property<string>("Describe")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("describe")
                        .HasComment("描述");

                    b.Property<bool>("Enable")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("enable")
                        .HasComment("启用");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("name")
                        .HasComment("列名字");

                    b.Property<int>("Order")
                        .HasColumnType("int")
                        .HasColumnName("order")
                        .HasComment("排序");

                    b.Property<long?>("UpdateBy")
                        .HasColumnType("bigint")
                        .HasColumnName("update_by")
                        .HasComment("修改人");

                    b.Property<DateTime?>("UpdateOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("update_on")
                        .HasComment("修改时间");

                    b.HasKey("Id");

                    b.ToTable("sys_apps", t =>
                        {
                            t.HasComment("系统应用表");
                        });
                });

            modelBuilder.Entity("HuanTian.Infrastructure.SysCodeGenDO", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<long?>("CreateBy")
                        .HasColumnType("bigint")
                        .HasColumnName("create_by")
                        .HasComment("创建人");

                    b.Property<DateTime?>("CreateOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_on")
                        .HasComment("创建时间");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("deleted")
                        .HasComment("软删除");

                    b.Property<int>("GenerationWay")
                        .HasColumnType("int")
                        .HasColumnName("generation_way")
                        .HasComment("生成方式");

                    b.Property<long>("MenuId")
                        .HasColumnType("bigint")
                        .HasColumnName("menu_id")
                        .HasComment("所属菜单");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("name")
                        .HasComment("名称");

                    b.Property<string>("TableName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("table_name")
                        .HasComment("表格名字");

                    b.HasKey("Id");

                    b.ToTable("sys_code_gen", t =>
                        {
                            t.HasComment("代码生成数据表");
                        });
                });

            modelBuilder.Entity("HuanTian.Infrastructure.SysCodeGenDetailDO", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("ColumnDescription")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("column_description")
                        .HasComment("列备注");

                    b.Property<long?>("CreateBy")
                        .HasColumnType("bigint")
                        .HasColumnName("create_by")
                        .HasComment("创建人");

                    b.Property<DateTime?>("CreateOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_on")
                        .HasComment("创建时间");

                    b.Property<string>("DataType")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("data_type")
                        .HasComment("列类型");

                    b.Property<string>("DbColumnName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("db_column_name")
                        .HasComment("列名字");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("deleted")
                        .HasComment("软删除");

                    b.Property<string>("DropDownCode")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("drop_down_code")
                        .HasComment("下拉框绑定字典值 如果没有就是不是下拉框");

                    b.Property<string>("FrontendType")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("frontend_type")
                        .HasComment("前端显示类型");

                    b.Property<long>("MasterId")
                        .HasColumnType("bigint")
                        .HasColumnName("master_id")
                        .HasComment("主表ID");

                    b.Property<int>("Order")
                        .HasColumnType("int")
                        .HasColumnName("order")
                        .HasComment("排序");

                    b.Property<bool>("QueryParameters")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("query_parameters")
                        .HasComment("是否是查询参数");

                    b.Property<string>("QueryType")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("query_type")
                        .HasComment("查询方式");

                    b.Property<bool>("Required")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("required")
                        .HasComment("是否必填");

                    b.HasKey("Id");

                    b.ToTable("sys_code_gen_detail", t =>
                        {
                            t.HasComment("代码生成数据详情表");
                        });
                });

            modelBuilder.Entity("HuanTian.Infrastructure.SysDeptDO", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<long?>("CreateBy")
                        .HasColumnType("bigint")
                        .HasColumnName("create_by")
                        .HasComment("创建人");

                    b.Property<DateTime?>("CreateOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_on")
                        .HasComment("创建时间");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("deleted")
                        .HasComment("软删除");

                    b.Property<string>("Describe")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("describe")
                        .HasComment("部门描述");

                    b.Property<bool>("Enable")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("enable")
                        .HasComment("部门启用");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("name")
                        .HasComment("部门名字");

                    b.Property<long>("ParentId")
                        .HasColumnType("bigint")
                        .HasColumnName("parent_id")
                        .HasComment("父级部门id");

                    b.HasKey("Id");

                    b.ToTable("sys_dept", t =>
                        {
                            t.HasComment("系统部门表");
                        });
                });

            modelBuilder.Entity("HuanTian.Infrastructure.SysDicDO", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("code")
                        .HasComment("系统字典表");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("deleted")
                        .HasComment("软删除");

                    b.Property<bool>("Enable")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("enable")
                        .HasComment("是否启用");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name")
                        .HasComment("字典名字");

                    b.HasKey("Id");

                    b.ToTable("sys_dic", t =>
                        {
                            t.HasComment("系统字典表");
                        });
                });

            modelBuilder.Entity("HuanTian.Infrastructure.SysDicDetailDO", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<long?>("CreateBy")
                        .HasColumnType("bigint")
                        .HasColumnName("create_by")
                        .HasComment("创建人");

                    b.Property<DateTime?>("CreateOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_on")
                        .HasComment("创建时间");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("deleted")
                        .HasComment("软删除");

                    b.Property<bool>("Enable")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("enable")
                        .HasComment("是否启用");

                    b.Property<long>("MasterId")
                        .HasColumnType("bigint")
                        .HasColumnName("master_id")
                        .HasComment("主表Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name")
                        .HasComment("字典名字");

                    b.Property<int>("Order")
                        .HasColumnType("int")
                        .HasColumnName("order")
                        .HasComment("排序");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("value")
                        .HasComment("字典值");

                    b.HasKey("Id");

                    b.ToTable("sys_dic_detail", t =>
                        {
                            t.HasComment("系统字典详情表");
                        });
                });

            modelBuilder.Entity("HuanTian.Infrastructure.SysEmailTemplateDO", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<long?>("CreateBy")
                        .HasColumnType("bigint")
                        .HasColumnName("create_by")
                        .HasComment("创建人");

                    b.Property<DateTime?>("CreateOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_on")
                        .HasComment("创建时间");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("deleted")
                        .HasComment("软删除");

                    b.Property<bool>("Enable")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("enable")
                        .HasComment("是否启用");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name")
                        .HasComment("名称");

                    b.Property<long>("TenantId")
                        .HasColumnType("bigint")
                        .HasColumnName("tenant_id")
                        .HasComment("租户ID");

                    b.Property<long?>("UpdateBy")
                        .HasColumnType("bigint")
                        .HasColumnName("update_by")
                        .HasComment("修改人");

                    b.Property<DateTime?>("UpdateOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("update_on")
                        .HasComment("修改时间");

                    b.HasKey("Id");

                    b.ToTable("sys_email_template", t =>
                        {
                            t.HasComment("系统邮箱模板表");
                        });
                });

            modelBuilder.Entity("HuanTian.Infrastructure.SysMenuDO", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("Component")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("component")
                        .HasComment("菜单前端绑定值");

                    b.Property<long?>("CreateBy")
                        .HasColumnType("bigint")
                        .HasColumnName("create_by")
                        .HasComment("创建人");

                    b.Property<DateTime?>("CreateOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_on")
                        .HasComment("创建时间");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("deleted")
                        .HasComment("软删除");

                    b.Property<bool?>("HideChildren")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("hide_children")
                        .HasComment("隐藏子类");

                    b.Property<string>("Icon")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("icon")
                        .HasComment("图标");

                    b.Property<bool?>("KeepAlive")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("keep_alive")
                        .HasComment("是否缓存");

                    b.Property<string>("MenuType")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("menu_type")
                        .HasComment("菜单类型");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name")
                        .HasComment("菜单名字");

                    b.Property<int?>("Order")
                        .HasColumnType("int")
                        .HasColumnName("order")
                        .HasComment("排序,越大越靠前");

                    b.Property<long>("ParentId")
                        .HasColumnType("bigint")
                        .HasColumnName("parent_id")
                        .HasComment("菜单父级ID");

                    b.Property<string>("Path")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("path")
                        .HasComment("菜单地址");

                    b.Property<string>("Redirect")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("redirect")
                        .HasComment("菜单跳转地址");

                    b.Property<bool>("Show")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("show")
                        .HasComment("是否显示菜单");

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("title")
                        .HasComment("菜单标题");

                    b.HasKey("Id");

                    b.ToTable("sys_menu", t =>
                        {
                            t.HasComment("系统菜单表");
                        });
                });

            modelBuilder.Entity("HuanTian.Infrastructure.SysMenuRoleDO", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<long?>("CreateBy")
                        .HasColumnType("bigint")
                        .HasColumnName("create_by")
                        .HasComment("创建人");

                    b.Property<DateTime?>("CreateOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_on")
                        .HasComment("创建时间");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("deleted")
                        .HasComment("软删除");

                    b.Property<long>("MenuId")
                        .HasColumnType("bigint")
                        .HasColumnName("menu_id");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint")
                        .HasColumnName("role_id");

                    b.HasKey("Id");

                    b.ToTable("sys_menu_role", t =>
                        {
                            t.HasComment("系统菜单角色表");
                        });
                });

            modelBuilder.Entity("HuanTian.Infrastructure.SysPermissionsDO", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("code")
                        .HasComment("权限Code");

                    b.Property<long?>("CreateBy")
                        .HasColumnType("bigint")
                        .HasColumnName("create_by")
                        .HasComment("创建人");

                    b.Property<DateTime?>("CreateOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_on")
                        .HasComment("创建时间");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("deleted")
                        .HasComment("软删除");

                    b.Property<long?>("MenuId")
                        .HasColumnType("bigint")
                        .HasColumnName("menu_id")
                        .HasComment("绑定系统菜单Id");

                    b.Property<string>("Name")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("name")
                        .HasComment("权限名字");

                    b.Property<int?>("Type")
                        .HasColumnType("int")
                        .HasColumnName("type")
                        .HasComment("权限类型");

                    b.HasKey("Id");

                    b.ToTable("sys_permissions");
                });

            modelBuilder.Entity("HuanTian.Infrastructure.SysRoleDO", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<long?>("CreateBy")
                        .HasColumnType("bigint")
                        .HasColumnName("create_by")
                        .HasComment("创建人");

                    b.Property<DateTime?>("CreateOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_on")
                        .HasComment("创建时间");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("deleted")
                        .HasComment("软删除");

                    b.Property<string>("Describe")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("describe")
                        .HasComment("角色描述");

                    b.Property<bool>("Enable")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("enable")
                        .HasComment("角色启用");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("role_name")
                        .HasComment("角色名字");

                    b.HasKey("Id");

                    b.ToTable("sys_role", t =>
                        {
                            t.HasComment("系统角色信息表");
                        });
                });

            modelBuilder.Entity("HuanTian.Infrastructure.SysRolePermissionsDO", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<long?>("CreateBy")
                        .HasColumnType("bigint")
                        .HasColumnName("create_by")
                        .HasComment("创建人");

                    b.Property<DateTime?>("CreateOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_on")
                        .HasComment("创建时间");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("deleted")
                        .HasComment("软删除");

                    b.Property<long>("PermissionsId")
                        .HasColumnType("bigint")
                        .HasColumnName("permissions_id");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint")
                        .HasColumnName("role_id");

                    b.HasKey("Id");

                    b.ToTable("sys_role_permissions");
                });

            modelBuilder.Entity("HuanTian.Infrastructure.SysUserDO", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("Avatar")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("avatar")
                        .HasComment("图片路径");

                    b.Property<long?>("CreateBy")
                        .HasColumnType("bigint")
                        .HasColumnName("create_by")
                        .HasComment("创建人");

                    b.Property<DateTime?>("CreateOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_on")
                        .HasComment("创建时间");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("deleted")
                        .HasComment("软删除");

                    b.Property<long>("DeptId")
                        .HasColumnType("bigint")
                        .HasColumnName("dept_id")
                        .HasComment("所属部门Id");

                    b.Property<bool>("Enable")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("enable")
                        .HasComment("启用");

                    b.Property<int>("Language")
                        .HasColumnType("int")
                        .HasColumnName("language")
                        .HasComment("系统语言");

                    b.Property<string>("LastLoginIp")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("last_login_ip")
                        .HasComment("最后登陆IP");

                    b.Property<DateTime?>("LastLoginTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("last_login_time")
                        .HasComment("最后登陆时间");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name")
                        .HasComment("名字");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("password")
                        .HasComment("用户密码");

                    b.Property<string>("Telephone")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("telephone")
                        .HasComment("电话");

                    b.Property<long>("TenantId")
                        .HasColumnType("bigint")
                        .HasColumnName("tenant_id")
                        .HasComment("租户ID");

                    b.Property<int>("Type")
                        .HasColumnType("int")
                        .HasColumnName("type")
                        .HasComment("账号类型");

                    b.Property<long?>("UpdateBy")
                        .HasColumnType("bigint")
                        .HasColumnName("update_by")
                        .HasComment("修改人");

                    b.Property<DateTime?>("UpdateOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("update_on")
                        .HasComment("修改时间");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("user_name")
                        .HasComment("用户名");

                    b.HasKey("Id");

                    b.ToTable("sys_user", t =>
                        {
                            t.HasComment("用户信息表");
                        });
                });

            modelBuilder.Entity("HuanTian.Infrastructure.SysUserRoleDO", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<long?>("CreateBy")
                        .HasColumnType("bigint")
                        .HasColumnName("create_by")
                        .HasComment("创建人");

                    b.Property<DateTime?>("CreateOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_on")
                        .HasComment("创建时间");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("deleted")
                        .HasComment("软删除");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint")
                        .HasColumnName("role_id");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.ToTable("sys_user_role", t =>
                        {
                            t.HasComment("系统用户权限表");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
