using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SqlSugar.Extensions;

namespace HuanTian.WebCore
{
    public static class HostAppLifetimeExtensions
    {
        /// <summary>
        /// 注册主机应用程序生命周期
        /// </summary>
        /// <param name="app"></param>
        public static void RegisterHostApplicationLifetime(this WebApplication app)
        {
            // 静态类存储
            App.WebHostEnvironment = app.Environment;
            // 注册主机应用程序生命周期
            var appLeftTime = app.Services.GetRequiredService<IHostApplicationLifetime>();
            var serviceProvider = app.Services;
            appLeftTime.ApplicationStarted.Register(() => ApplicationStarted(serviceProvider));
        }
        /// <summary>
        /// 项目加载完毕之后执行
        /// </summary>
        private static void ApplicationStarted(IServiceProvider serviceProvider)
        {
            if (!App.Configuration["AppSettings:MiddlewareEnable"].ObjToBool())
                return;

            // 打开消息队列消费者
            EmailMQ.OpenConsumer();
            LogMQ.OpenConsumer();
        }
    }
}

