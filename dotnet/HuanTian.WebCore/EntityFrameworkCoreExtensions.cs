﻿#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 Microsoft NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：MS-SOBOTFLKMEBA
 * 公司名称：Microsoft
 * 命名空间：HuanTian.WebCore
 * 唯一标识：eabc4168-21d1-4e65-961e-cd25f4b9e0cf
 * 文件名：EntityFrameworkCoreExtensions
 * 当前用户域：MS-SOBOTFLKMEBA
 * 
 * 创建者：wanglei
 * 电子邮箱：
 * 创建时间：2023/4/2 14:51:30
 * 版本：V1.0.0
 * 描述：
 *
 * ----------------------------------------------------------------
 * 修改人：
 * 时间：
 * 修改说明：
 *
 * 版本：V1.0.1
 *----------------------------------------------------------------*/
#endregion << 版 本 注 释 >>
using HuanTian.EntityFrameworkCore;
using HuanTian.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

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
