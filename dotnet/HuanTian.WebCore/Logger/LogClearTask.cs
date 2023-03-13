#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 Microsoft NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：MS-SOBOTFLKMEBA
 * 公司名称：Microsoft
 * 命名空间：HuanTian.WebCore.Logger
 * 唯一标识：2adb716b-ef50-42a5-a665-d634c714ddcd
 * 文件名：LogClearTask
 * 当前用户域：MS-SOBOTFLKMEBA
 * 
 * 创建者：wanglei
 * 电子邮箱：
 * 创建时间：2023/3/13 11:58:10
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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuanTian.WebCore
{
    public class LogClearTask : BackgroundService
    {
        private readonly int saveDays;

        public LogClearTask(IOptionsMonitor<LoggerSetting> config)
        {
            saveDays = config.CurrentValue.SaveDays;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {

                    string basePath = Directory.GetCurrentDirectory().Replace("\\", "/") + "/Logs/";

                    if (Directory.Exists(basePath))
                    {
                        //List<string> logPaths = IOHelper.GetFolderAllFiles(basePath).ToList();

                        //var deleteTime = DateTime.UtcNow.AddDays(-1 * saveDays);

                        //if (logPaths.Count != 0)
                        //{
                        //    foreach (var logPath in logPaths)
                        //    {
                        //        var fileInfo = new FileInfo(logPath);

                        //        if (fileInfo.CreationTimeUtc < deleteTime)
                        //        {
                        //            File.Delete(logPath);
                        //        }

                        //    }
                        //}
                    }

                }
                catch
                {
                }

                await Task.Delay(1000 * 60 * 60 * 24, stoppingToken);
            }
        }

    }
}