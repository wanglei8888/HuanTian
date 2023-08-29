#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 HP NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：WEIHAN
 * 公司名称：HP
 * 命名空间：HuanTian.Infrastructure.Entities
 * 唯一标识：231abffa-5d65-4936-b920-f21b2b983913
 * 文件名：SysLogInfo
 * 当前用户域：weihan
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/8/25 16:53:09
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

using SqlSugar;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace HuanTian.Infrastructure
{
    /// <summary>
    /// 普通日志
    /// </summary>
    [Comment("普通日志")]
    public class SysLogInfoDO
    {
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        [Comment("用户ID")]
        public long UserId { get; set; }
        /// <summary>
        /// 租户ID
        /// </summary>
        [Comment("租户ID")]
        public long TenantId { get; set; }

        [Comment("日志等级 trace0、Debug1、Information2、Warning3、Error4、Critical5、None6")]
        public LogLevel Level { get; set; }

        [Comment("日志信息")]
        public string Msg { get; set; }

        [Comment("日志时间")]
        public DateTime CreateOn { get; set; }
    }
}
