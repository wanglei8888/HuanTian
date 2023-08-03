using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HuanTian.Infrastructure
{
    /// <summary>
    /// 系统应用表
    /// </summary>
    [Comment("系统应用表")]
    public class SysAppsDO : BaseEntityUpdate
    {
        [MaxLength(30)]
        [Comment("列名字")]
        public string Name { get; set; }
        [MaxLength(30)]
        [Comment("编码")]
        public string Code { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Comment("排序")]
        public int Order { get; set; }
        /// <summary>
        /// 启用
        /// </summary>
        [Comment("启用")]
        public bool Enable { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(100)]
        [Comment("描述")]
        public string? Describe { get; set; }
    }
}
