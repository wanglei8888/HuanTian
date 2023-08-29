using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            appLeftTime.ApplicationStarted.Register(new AppLeftTime().ApplicationStarted);
        }
    }
    /// <summary>
    /// 主机应用程序生命周期方法
    /// </summary>
    public class AppLeftTime
    {
        public AppLeftTime()
        {

        }
        /// <summary>
        /// 项目加载完毕之后执行
        /// </summary>
        public void ApplicationStarted()
        {
            EmailMQ.OpenConsumer();
            LogMQ.OpenConsumer();
        }
    }
}
