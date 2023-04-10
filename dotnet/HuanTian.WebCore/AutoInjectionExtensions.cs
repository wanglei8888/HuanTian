#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-4P8G8RH
 * 公司名称：
 * 命名空间：HuanTian.WebCore
 * 唯一标识：92c091f9-638d-49a1-9e34-f022c48fd16a
 * 文件名：Class1
 * 当前用户域：DESKTOP-4P8G8RH
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/4/8 14:10:26
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
using System.Reflection;

namespace HuanTian.WebCore
{
    public static class AutoInjectionExtensions
    {
        /// <summary>
        /// 动态依赖注入
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAutoInjection(this IServiceCollection services)
        {
            // 需要暴露接口所在的程序集
            var loadServices = AssemblyHelper.GetAssembly("HuanTian.Service");

            // 所有尾名Service的类型
            var typesToRegister = loadServices.GetTypes()
                .Where(t => t.Name.EndsWith("Service" ));

            foreach (var service in typesToRegister)
            {
                // 继承接口的类型
                var intefaceList = service.GetInterfaces();
                if (service.IsClass)
                {
                    var interfacesType = intefaceList.LastOrDefault();
                    // 获取注入的类型
                    var serviceLifetime = TryGetServiceLifetime(interfacesType);
                    // 依赖注入的接口
                    var iService = intefaceList.FirstOrDefault(t => t.Name == $"I{service.Name}");
                    if (iService != null)
                    {
                        // 动态依赖注入接口
                        services.Add(new ServiceDescriptor(iService, service, serviceLifetime));
                    }
                }
            }

            return services;
        }

        /// <summary>
        /// 根据依赖接口类型解析 ServiceLifetime 对象
        /// </summary>
        /// <param name="dependencyType"></param>
        /// <returns></returns>
        private static ServiceLifetime TryGetServiceLifetime(Type dependencyType)
        {
            if (dependencyType == typeof(ITransient))
            {
                return ServiceLifetime.Transient;
            }
            else if (dependencyType == typeof(IScoped))
            {
                return ServiceLifetime.Scoped;
            }
            else if (dependencyType == typeof(ISingleton))
            {
                return ServiceLifetime.Singleton;
            }
            else
            {
                // 默认依赖注入作用域
                return ServiceLifetime.Scoped;
            }
        }
    }
}
