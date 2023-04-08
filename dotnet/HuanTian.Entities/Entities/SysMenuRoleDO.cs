using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuanTian.Entities
{
    /// <summary>
    /// 系统菜单权限表
    /// </summary>
    [Comment("系统菜单权限表")]
    public class SysMenuRoleDO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// 菜单ID
        /// </summary>
        [Required]
        public int MenuId { get; set; }
        /// <summary>
        /// 权限ID
        /// </summary>
        [Required]
        public int RoleId { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [MaxLength(50)]
        [Comment("创建人")]
        public string? Creater { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Comment("创建时间")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [MaxLength(50)]
        [Comment("修改人")]
        public string? Updater { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Comment("修改时间")]
        public DateTime UpdateTime { get; set; }
    }
}
