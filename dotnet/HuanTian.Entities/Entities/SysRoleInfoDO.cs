using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuanTian.Entities
{
    /// <summary>
    /// 系统角色信息表
    /// </summary>
    [Comment("系统角色信息表")]
    public class SysRoleInfoDO
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// 角色名字
        /// </summary>
        [Required]
        [MaxLength(50)]
        [Comment("角色名字")]
        public string? RoleName { get; set; }
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
