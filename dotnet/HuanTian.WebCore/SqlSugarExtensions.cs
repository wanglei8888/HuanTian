using HuanTian.Common;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;

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
                ConnectionString = Appsettings.Configuration["ConnectionStrings:MySqlConnection"],
                IsAutoCloseConnection = true,
                ConfigureExternalServices = new ConfigureExternalServices()
                {
                    EntityNameService = (type, entity) =>
                    {
                        //全局设置表名
                        if (Appsettings.Configuration["SqlSettings:GlobalSettingsTableName"] == "True")
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
                        if (Appsettings.Configuration["SqlSettings:GlobalSettingsColumnName"] == "True")
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
