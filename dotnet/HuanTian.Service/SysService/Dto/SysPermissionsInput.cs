#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 文件名：SysPermissionsInput
 * 代码生成文件
 * 创建时间：2023-06-05 10:29:25
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


namespace HuanTian.Service;

/// <summary>
/// SysPermissions输入参数
/// </summary>
public class SysPermissionsInput : IPageInput
{
    /// <summary>
    /// RoleId
    /// </summary>
    public string Id { get; set; }
    /// <summary>
    /// 权限code
    /// </summary>
    public string Code { get; set; }
    /// <summary>
    /// 权限名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 菜单Id
    /// </summary>
    public string MenuId { get; set; }
    public PermissionTypeEnum Type { get; set; }
    public int PageSize { get; set; }
    public int PageNo { get; set; }
}

public class SysRolePermissionsInput
{
    /// <summary>
    /// RoleId
    /// </summary>
    public long RoleId { get; set; }
    /// <summary>
    /// RoleId
    /// </summary>
    public long MenuId { get; set; }
    public string PermType { get; set; }
}
public class SysMenuPermsInput { 
    public string Type { get; set; }
}
/// <summary>
/// SysPermissions表单参数
/// </summary>
public class SysPermissionsFormInput : SysPermissionsDO
{

}


