using HuanTian.Common;
using HuanTian.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuanTian.WebCore
{
    public static class EntityFrameworkCoreExtensions
    {
        /// <summary>
        /// EntityFramework注入拓展
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEntityFrameworkService(this IServiceCollection services, DbType dbType)
        {
            var ConnectionStrings = Appsettings.Configuration["ConnectionStrings:MySqlConnection"];
            switch (dbType)
            {
                case DbType.MySql:
                    services.AddDbContext<EfSqlContext>(options => options.UseMySql(ConnectionStrings,
                        ServerVersion.AutoDetect(ConnectionStrings)));
                    break;
                case DbType.SqlServer:
                    services.AddDbContext<EfSqlContext>(options => options.UseSqlServer(ConnectionStrings));
                    break;
            }
            return services;
        }
    }
}
