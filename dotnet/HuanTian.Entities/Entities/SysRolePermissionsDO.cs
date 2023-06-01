#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-6BOHDBE
 * 公司名称：
 * 命名空间：HuanTian.Entities.Entities
 * 唯一标识：07820a72-a0ac-441c-bbaf-33d5cc32ddb9
 * 文件名：SysRolePermissionsDO
 * 当前用户域：DESKTOP-6BOHDBE
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/5/29 22:37:22
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


namespace HuanTian.Entities
{
    /// <summary>
    /// 系统角色权限表
    /// </summary>
    public class SysRolePermissionsDO : BaseEntityCreate
    {
        public long RoleId { get; set; }
        public long PermissionsId { get; set; }
    }
}