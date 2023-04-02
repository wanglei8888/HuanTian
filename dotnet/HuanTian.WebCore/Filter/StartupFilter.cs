using HuanTian.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuanTian.WebCore
{
    /// <summary>
    /// 中间件
    /// </summary>
    public class StartupFilter : IStartupFilter
    {
        /// <summary>
        /// 配置中间件
        /// </summary>
        /// <param name="next"></param>
        /// <returns></returns>
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                // 存储根服务
                InternalApp.RootServices = app.ApplicationServices;
                
                app.Use(async (context, next) =>
                {

                    // 执行下一个中间件
                    await next.Invoke();

                    // 释放所有未托管的服务提供器
                    App.DisposeUnmanagedObjects();
                
                });

                // 调用启动层的 Startup
                next(app);
            };
        }
    }
}
