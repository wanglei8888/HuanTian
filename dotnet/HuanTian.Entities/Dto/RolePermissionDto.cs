#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-4P8G8RH
 * 公司名称：
 * 命名空间：HuanTian.Entities.DtoModel
 * 唯一标识：2fe11123-f6e6-4e24-97a6-1aa3cbd2ebf5
 * 文件名：Class1
 * 当前用户域：DESKTOP-4P8G8RH
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/4/16 15:45:11
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
    /// 角色权限
    /// </summary>
    public class RolePermissionDto: SysRoleDO
    {
        
        /// <summary>
        /// 权限列表
        /// </summary>
        public List<UserPermission> Permissions { get; set; }
    }

    /// <summary>
    /// 权限
    /// </summary>
    public class UserPermission
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        public string PermissionId { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermissionName { get; set; }

        /// <summary>
        /// 动作列表
        /// </summary>
        public string Actions { get; set; }

        /// <summary>
        /// 动作列表
        /// </summary>
        public List<string> ActionList { get; set; }
    }
}
