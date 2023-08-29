#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 HP NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：WEIHAN
 * 公司名称：HP
 * 命名空间：HuanTian.Service.Common
 * 唯一标识：acf71771-7d1f-45fc-b2ea-7f1101702b55
 * 文件名：LogMQ
 * 当前用户域：weihan
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/8/24 14:48:06
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

using System.Text;
using System.Text.Json;

namespace HuanTian.Service
{
    /// <summary>
    /// 日志消息队列
    /// </summary>
    public class LogMQ
    {
        // private static readonly SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 5);

        /// <summary>
        /// 发送消息到队列
        /// </summary>
        /// <param name="msg"></param>
        public static void SendMQ(string msg)
        {
            // 送邮件将信息存到消息队列中
            var emailQueue = App.GetService<IMessageQueue>();
            emailQueue.SelectQueue(MsgQConst.Log)
                .Enqueue(msg);
        }
        /// <summary>
        /// 打开消费端
        /// </summary>
        public static void OpenConsumer()
        {
            var logPath = App.Configuration["AppSettings:LogPath"];
            var messageQueue = App.GetService<IMessageQueue>().SelectQueue(MsgQConst.Log);
            switch (logPath)
            {
                case "Sql":
                    messageQueue.StartConsumingAsync(async (message) =>
                    {
                        // 接受消息
                        var logInfo = JsonSerializer.Deserialize<LogMQDto>(message);

                        // 条件过滤
                        if (logInfo == null)
                            throw new ArgumentException("Log消息参数为空,无法记录");
                        if (!IsEnabled(logInfo.Level))
                            return true;

                        // 处理消息
                        await LogToSql(logInfo);
                        return true;
                    });
                    break;
                default:
                    // 默认储存到文件
                    messageQueue.StartConsuming((message) =>
                    {
                        // 接受消息
                        var logInfo = JsonSerializer.Deserialize<LogMQDto>(message);

                        // 条件过滤
                        if (logInfo == null)
                            throw new ArgumentException("Log消息参数为空,无法记录");
                        if (!IsEnabled(logInfo.Level))
                            return true;

                        // 处理消息
                        LogToFile(logInfo);
                        return true;
                    });
                    break;
            }

        }
        private static void LogToFile(LogMQDto logInfo)
        {
            var basePath = Directory.GetCurrentDirectory().Replace("\\", "/") + "/Logs/";

            var logPath = basePath + "/Info/" + logInfo.CreateOn.ToString("yyyyMMdd") + ".log";
            //区分报错日志
            var logContent = $"【时间】{logInfo.CreateOn.ToString("HH-mm-ss-fff")} 【等级】{logInfo.Level} 【内容】{logInfo.Msg}";
            if (logInfo.Exception != null)
            {
                logContent = $"【报错信息 {logInfo.Msg}】\r\n【报错内容 {logInfo.Exception?.Message}】\r\n【报错详情 {logInfo.Exception?.StackTrace}】";
            }
            if (logInfo.Level == LogLevel.Error || logInfo.Level == LogLevel.Debug)
            {
                logPath = basePath + "/Error/" + logInfo.CreateOn.ToString("yyyyMMdd") + ".log";
            }

            using (StreamWriter writer = new StreamWriter(logPath, true, Encoding.UTF8))
            {
                writer.WriteLineAsync(logContent);
            }
        }

        private static async Task LogToSql(LogMQDto logInfo)
        {

            logInfo.Msg = $"【时间】{logInfo.CreateOn.ToString("HH-mm-ss-fff")} 【等级】{logInfo.Level} 【内容】{logInfo.Msg}";
            switch (logInfo.Level)
            {
                case LogLevel.Warning:
                case LogLevel.Error:
                    if (logInfo.Exception != null)
                    {
                        logInfo.Msg = $"【报错信息 {logInfo.Msg}】\r\n【报错内容 {logInfo.Exception?.Message}】\r\n【报错详情 {logInfo.Exception?.StackTrace}】";
                    }
                    var errorLog = logInfo.Adapt<SysLogErrorDO>();
                    var errorService = App.GetService<IRepository<SysLogErrorDO>>();
                    await errorService.InitTable(errorLog).AddAsync();
                    break;
                default:
                    var log = logInfo.Adapt<SysLogInfoDO>();
                    var logService = App.GetService<IRepository<SysLogInfoDO>>();
                    await logService.InitTable(log).AddAsync();
                    break;
            }

        }
        private static bool IsEnabled(LogLevel logLevel)
        {
            if (logLevel >= LogLevel.Information)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
