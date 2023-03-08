using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using HuanTian.Domain.Base;

namespace HuanTian.Domain
{
    /// <summary>
    /// 系统角色信息表
    /// </summary>
    [Description("系统角色信息表")]
    [Table("SysRoleInfo")]
    public class SysRoleInfoDO
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// 角色名字
        /// </summary>
        [Required]
        [MaxLength(50)]
        [Description("角色名字")]
        public string? RoleName { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [MaxLength(50)]
        [Description("创建人")]
        public string? Creater { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Description("创建时间")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [MaxLength(50)]
        [Description("修改人")]
        public string? Updater { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Description("修改时间")]
        public DateTime UpdateTime { get; set; }
    }
}
