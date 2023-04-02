using HuanTian.Common;
using HuanTian.Interface;
using HuanTian.SqlSugar;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuanTian.WebCore
{
    public static class InjectionExtensions
    {
        /// <summary>
        /// 依赖注入集合
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddInject(this IServiceCollection services, IConfiguration configuration)
        {
            InternalApp.Configuration = configuration;
            InternalApp.InternalServices = services;
            services.AddSingleton(typeof(IRepository<>), typeof(SqlSugarRepository<>));
            services.AddSingleton(new Appsettings(configuration));
            services.AddSingleton<IStartupFilter, StartupFilter>();
            
            return services;
        }
    }
}
