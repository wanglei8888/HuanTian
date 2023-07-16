#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 文件名：SysRoleInput
 * 代码生成文件
 * 创建时间：2023-05-30 21:56:41
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

using System.ComponentModel.DataAnnotations;

namespace HuanTian.Service;

/// <summary>
/// SysRole输入参数
/// </summary>
public class SysRoleInput : PageInput
{
    /// <summary>
    /// RoleId
    /// </summary>
    public string Id { get; set; } 
    /// <summary>
    /// 角色名字
    /// </summary>
    public string RoleName { get; set; }
    public string Enable { get; set; }
    public int PageSize { get; set; }
    public int PageNo { get; set; }
}

/// <summary>
/// SysRole表单参数
/// </summary>
public class SysRoleFormInput : SysRoleDO
{
       
}
/// <summary>
/// 角色菜单入参
/// </summary>
public class RoleMenuInput
{
    [Required(ErrorMessage = "RoleId不能为空")]
    public string RoleId { get; set; }
    [Required(ErrorMessage = "MenuId不能为空")]
    public long[] MenuId { get; set; }
}
/// <summary>
/// 角色菜单入参
/// </summary>
public class RolePermsInput
{
    [Required(ErrorMessage = "RoleId不能为空")]
    public long RoleId { get; set; }
    [Required(ErrorMessage = "PermissionsId不能为空")]
    public long[] PermissionsId { get; set; }
}


