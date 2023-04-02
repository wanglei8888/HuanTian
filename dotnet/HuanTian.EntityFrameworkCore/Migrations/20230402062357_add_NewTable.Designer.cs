﻿// <auto-generated />
using System;
using HuanTian.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HuanTian.EntityFrameworkCore.Migrations
{
    [DbContext(typeof(EfSqlContext))]
    [Migration("20230402062357_add_NewTable")]
    partial class add_NewTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("HuanTian.Domain.SysMenuDO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_time");

                    b.Property<string>("Creater")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("creater");

                    b.Property<string>("Icon")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("icon");

                    b.Property<bool?>("KeepAlive")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("keep_alive");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name");

                    b.Property<int>("ParentId")
                        .HasColumnType("int")
                        .HasColumnName("parent_id");

                    b.Property<string>("Path")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("path");

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("title");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("update_time");

                    b.Property<string>("Updater")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("updater");

                    b.HasKey("Id");

                    b.ToTable("sys_menu");
                });

            modelBuilder.Entity("HuanTian.Domain.SysMenuRoleDO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_time");

                    b.Property<string>("Creater")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("creater");

                    b.Property<int>("MenuId")
                        .HasColumnType("int")
                        .HasColumnName("menu_id");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("update_time");

                    b.Property<string>("Updater")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("updater");

                    b.HasKey("Id");

                    b.ToTable("sys_menu_role");
                });

            modelBuilder.Entity("HuanTian.Domain.SysRoleInfoDO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_time");

                    b.Property<string>("Creater")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("creater");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("role_name");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("update_time");

                    b.Property<string>("Updater")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("updater");

                    b.HasKey("Id");

                    b.ToTable("sys_role_info");
                });

            modelBuilder.Entity("HuanTian.Domain.SysUserInfoDO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("avatar");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_time");

                    b.Property<string>("CreatorId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("creator_id");

                    b.Property<int>("Deleted")
                        .HasColumnType("int")
                        .HasColumnName("deleted");

                    b.Property<int>("Langguage")
                        .HasColumnType("int")
                        .HasColumnName("langguage");

                    b.Property<string>("LastLoginIp")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("last_login_ip");

                    b.Property<DateTime>("LastLoginTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("last_login_time");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("password");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("role_id");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("telephone");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("user_name");

                    b.HasKey("Id");

                    b.ToTable("sys_user_info");
                });

            modelBuilder.Entity("HuanTian.Domain.SysUserRoleDO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_time");

                    b.Property<string>("Creater")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("creater");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("update_time");

                    b.Property<string>("Updater")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("updater");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.ToTable("sys_user_role");
                });
#pragma warning restore 612, 618
        }
    }
}