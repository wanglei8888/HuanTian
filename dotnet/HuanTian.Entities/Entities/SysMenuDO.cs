using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HuanTian.Entities
{
    /// <summary>
    /// 系统菜单表
    /// </summary>
    [Comment("系统菜单表")]
    public class SysMenuDO : BaseEntityCreate
    {
        /// <summary>
        /// 菜单父级ID
        /// </summary>
        [Comment("菜单父级ID")]
        public long ParentId { get; set; }
        /// <summary>
        /// 菜单名字
        /// </summary>
        [MaxLength(50)]
        [Comment("菜单名字")]
        public string? Name { get; set; }
        /// <summary>
        /// 菜单地址
        /// </summary>
        [MaxLength(50)]
        [Comment("菜单地址")]
        public string? Path { get; set; }
        /// <summary>
        /// 菜单标题  多语言使用
        /// </summary>
        [MaxLength(50)]
        [Comment("菜单标题")]
        public string? Title { get; set; }
        /// <summary>
        /// 是否缓存
        /// </summary>
        [Comment("是否缓存")]
        public bool? KeepAlive { get; set; } = false;
        /// <summary>
        /// 图标
        /// </summary>
        [MaxLength(50)]
        [Comment("图标")]
        public string? Icon { get; set; }
        /// <summary>
        /// 是否显示菜单
        /// </summary>
        [Comment("是否显示菜单")]
        public bool Show { get; set; } = true;
        /// <summary>
        /// 菜单跳转地址
        /// </summary>
        [Comment("菜单跳转地址")]
        public string? Redirect { get; set; }
        /// <summary>
        /// 隐藏子类
        /// </summary>
        [Comment("隐藏子类")]
        public bool? HideChildren { get; set; }
        /// <summary>
        /// 菜单类型
        /// </summary>
        [Comment("菜单类型")]
        public string? Component { get; set; }
    }
}
