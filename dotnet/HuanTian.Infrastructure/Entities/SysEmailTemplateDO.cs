using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HuanTian.Infrastructure
{
    /// <summary>
    /// 系统邮箱模板表
    /// </summary>
    [Comment("系统邮箱模板表")]
    public class SysEmailTemplateDO : BaseEntityBusiness
    {
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(50)]
        [Comment("名称")]
        public string Name { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [Comment("是否启用")]
        public bool Enable { get; set; }

    }
}
