using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuanTian.DtoModel
{
    public class MenuParent
    {
        /// <summary>
        /// 菜单名字
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 菜单地址
        /// </summary>
        public string? Path { get; set; }
        /// <summary>
        /// 根菜单重定位
        /// </summary>
        public string? Redirect { get; set; }

        public string? Component { get; set; }
        public List<MenuParent> Child { get; set; }
        
        public Meta Meta { get; set; }
        
    }

    public class Meta 
    {
        /// <summary>
        /// 菜单标题  多语言使用
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 是否缓存
        /// </summary>
        public bool? KeepAlive { get; set; } = false;
        /// <summary>
        /// 图标
        /// </summary>
        public string? Icon { get; set; }

    }
}
