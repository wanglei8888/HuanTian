

using HuanTian.Domain;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HuanTian.EntityFrameworkCore.SqlServer
{
    public class SqlServerContext : DbContext
    {
        public DbSet<FestivalInfoDO> Book { get; set; }
        public SqlServerContext()
        {

        }

        public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options)
        {

        }



        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //这里是连接数据库，其中如果database指定的数据库不存在，则会创建
        //    string connString = @"server=47.57.112.243;database=MikeDuoTest;uid=sa;pwd=Areclong168,;Pooling=true;Encrypt=True;TrustServerCertificate=True;";
        //    optionsBuilder.UseSqlServer(connString);
        //}
    }
}