#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 HP NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：WEIHAN
 * 公司名称：HP
 * 命名空间：HuanTian.Service.Job
 * 唯一标识：d5dbf5ea-3e36-4285-887a-551595819a3c
 * 文件名：DeleteData
 * 当前用户域：weihan
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/9/6 17:03:53
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

using Hangfire.HttpJob.Agent;
using Hangfire.HttpJob.Agent.Attribute;
using SqlSugar;
using System.Text.Json;

namespace HuanTian.Service.Job
{
    /// <summary>
    /// 定时删除数据状态为 Deleted 得数据
    /// </summary>
    [SingletonJob(RegisterId = "定时删除Deleted数据")]
    public class DeleteDataJob : JobAgent
    {
        private readonly ISqlSugarClient _db;
        public DeleteDataJob(ISqlSugarClient db)
        {
            _db = db;
        }
        public override async Task OnStart(JobContext jobContext)
        {
            var test1 = App.HttpContext.Request;
            var param = JsonSerializer.Deserialize<DeleteDataInput>(jobContext.Param);
            if (string.IsNullOrEmpty(param?.TableName))
            {
                throw new Exception("表格名称不能为空,请修改后再试!");
            }
            var sumCount = 0;
            foreach (var item in param.TableName.Split(","))
            {
                var count = await _db.Deleteable<object>().AS(item).Where("deleted=@value", new { value = true }).ExecuteCommandAsync();
                jobContext.Console.Info($"{item} 删除 {count} 条数据");
                sumCount += count;
            }

            jobContext.Console.Info($"总共删除 {sumCount} 条数据,执行完毕 ");

        }
    }
    public class DeleteDataInput
    {
        public string TableName { get; set; }
    }
}