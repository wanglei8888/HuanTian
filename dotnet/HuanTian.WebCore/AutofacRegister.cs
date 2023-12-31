﻿#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 Microsoft NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：MS-SOBOTFLKMEBA
 * 公司名称：Microsoft
 * 命名空间：HuanTian.WebCore
 * 唯一标识：eabc4168-21d1-4e65-961e-cd25f4b9e0cf
 * 文件名：AutofacRegister
 * 当前用户域：MS-SOBOTFLKMEBA
 * 
 * 创建者：wanglei
 * 电子邮箱：
 * 创建时间：2023/2/26 18:51:41
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
using Autofac;
using Autofac.Core.Lifetime;
using Autofac.Core;
using HuanTian.EntityFrameworkCore;
using HuanTian.SqlSugar;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace HuanTian.WebCore
{
    /// <summary>
    /// Autofac 拓展
    /// </summary>
    public class AutofacRegister : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // 加载依赖注入
            RegisterTypes(builder);

            // 需要暴露接口所在的程序集
            var basePath = AppContext.BaseDirectory;

            // 加载目标程序集
            var Services = AssemblyHelper.GetAssemblyArray();
            var test1 = Services.Where(t => typeof(ITransient) == t.GetType());
            // 扫描继承 ISingleton 接口的所有类 注入为单例
            builder.RegisterAssemblyTypes(Services)
                       .Where(t => typeof(ISingleton).IsAssignableFrom(t))
                       .AsImplementedInterfaces()
                       .SingleInstance();
            // 扫描继承 IScoped 接口的所有类 注入为作用域
            builder.RegisterAssemblyTypes(Services)
                       .Where(t => typeof(IScoped).IsAssignableFrom(t))
                       .AsImplementedInterfaces()
                       .InstancePerLifetimeScope();
            // 扫描继承 ITransient 接口的所有类 注入为瞬时
            builder.RegisterAssemblyTypes(Services)
                       .Where(t => typeof(ITransient).IsAssignableFrom(t))
                       .AsImplementedInterfaces()
                       .InstancePerDependency();
            //var container = builder.Build();
            //var registeredServices = container.ComponentRegistry.Registrations;
        }
        private void RegisterTypes(Autofac.ContainerBuilder builder)
        {

            // 注册生命周期为单例   Singleton 的类 builder.RegisterType(type).AsImplementedInterfaces().SingleInstance();
            // 注册生命周期为作用域 Scoped    的类 builder.RegisterType(type).AsImplementedInterfaces().InstancePerLifetimeScope();
            // 注册生命周期为瞬时   Transient 的类 builder.RegisterType(type).AsImplementedInterfaces().InstancePerDependency();
            //builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            //builder.RegisterType<StartupFilter>().As<IStartupFilter>().InstancePerDependency();
            //builder.RegisterType<SysUserService>().As<ISysUserService>().SingleInstance();
            //builder.RegisterType<RabbitMQMessageQueue>().As<IMessageQueue>()
            //    .WithParameter("connectionString", App.Configuration["ConnectionStrings:RabbitMQ"]).SingleInstance();
            //builder.RegisterType<RedisCache>().As<IRedisCache>()
            //    .WithParameter("connectionString", App.Configuration["ConnectionStrings:Redis"]).SingleInstance();

        }

    }
    /// <summary>
    /// 手写依赖注入
    /// </summary>
    public static class AutoInjectionExtensions
    {
        /// <summary>
        /// 动态依赖注入
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAutoInjection(this IServiceCollection services)
        {
            // 所有尾名Service的类型
            var typesToRegister = AssemblyHelper.GetAssemblyAllTypeList()
                .Where(t => t.Name.EndsWith("Service"));

            foreach (var service in typesToRegister)
            {
                // 继承接口的类型
                var intefaceList = service.GetInterfaces();
                if (!service.IsClass)
                    continue;

                // 获取注入的类型
                var serviceLifetime = TryGetServiceLifetime(intefaceList);
                // 依赖注入的接口
                var iService = intefaceList.FirstOrDefault(t => t.Name == $"I{service.Name}");
                if (iService != null)
                {
                    // 动态依赖注入接口
                    services.Add(new ServiceDescriptor(iService, service, serviceLifetime));
                }
            }

            return services;
        }

        /// <summary>
        /// 根据依赖接口类型解析 ServiceLifetime 对象
        /// </summary>
        /// <param name="dependencyType"></param>
        /// <returns></returns>
        private static ServiceLifetime TryGetServiceLifetime(Type[] dependencyType)
        {
            // 取出三个依赖注入的第一个类型
            Type firstMatchingType = null;
            foreach (Type type in dependencyType)
            {
                if (type == typeof(ITransient) || type == typeof(IScoped) || type == typeof(ISingleton))
                {
                    firstMatchingType = type;
                    break;
                }
            }
            // 根据类型判断依赖注入类型
            if (firstMatchingType == typeof(ITransient))
            {
                return ServiceLifetime.Transient;
            }
            else if (firstMatchingType == typeof(IScoped))
            {
                return ServiceLifetime.Scoped;
            }
            else if (firstMatchingType == typeof(ISingleton))
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