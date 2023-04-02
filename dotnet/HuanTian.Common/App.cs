﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace HuanTian.Common
{
    public static class App
    {
        /// <summary>
        /// 全局配置选项
        /// </summary>
        public static IConfiguration Configuration => CatchOrDefault(() => InternalApp.Configuration.Reload(), new ConfigurationBuilder().Build());

        /// <summary>
        /// 存储根服务，可能为空
        /// </summary>
        public static IServiceProvider RootServices => InternalApp.RootServices;

        /// <summary>
        /// 获取请求上下文
        /// </summary>
        public static HttpContext HttpContext => CatchOrDefault(() => RootServices?.GetService<IHttpContextAccessor>()?.HttpContext);

        /// <summary>
        /// 未托管的对象集合
        /// </summary>
        public static readonly ConcurrentBag<IDisposable> UnmanagedObjects;


        /// <summary>
        /// 获取请求生存周期的服务
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static TService GetService<TService>(IServiceProvider serviceProvider = default)
            where TService : class
        {
            return GetService(typeof(TService), serviceProvider) as TService;
        }
        /// <summary>
        /// 获取请求生存周期的服务
        /// </summary>
        /// <param name="type"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static object GetService(Type type, IServiceProvider serviceProvider = default)
        {
            return (serviceProvider ?? GetServiceProvider(type)).GetService(type);
        }
        /// <summary>
        /// 解析服务提供器
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public static IServiceProvider GetServiceProvider(Type serviceType)
        {
            //// 处理控制台应用程序
            //if (HostEnvironment == default) return RootServices;

            // 第一选择，判断是否是单例注册且单例服务不为空，如果是直接返回根服务提供器
            if (RootServices != null && InternalApp.InternalServices.Where(u => u.ServiceType == (serviceType.IsGenericType ? serviceType.GetGenericTypeDefinition() : serviceType))
                                                                    .Any(u => u.Lifetime == ServiceLifetime.Singleton)) return RootServices;

            // 第二选择是获取 HttpContext 对象的 RequestServices
            var httpContext = HttpContext;
            if (httpContext?.RequestServices != null) return httpContext.RequestServices;
            // 第三选择，创建新的作用域并返回服务提供器
            else if (RootServices != null)
            {
                var scoped = RootServices.CreateScope();
                // UnmanagedObjects.Add(scoped);
                return scoped.ServiceProvider;
            }
            // 第四选择，构建新的服务对象（性能最差）
            else
            {
                var serviceProvider = InternalApp.InternalServices.BuildServiceProvider();
                // UnmanagedObjects.Add(serviceProvider);
                return serviceProvider;
            }

        }
        /// <summary>
        /// 释放所有未托管的对象
        /// </summary>
        public static void DisposeUnmanagedObjects()
        {
            //foreach (var dsp in UnmanagedObjects)
            //{
            //    try
            //    {
            //        dsp?.Dispose();
            //    }
            //    finally { }
            //}

            //// 强制手动回收 GC 内存
            //if (UnmanagedObjects.Any())
            //{         
            //   GC.Collect();     
            //}

            //UnmanagedObjects.Clear();
        }
        public static TOptions GetConfig<TOptions>(string path, bool loadPostConfigure = false)
        {
            var options = Appsettings.Configuration.GetSection(path).Get<TOptions>();

            // 加载默认选项配置
            if (loadPostConfigure)
            {
                var postConfigure = typeof(TOptions).GetMethod("PostConfigure");
                if (postConfigure != null)
                {
                    options ??= Activator.CreateInstance<TOptions>();
                    postConfigure.Invoke(options, new object[] { options, Appsettings.Configuration });
                }
            }

            return options;
        }
        /// <summary>
        /// 处理获取对象异常问题
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="action">获取对象委托</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>T</returns>
        private static T CatchOrDefault<T>(Func<T> action, T defaultValue = null)
            where T : class
        {
            try
            {
                return action();
            }
            catch
            {
                return defaultValue ?? null;
            }
        }
    }


    public static class InternalApp
    {
        /// <summary>
        /// 应用服务
        /// </summary>
        public static IServiceCollection InternalServices;
        /// <summary>
        /// 根服务
        /// </summary>
        public static IServiceProvider RootServices;
        /// <summary>
        /// 配置对象
        /// </summary>
        public static IConfiguration Configuration;
    }
}
