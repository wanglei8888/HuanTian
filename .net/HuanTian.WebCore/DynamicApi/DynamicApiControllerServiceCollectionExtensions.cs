using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace HuanTian.WebCore
{
    public static class DynamicApiControllerServiceCollectionExtensions
    {
        /// <summary>
        /// 添加动态接口控制器服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDynamicApiControllers(this IServiceCollection services)
        {
            var partManager = services.FirstOrDefault(s => s.ServiceType == typeof(ApplicationPartManager))?.ImplementationInstance as ApplicationPartManager
                ?? throw new InvalidOperationException($"`{nameof(AddDynamicApiControllers)}` must be invoked after `{nameof(MvcServiceCollectionExtensions.AddControllers)}`.");

            // 解决项目类型为 <Project Sdk="Microsoft.NET.Sdk"> 不能加载 API 问题，默认支持 <Project Sdk="Microsoft.NET.Sdk.Web">
            //foreach (var assembly in App.Assemblies)
            //{
            //    if (partManager.ApplicationParts.Any(u => u.Name != assembly.GetName().Name))
            //    {
            //        partManager.ApplicationParts.Add(new AssemblyPart(assembly));
            //    }
            //}
            // 载入模块化/插件程序集部件
            //if (App.ExternalAssemblies.Any())
            //{
                //foreach (var assembly in App.ExternalAssemblies)
                //{
                //    if (partManager.ApplicationParts.Any(u => u.Name != assembly.GetName().Name))
                //    {
                //        partManager.ApplicationParts.Add(new AssemblyPart(assembly));
                //    }
                //}
            //}

            // 添加控制器特性提供器
            partManager.FeatureProviders.Add(new DynamicApiControllerFeatureProvider());

            // 添加配置
            services.AddConfigurableOptions<DynamicApiControllerSettingsOptions>();

            // 配置 Mvc 选项
            services.Configure<MvcOptions>(options =>
            {
                // 添加应用模型转换器
                options.Conventions.Add(new DynamicApiControllerApplicationModelConvention(services));
            });

            return services;
        }

    }
}
