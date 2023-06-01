using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HuanTian.Entities
{
    /// <summary>
    /// 系统角色信息表
    /// </summary>
    [Comment("系统角色信息表")]
    public class SysRoleDO : BaseEntityCreate
    {
        /// <summary>
        /// 角色名字
        /// </summary>
        [Required]
        [MaxLength(50)]
        [Comment("角色名字")]
        public string? RoleName { get; set; }
        /// <summary>
        /// 角色描述
        /// </summary>
        [MaxLength(200)]
        [Comment("角色描述")]
        public string Describe { get; set; }
        /// <summary>
        /// 角色启用
        /// </summary>
        [Required]
        [Comment("角色启用")]
        public bool Enable { get; set; }
    }
}
