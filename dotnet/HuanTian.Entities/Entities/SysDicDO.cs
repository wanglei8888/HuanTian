#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-6BOHDBE
 * 公司名称：
 * 命名空间：HuanTian.Entities.Entities
 * 唯一标识：cad327be-eb79-4d57-850d-b8f536db893c
 * 文件名：SysDicDO
 * 当前用户域：DESKTOP-6BOHDBE
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/6/29 22:25:48
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
    /// 系统字典表
    /// </summary>
    [Comment("系统字典表")]
    public class SysDicDO: BaseEntity
    {
        /// <summary>
        /// 字典名字
        /// </summary>
        [Required]
        [MaxLength(50)]
        [Comment("字典名字")]
        public string Name { get; set; }
        /// <summary>
        /// 键值
        /// </summary>
        [Required]
        [MaxLength(50)]
        [Comment("系统字典表")]
        public string Code { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [Comment("是否启用")]
        public bool Enable { get; set; }
    }
}