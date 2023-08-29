using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HuanTian.Infrastructure
{
    /// <summary>
    /// 系统租户表
    /// </summary>
    [Comment("系统租户表")]
    public class SysTenantDO : BaseEntityUpdate
    {
        /// <summary>
        /// 租户管理员
        /// </summary>
        [Comment("租户管理员")]
        public long TenantAdmin { get; set; }
        /// <summary>
        /// 租户名字
        /// </summary>
        [MaxLength(50)]
        [Comment("租户名字")]
        public string Name { get; set; }
        /// <summary>
        /// 邮件配置 用;隔开 例如：2719@qq.com;pwd;smtp.163.com;25;
        /// </summary>
        [MaxLength(100)]
        [Comment("邮件配置")]
        public string EmailConfig { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [Comment("是否启用")]
        public bool Enable { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(100)]
        [Comment("描述")]
        public string? Describe { get; set; }
    }
}
