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

namespace HuanTian.Service;

/// <summary>
/// SysRole输出参数
/// </summary>
public class SysRoleOutput : SysRoleDO
{
        
}

public class Permission
{
    /// <summary>
    /// 权限名称
    /// </summary>
    public string PermissionName { get; set; }
    //public long MenuId { get; set; }
    //public string Actions { get; set; }
    /// <summary>
    /// 动作列表
    /// </summary>
    public List<ActionEntity> ActionEntitySet { get; set; }
}

public class ActionEntity
{
    public long Id { get; set; }
    /// <summary>
    /// 动作
    /// </summary>
    public string Action { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string Describe { get; set; }
}

public class Role : SysRoleDO
{
    /// <summary>
    /// 权限列表
    /// </summary>
    public List<Permission> Permissions { get; set; }
}

