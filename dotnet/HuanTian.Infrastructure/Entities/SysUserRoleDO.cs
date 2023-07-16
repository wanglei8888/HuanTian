using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HuanTian.Infrastructure
{
    /// <summary>
    /// 系统用户权限表
    /// </summary>
    [Comment("系统用户权限表")]
    public class SysUserRoleDO: BaseEntityCreate
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required]
        public long UserId { get; set; }
        /// <summary>
        /// 权限ID
        /// </summary>
        [Required]
        public long RoleId { get; set; }
    }
}
