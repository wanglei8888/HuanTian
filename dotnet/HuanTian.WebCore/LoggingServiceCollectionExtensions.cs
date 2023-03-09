//#region << 版 本 注 释 >>
///*----------------------------------------------------------------
// * 版权所有 (c) 2023 Microsoft NJRN 保留所有权利。
// * CLR版本：4.0.30319.42000
// * 机器名称：MS-SOBOTFLKMEBA
// * 公司名称：Microsoft
// * 命名空间：HuanTian.WebCore
// * 唯一标识：aba58961-37db-4dc8-bfb9-4d22d5fc6560
// * 文件名：LoggingServiceCollectionExtensions
// * 当前用户域：MS-SOBOTFLKMEBA
// * 
// * 创建者：wanglei
// * 电子邮箱：
// * 创建时间：2023/3/9 10:54:56
// * 版本：V1.0.0
// * 描述：
// *
// * ----------------------------------------------------------------
// * 修改人：
// * 时间：
// * 修改说明：
// *
// * 版本：V1.0.1
// *----------------------------------------------------------------*/
//#endregion << 版 本 注 释 >>
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace HuanTian.WebCore;

//public static class LoggingServiceCollectionExtensions
//{

//    public static void AddLocalFileLogger(this ILoggingBuilder builder, Action<LoggerSetting> action)
//    {
//        builder.Services.Configure(action);
//        builder.Services.AddSingleton<ILoggerProvider, LocalFileLoggerProvider>();
//        builder.Services.AddSingleton<IHostedService, LogClearTask>();
//    }
//}
//public sealed class FileLoggerProvider : ILoggerProvider, ISupportExternalScope
//{
//    /// <summary>
//    /// 存储多日志分类日志记录器
//    /// </summary>
//    private readonly ConcurrentDictionary<string, FileLogger> _fileLoggers = new();
//    public ILogger CreateLogger(string categoryName)
//    {
//        return _fileLoggers.GetOrAdd(categoryName, name => new FileLogger(name, this));
//    }

//    public void Dispose()
//    {
//        throw new NotImplementedException();
//    }

//    public void SetScopeProvider(IExternalScopeProvider scopeProvider)
//    {
//        throw new NotImplementedException();
//    }
//}

