﻿#region << 版 本 注 释 >>
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
/// SysRole输入参数
/// </summary>
public class SysRoleInput : PageInput
{
    /// <summary>
    /// Id
    /// </summary>
    public string Id { get; set; } 
    /// <summary>
    /// 角色名字
    /// </summary>
    public string RoleName { get; set; }
    public string Enable { get; set; }
}

/// <summary>
/// SysRole表单参数
/// </summary>
public class SysRoleFormInput : SysRoleDO
{
       
}
 

