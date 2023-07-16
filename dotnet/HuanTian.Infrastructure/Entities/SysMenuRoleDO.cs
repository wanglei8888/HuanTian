using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HuanTian.Infrastructure
{
    /// <summary>
    /// 系统菜单角色表
    /// </summary>
    [Comment("系统菜单角色表")]
    public class SysMenuRoleDO : BaseEntityCreate
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        [Required]
        public long MenuId { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        [Required]
        public long RoleId { get; set; }

    }
}
