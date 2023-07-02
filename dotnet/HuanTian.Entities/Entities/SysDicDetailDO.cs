#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 HP NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：WEIHAN
 * 公司名称：HP
 * 命名空间：HuanTian.Entities.Entities
 * 唯一标识：2c5cd21c-fd68-4c5f-ba82-4bde7766730e
 * 文件名：SysDicDetailDO
 * 当前用户域：weihan
 * 
 * 创建者：wanglei
 * 创建时间：2023/5/24 14:16:38
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
using System.ComponentModel.DataAnnotations;

namespace HuanTian.Entities
{
    /// <summary>
    /// 系统字典详情表
    /// </summary>
    [Comment("系统字典详情表")]
    public class SysDicDetailDO : BaseEntityCreate
    {
        /// <summary>
        /// 主表Id
        /// </summary>
        [Required]
        [Comment("主表Id")]
        public long MasterId { get; set; }
        /// <summary>
        /// 字典值
        /// </summary>
        [Required]
        [MaxLength(50)]
        [Comment("字典值")]
        public string Value { get; set; }
        /// <summary>
        /// 字典名字
        /// </summary>
        [Required]
        [MaxLength(50)]
        [Comment("字典名字")]
        public string Name { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Comment("排序")]
        public int Order { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [Comment("是否启用")]
        public bool Enable { get; set; }
    }
}
