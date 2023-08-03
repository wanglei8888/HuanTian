#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 文件名：SysPermissionsOutput
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
/// SysPermissions输出参数
/// </summary>
public class SysPermsMenuOutput : SysPermissionsDO
{
    public long ParentId { get; set; }
    public string MenuName { get; set; }
    public List<SysPermsMenuOutput> Children { get; set; }
}

