using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using HuanTian.Domain.Base;

namespace HuanTian.Domain
{
    /// <summary>
    /// 系统菜单权限表
    /// </summary>
    [Description("系统菜单权限表")]
    [Table("SysMneuRole")]
    public class SysMneuRoleDO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
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
