#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-6BOHDBE
 * 公司名称：
 * 命名空间：HuanTian.Service.SysService.Register
 * 唯一标识：9a6ba6ff-f643-45c6-9807-41ca60dcee29
 * 文件名：SysMenuOutput
 * 当前用户域：DESKTOP-6BOHDBE
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/5/14 17:33:24
 * 版本：V1.0.0
 * 描述：
 *
 * ----------------------------------------------------------------
 * 修改人：
 * 时间：
 * 修改说明：
 *
 * 版本：V1.0.1
 *----------------------------------------------------------------*/
#endregion << 版 本 注 释 >>


using HuanTian.Infrastructure;
using System.Collections;

namespace HuanTian.Service
{
    /// <summary>
    /// SysMenu出参
    /// </summary>
    public class SysMenuOutput
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父菜单ID
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 菜单元信息
        /// </summary>
        public Meta Meta { get; set; }

        /// <summary>
        /// 路由组件名称
        /// </summary>
        public string Component { get; set; }

        /// <summary>
        /// 路由重定向地址
        /// </summary>
        public string Redirect { get; set; }

        /// <summary>
        /// 路由路径
        /// </summary>
        public string Path { get; set; }
    }
    public class Meta
    {
        /// <summary>
        /// 菜单标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 是否显示菜单
        /// </summary>
        public bool Show { get; set; } = true;

        /// <summary>
        /// 菜单跳转方式
        /// </summary>
        public string Target { get; set; }
        /// <summary>
        /// 隐藏子类
        /// </summary>
        public bool HideChildren { get; set; }

        /// <summary>
        /// 是否缓存
        /// </summary>
        public bool KeepAlive { get; set; }
    }

    public class SysMenuTreeOutput : SysMenuDO, ITreeBuild
    {
        public List<SysMenuTreeOutput> Children { get; set; }

        public long GetId()
        {
            return Id;
        }

        public long GetParentId()
        {
            return ParentId;
        }

        public void SetChildren(IList children)
        {
            Children = (List<SysMenuTreeOutput>)children;
        }
    }
    
}