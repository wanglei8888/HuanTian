#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 Microsoft NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：MS-SOBOTFLKMEBA
 * 公司名称：Microsoft
 * 命名空间：HuanTian.WebCore.Logger
 * 唯一标识：475f7cb3-c202-4006-b988-f4f835e6202f
 * 文件名：LocalFileLogger
 * 当前用户域：MS-SOBOTFLKMEBA
 * 
 * 创建者：wanglei
 * 电子邮箱：
 * 创建时间：2023/3/13 11:57:17
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuanTian.WebCore
{
    public class LocalFileLogger : ILogger
    {
        private readonly string categoryName;
        private readonly string basePath;

        public LocalFileLogger(string categoryName)
        {
            this.categoryName = categoryName;

            basePath = Directory.GetCurrentDirectory().Replace("\\", "/") + "/Logs/";

            if (Directory.Exists(basePath) == false)
            {
                Directory.CreateDirectory(basePath + "/Info/");
                Directory.CreateDirectory(basePath + "/Error/");
            }
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return default!;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            if (logLevel != LogLevel.None)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (IsEnabled(logLevel))
            {
                if (state != null && state.ToString() != null)
                {
                    var logContent = state.ToString();

                    if (logContent != null)
                    {
                        if (exception != null)
                        {
                            var logMsg = new
                            {
                                message = logContent,
                                error = new
                                {
                                    exception?.Source,
                                    exception?.Message,
                                    exception?.StackTrace
                                }
                            };

                            //logContent = "123";//JsonHelper.ObjectToJson(logMsg);
                        }

                        var log = new
                        {
                            CreateTime = DateTime.UtcNow,
                            Category = categoryName,
                            Level = logLevel,
                            Content = $"[{DateTime.Now.ToString("HH-mm-ss-fff")}] {logLevel} {logContent}"
                        };


                        //区分报错日志
                        var logPath = basePath + "/Info/" + DateTime.UtcNow.ToString("yyyyMMdd") + ".log";
                        if (log.Level == LogLevel.Error || log.Level == LogLevel.Debug)
                        {
                            logPath = basePath + "/Error/" + DateTime.UtcNow.ToString("yyyyMMdd") + ".log";
                        }


                        File.AppendAllText(logPath, log.Content + Environment.NewLine, Encoding.UTF8);

                    }
                }
            }
        }
    }
}