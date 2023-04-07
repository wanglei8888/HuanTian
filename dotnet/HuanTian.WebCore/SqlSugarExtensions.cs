#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 Microsoft NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：MS-SOBOTFLKMEBA
 * 公司名称：Microsoft
 * 命名空间：HuanTian.WebCore
 * 唯一标识：eabc4168-21d1-4e65-961e-cd25f4b9e0cf
 * 文件名：SqlSugarExtensions
 * 当前用户域：MS-SOBOTFLKMEBA
 * 
 * 创建者：wanglei
 * 电子邮箱：
 * 创建时间：2023/4/2 11:59:30
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
using HuanTian.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

namespace HuanTian.WebCore
{
    public static class SqlSugarExtensions
    {
        /// <summary>
        /// SqlSugar注入拓展
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSqlSugarService(this IServiceCollection services, DbType dbType)
        {
            var config = new ConnectionConfig()
            {
                DbType = dbType, //数据库切换，需要改
                ConnectionString = App.Configuration["ConnectionStrings:MySqlConnection"],
                IsAutoCloseConnection = true,
                ConfigureExternalServices = new ConfigureExternalServices()
                {
                    EntityNameService = (type, entity) =>
                    {
                        //全局设置表名
                        if (App.Configuration["SqlSettings:GlobalSettingsTableName"] == "True")
                        {
                            if (entity.DbTableName.EndsWith("DO"))
                            {
                                // 替换DO后缀 并将属性名转换成小写，并将驼峰命名方式转换为下划线命名方式
                                entity.DbTableName = entity.DbTableName.Replace("DO", "").ToLowerHump();
                            }
                        }
                    },
                    EntityService = (property, column) => 
                    {
                        //全局设置列名
                        if (App.Configuration["SqlSettings:GlobalSettingsColumnName"] == "True")
                        {
                            // 将属性名转换成小写，并将驼峰命名方式转换为下划线命名方式
                            column.DbColumnName = column.DbColumnName.ToLowerHump();
                        }
                    }
                }
            };
            services.AddScoped<ISqlSugarClient>(s =>
            {
                //Scoped用SqlSugarClient 
               SqlSugarClient sqlSugar = new SqlSugarClient(config,
               db =>
               {
                   //单例参数配置，所有上下文生效
                   db.Aop.OnLogExecuting = (sql, pars) =>
                   {
                       //获取IOC对象不要求在一个上下文
                       //vra log=s.GetService<Log>()

                       //获取IOC对象要求在一个上下文
                       //var appServive = s.GetService<IHttpContextAccessor>();
                       //var log= appServive?.HttpContext?.RequestServices.GetService<Log>();
                   };
               });
                return sqlSugar;
            });
            return services;
        }
    }
}
