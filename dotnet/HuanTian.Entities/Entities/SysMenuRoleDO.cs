using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HuanTian.Entities
{
    /// <summary>
    /// 系统菜单权限表
    /// </summary>
    [Comment("系统菜单权限表")]
    public class SysMenuRoleDO : BaseEntityCreate
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        [Required]
        public long MenuId { get; set; }
        /// <summary>
        /// 权限ID
        /// </summary>
        [Required]
        public long RoleId { get; set; }

    }
}
