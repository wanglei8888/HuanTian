#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 Microsoft NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：MS-SOBOTFLKMEBA
 * 公司名称：Microsoft
 * 命名空间：HuanTian.WebCore
 * 唯一标识：eabc4168-21d1-4e65-961e-cd25f4b9e0cf
 * 文件名：InjectionExtensions
 * 当前用户域：MS-SOBOTFLKMEBA
 * 
 * 创建者：wanglei
 * 电子邮箱：
 * 创建时间：2023/4/2 12:53:30
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
using Hangfire.HttpJob.Agent.Config;
using HuanTian.EntityFrameworkCore;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yitter.IdGenerator;

namespace HuanTian.WebCore
{
    /// <summary>
    /// 依赖注入拓展
    /// </summary>
    public static class InjectionExtensions
	{
        /// <summary>
        /// 静态类存储
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddInject(this IServiceCollection services, IConfiguration configuration)
		{
			InternalApp.Configuration = configuration;
			InternalApp.InternalServices = services;
            // 雪花ID
            YitIdHelper.SetIdGenerator(new IdGeneratorOptions(1));
            // Mapster加载全局设计
            var assemblies = AssemblyHelper.GetAssemblyArray();
            foreach (var assembly in assemblies)
            {
                TypeAdapterConfig.GlobalSettings.Scan(assembly);
            }
            // 注册Hangfire服务 Agent自动注入的项目
            JobAgentServiceConfigurer hangFire = new JobAgentServiceConfigurer(services);
            hangFire.AddJobAgent(AssemblyHelper.GetAssembly("HuanTian.Service"));

            return services;
        }
        /// <summary>
        /// 依赖注入服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDependencyInject(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IStartupFilter, StartupFilter>();
            services.AddScoped(typeof(IRepository<>), typeof(HuanTian.SqlSugar.SqlSugarRepository<>));
            services.AddScoped<IQueryFilter, QueryFilter>();
            // 注册Redis缓存服务
            services.AddSingleton<IRedisCache>(provider =>
                new RedisCache(configuration["ConnectionStrings:Redis"]));
            // 注册RabbitMQ服务
            services.AddSingleton<IMessageQueue, RabbitMQMessageQueue>();
            //services.AddSingleton<IMessageQueue, RabbitMQMessageQueue>(provider =>
            //   new RabbitMQMessageQueue(configuration["ConnectionStrings:RabbitMQ"]));

            return services;
        }
    }
}

