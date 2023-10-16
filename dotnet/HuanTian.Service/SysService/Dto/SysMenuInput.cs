#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-6BOHDBE
 * 公司名称：
 * 命名空间：HuanTian.Service.SysService.Dto
 * 唯一标识：77009abb-af58-44e6-8b8d-65ba58a24eae
 * 文件名：SysMenuInput
 * 当前用户域：DESKTOP-6BOHDBE
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/5/14 20:15:18
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
    /// SysMenuInput 入参
    /// </summary>
    public class SysMenuInput : IPageInput
    {
        public int PageSize { get; set; }
        public int PageNo { get; set; }
    }
    public class SysMenuTypeInput
    {
        public string MenuType { get; set; }
        public string Name { get; set; }
        public long Id { get; set; }
    }
    public class SysRoleMenuTypeInput
    {
        public string MenuType { get; set; }
        public long RoleId { get; set; }
    }
    public class SysUserMenuInput
    {
        public SysUserMenuInput(long userId)
        {
            UserId = userId;
        }
        public long UserId { get; set; }
    }
}