

using HuanTian.Domain;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HuanTian.EntityFrameworkCore.SqlServer
{
    public class Context : DbContext
    {
        public Context()
        {

        }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<FestivalInfoDO> Book { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //这里是连接数据库，其中如果database指定的数据库不存在，则会创建
            string connString = @"server=.;database=dbtest;uid=sa;pwd=123;Pooling=true;Encrypt=True;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connString);
        }
    }
}