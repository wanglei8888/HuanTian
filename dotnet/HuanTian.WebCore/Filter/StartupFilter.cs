#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 Microsoft NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：MS-SOBOTFLKMEBA
 * 公司名称：Microsoft
 * 命名空间：HuanTian.WebCore
 * 唯一标识：eabc4168-21d1-4e65-961e-cd25f4b9e0cf
 * 文件名：TemplateResultFilter
 * 当前用户域：MS-SOBOTFLKMEBA
 * 
 * 创建者：wanglei
 * 电子邮箱：
 * 创建时间：2023/4/2 19:51:58
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
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace HuanTian.WebCore
{
    /// <summary>
    /// 中间件
    /// </summary>
    public class StartupFilter : IStartupFilter
    {
        private readonly ILogger<StartupFilter> _logger;
        public StartupFilter(ILogger<StartupFilter> logger)
        {
            _logger = logger;
        }
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
                    // 记录接口时间
                    var watch = new Stopwatch();
                    watch.Start();
                    // 执行下一个中间件
                    await next.Invoke();
                    watch.Stop();
                    // 记录接口日志
                    await ApiLogging(context, watch.ElapsedMilliseconds);
                    // 释放所有未托管的服务提供器
                    App.DisposeUnmanagedObjects();
                
                });

                // 调用启动层的 Startup
                next(app);
            };
        }
        private async Task ApiLogging(HttpContext context, long time)
        {
            var logLevel = App.Configuration["AppSettings:ApiLogLevel"];
            // 0 不加载接口日志
            if (string.IsNullOrEmpty(logLevel) || logLevel == "0")
                return;

            var path = context.Request.Path + "/" + context.Request.Method;
            var message = "";
            switch (logLevel)
            {
                case "2":
                    var paramas = context.Request.QueryString.ToString();
                    // 先读取QueryString 再读取Body
                    if (string.IsNullOrEmpty(paramas))
                    {
                        using (var reader = new StreamReader(context.Request.Body))
                        {
                            paramas = await reader.ReadToEndAsync();
                        }
                    }
                    paramas = paramas.Length > 0 ? paramas.Substring(1) : paramas;
                    message = $"接口:{path}       用户:{App.GetUserId()}       时间:{time}ms       参数:{paramas}";
                    break;
                default:
                    message = $"接口:{path}       用户:{App.GetUserId()}       时间:{time}ms";
                    break;
            }
            _logger.LogInformation(message);
        }
    }
}
