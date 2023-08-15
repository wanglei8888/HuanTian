#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-6BOHDBE
 * 公司名称：
 * 命名空间：HuanTian.Infrastructure.Entities
 * 唯一标识：21da3f30-eb54-4977-9d65-10f727f13fc4
 * 文件名：SysDeptDO
 * 当前用户域：DESKTOP-6BOHDBE
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/5/29 21:44:30
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

namespace HuanTian.Infrastructure
{
    /// <summary>
    /// 系统部门表
    /// </summary>
    [Comment("系统部门表")]
    public class SysDeptDO : BaseEntityBusiness
    {
        /// <summary>
        /// 父级部门id
        /// </summary>
        [Comment("父级部门id")]
        public long ParentId { get; set; }
        /// <summary>
        /// 部门名字
        /// </summary>
        [MaxLength(30)]
        [Comment("部门名字")]
        public string Name { get; set; }
        /// <summary>
        /// 部门描述
        /// </summary>
        [MaxLength(200)]
        [Comment("部门描述")]
        public string Describe { get; set; }
        /// <summary>
        /// 部门启用
        /// </summary>
        [Comment("部门启用")]
        public bool Enable { get; set; }
    }
}