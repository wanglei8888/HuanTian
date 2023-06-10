#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-6BOHDBE
 * 公司名称：
 * 命名空间：HuanTian.Entities.Entities
 * 唯一标识：39d1db96-f5ff-4ccd-83d1-7b8ce92d4ac1
 * 文件名：SysPermissionsDO
 * 当前用户域：DESKTOP-6BOHDBE
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/5/29 22:19:39
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
    /// 系统权限表
    /// </summary>
    public class SysPermissionsDO : BaseEntityCreate
    {
        /// <summary>
        /// 绑定系统菜单Id
        /// </summary>
        [Comment("绑定系统菜单Id")]
        public long? MenuId { get; set; }
        /// <summary>
        /// 权限Code
        /// </summary>
        [Required]
        [MaxLength(30)]
        [Comment("权限Code")]
        public string Code { get; set; }
        /// <summary>
        /// 权限名字
        /// </summary>
        [MaxLength(30)]
        [Comment("权限名字")]
        public string? Name { get; set; }
        /// <summary>
        /// 权限类型
        /// </summary>
        [Comment("权限类型")]
        [EnumDataType(typeof(PermissionTypeEnum))]
        public PermissionTypeEnum? Type { get; set; }
    }
    /// <summary>
    /// 权限类型枚举
    /// </summary>
    public enum PermissionTypeEnum
    {
        /// <summary>
        /// 空值
        /// </summary>
        None = 0,
        /// <summary>
        /// 按钮权限
        /// </summary>
        Button,
        /// <summary>
        /// 路由权限
        /// </summary>
        Router
    }
}