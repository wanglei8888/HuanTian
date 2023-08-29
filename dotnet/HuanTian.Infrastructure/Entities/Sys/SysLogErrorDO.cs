#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 HP NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：WEIHAN
 * 公司名称：HP
 * 命名空间：HuanTian.Infrastructure.Entities
 * 唯一标识：384af495-d097-4250-a931-2f1afeb18043
 * 文件名：SysLogError
 * 当前用户域：weihan
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/8/25 16:53:23
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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace HuanTian.Infrastructure
{
    /// <summary>
    /// 错误日志
    /// </summary>
    [Comment("错误日志")]
    public class SysLogErrorDO
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

        [Comment("错误地址")]
        [MaxLength(50)]
        public string Path { get; set; }
    }
}
