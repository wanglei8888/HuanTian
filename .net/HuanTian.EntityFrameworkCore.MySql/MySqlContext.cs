using HuanTian.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuanTian.EntityFrameworkCore.MySql
{
    public class MySqlContext : DbContext
    {
        public DbSet<FestivalInfoDO> FestivalInfoDO { get; set; }
        public DbSet<UserInfoDO> UserInfoDO { get; set; }
        #region 
        public MySqlContext()
        {

        }

        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        string connStringMySql = @"Server=localhost;DataBase=CoreMvcTestDB;uid=root;pwd=123456;pooling=true;port=3306;Charset=utf8;sslMode=None;";
        //        //参数除了连接字符串，还要规定版本
        //        //optionsBuilder.UseMySql(connStringMySql, Microsoft.EntityFrameworkCore.ServerVersion.Create(new Version(8, 0, 19), ServerType.MySql));
        //        //一般会这样写：
        //        optionsBuilder.UseMySql(connStringMySql, ServerVersion.AutoDetect(connStringMySql));
        //        //optionsBuilder.UseMySql("Server=yourserver;Port=yourport;Database=yourdatabasename;Uid=youruser;Pwd=yourpwd;", ServerVersion.AutoDetect("Server=yourserver;Port=yourport;Database=yourdatabasename;Uid=youruser;Pwd=yourpwd;"));
        //    }

        //}
        #endregion
    }
}
