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
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("HuanTian.Entities.SysMenuDO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_time")
                        .HasComment("创建时间");

                    b.Property<string>("Creater")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("creater")
                        .HasComment("创建人");

                    b.Property<string>("Icon")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("icon")
                        .HasComment("图标");

                    b.Property<bool?>("KeepAlive")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("keep_alive")
                        .HasComment("是否缓存");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name")
                        .HasComment("菜单名字");

                    b.Property<int>("ParentId")
                        .HasColumnType("int")
                        .HasColumnName("parent_id")
                        .HasComment("菜单父级ID");

                    b.Property<string>("Path")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("path")
                        .HasComment("菜单地址");

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("title")
                        .HasComment("菜单标题");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("update_time")
                        .HasComment("修改时间");

                    b.Property<string>("Updater")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("updater")
                        .HasComment("修改人");

                    b.HasKey("Id");

                    b.ToTable("sys_menu");

                    b.HasComment("系统菜单表");
                });

            modelBuilder.Entity("HuanTian.Entities.SysMenuRoleDO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_time")
                        .HasComment("创建时间");

                    b.Property<string>("Creater")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("creater")
                        .HasComment("创建人");

                    b.Property<int>("MenuId")
                        .HasColumnType("int")
                        .HasColumnName("menu_id");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("update_time")
                        .HasComment("修改时间");

                    b.Property<string>("Updater")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("updater")
                        .HasComment("修改人");

                    b.HasKey("Id");

                    b.ToTable("sys_menu_role");

                    b.HasComment("系统菜单权限表");
                });

            modelBuilder.Entity("HuanTian.Entities.SysRoleInfoDO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_time")
                        .HasComment("创建时间");

                    b.Property<string>("Creater")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("creater")
                        .HasComment("创建人");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("deleted")
                        .HasComment("是否删除");

                    b.Property<string>("Describe")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("describe")
                        .HasComment("角色描述");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("role_name")
                        .HasComment("角色名字");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status")
                        .HasComment("角色状态");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("update_time")
                        .HasComment("修改时间");

                    b.Property<string>("Updater")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("updater")
                        .HasComment("修改人");

                    b.HasKey("Id");

                    b.ToTable("sys_role_info");

                    b.HasComment("系统角色信息表");
                });

            modelBuilder.Entity("HuanTian.Entities.SysUserInfoDO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("avatar")
                        .HasComment("图片路径");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_time")
                        .HasComment("创建时间");

                    b.Property<string>("CreatorId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("creator_id")
                        .HasComment("创建人");

                    b.Property<int>("Deleted")
                        .HasColumnType("int")
                        .HasColumnName("deleted")
                        .HasComment("是否删除");

                    b.Property<int>("Language")
                        .HasColumnType("int")
                        .HasColumnName("language")
                        .HasComment("系统语言");

                    b.Property<string>("LastLoginIp")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("last_login_ip")
                        .HasComment("最后登陆IP");

                    b.Property<DateTime>("LastLoginTime")
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

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("role_id")
                        .HasComment("权限ID");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status")
                        .HasComment("状态");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("telephone")
                        .HasComment("电话");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("user_name")
                        .HasComment("用户名");

                    b.HasKey("Id");

                    b.ToTable("sys_user_info");

                    b.HasComment("用户信息表");
                });

            modelBuilder.Entity("HuanTian.Entities.SysUserRoleDO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_time")
                        .HasComment("创建时间");

                    b.Property<string>("Creater")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("creater")
                        .HasComment("创建人");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("update_time")
                        .HasComment("修改时间");

                    b.Property<string>("Updater")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("updater")
                        .HasComment("修改人");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.ToTable("sys_user_role");

                    b.HasComment("系统用户权限表");
                });
#pragma warning restore 612, 618
        }
    }
}
