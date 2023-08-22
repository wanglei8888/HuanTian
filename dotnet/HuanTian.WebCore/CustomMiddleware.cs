#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 Microsoft NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：MS-SOBOTFLKMEBA
 * 公司名称：Microsoft
 * 命名空间：HuanTian.WebCore
 * 唯一标识：eabc4168-21d1-4e65-961e-cd25f4b9e0cf
 * 文件名：CustomMiddleware
 * 当前用户域：MS-SOBOTFLKMEBA
 * 
 * 创建者：wanglei
 * 电子邮箱：
 * 创建时间：2023/3/25 10:51:30
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

using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace HuanTian.WebCore
{
    /// <summary>
    /// 自定义中间件，功能暂未实现
    /// </summary>
    public class CustomMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<CustomMiddleware> _logger;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="requestDelegate">请求委托</param>
        public CustomMiddleware(RequestDelegate requestDelegate, ILogger<CustomMiddleware> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        /// <summary>
        /// 回调
        /// </summary>
        /// <param name="context">Http内容</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context, IServiceProvider serviceProvider)
        {
            // 记录接口时间
            var watch = new Stopwatch();
            watch.Start();
            await _requestDelegate(context);
            watch.Stop();
            await ApiLogging(context, watch.ElapsedMilliseconds);
        }
        private async Task ApiLogging(HttpContext context,long time)
        {
            var logLevel = App.Configuration["AppSettings:ApiLogLevel"];
            var path = context.Request.Path;
            var message = "";
            switch (logLevel)
            {
                case "1":
                    var paramas = context.Request.QueryString.ToString();
                    // 先读取QueryString 再读取Body
                    if (string.IsNullOrEmpty(paramas))
                    {
                        using (var reader = new StreamReader(context.Request.Body))
                        {
                            paramas = await reader.ReadToEndAsync();
                        }
                    }
                    paramas = paramas.Substring(1);
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
