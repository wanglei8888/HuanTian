#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：WANGXIAOPANG
 * 公司名称：
 * 命名空间：HuanTian.Service.SysService.Dto
 * 唯一标识：c26b9da3-888f-4a46-bb66-cbbb56ea7ed1
 * 文件名：SysUserInput
 * 当前用户域：WANGXIAOPANG
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/7/15 23:49:18
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

namespace HuanTian.Service
{
    /// <summary>
    /// SysUserOutput
    /// </summary>
    public class SysUserOutput : SysUserDO
    {
        /// <summary>
        /// 权限列表
        /// </summary>
        public IEnumerable<Permission> Role { get; set; }
        /// <summary>
        /// 应用列表
        /// </summary>
        public IEnumerable<SysAppDO> App { get; set; }
        /// <summary>
        /// 菜单列表
        /// </summary>
        public IEnumerable<SysMenuOutput> Menu { get; set; }
    }
}
