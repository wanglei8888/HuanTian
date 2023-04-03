using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuanTian.Entities
{
    /// <summary>
    /// 系统菜单表
    /// </summary>
    [Description("系统菜单表")]
    public class SysMenuDO
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// 菜单父级ID
        /// </summary>
        [Description("菜单父级ID")]
        public int ParentId { get; set; }
        /// <summary>
        /// 菜单名字
        /// </summary>
        [MaxLength(50)]
        [Description("菜单名字")]
        public string? Name { get; set; }

        /// <summary>
        /// 菜单地址
        /// </summary>
        [MaxLength(50)]
        [Description("菜单地址")]
        public string? Path { get; set; }
        /// <summary>
        /// 菜单标题  多语言使用
        /// </summary>
        [MaxLength(50)]
        [Description("菜单标题")]
        public string? Title { get; set; }

        /// <summary>
        /// 是否缓存
        /// </summary>
        [Description("是否缓存")]
        public bool? KeepAlive { get; set; } = false;
        /// <summary>
        /// 图标
        /// </summary>
        [MaxLength(50)]
        [Description("图标")]
        public string? Icon { get; set; }
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
        public DateTime CreateTime { get; set; } = DateTime.Now;
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
        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }
}
