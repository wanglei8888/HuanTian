#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 HP NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：WEIHAN
 * 公司名称：HP
 * 命名空间：HuanTian.Infrastructure.Comon
 * 唯一标识：ae0640dc-ac73-4961-8a80-f2577bc12603
 * 文件名：LogMQDto
 * 当前用户域：weihan
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/8/24 16:00:31
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

namespace HuanTian.Infrastructure
{
    /// <summary>
    /// 日志队列实体
    /// </summary>
    public class LogMQDto : IMsgQBaseEntity
    {
        /// <summary>
        /// 日志
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 报错日志
        /// </summary>
        public Exception Exception { get; set; }
        public long UserId { get; set; }
        public long TenantId { get; set; }
        /// <summary>
        /// 发生报错的地址
        /// </summary>
        public string Path { get; set; }
        public LogLevel Level { get; set; }
        public DateTime CreateOn { get; set; }
        public string ErrorMsg { get; set; }
    }
}
