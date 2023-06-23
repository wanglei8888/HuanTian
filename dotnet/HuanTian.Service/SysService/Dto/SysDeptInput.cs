#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 文件名：SysDeptInput
 * 代码生成文件
 * 创建时间：2023-06-15 17:15:23
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
/// 系统部门表输入参数
/// </summary>
public class SysDeptInput : PageInput
{
    /// <summary>
    /// Id
    /// </summary>
    public string Id { get; set; } 
    /// <summary>
    /// 父级Id
    /// </summary>
    public string ParentId { get; set; } 
    /// <summary>
    /// 部门名字
    /// </summary>
    public string Name { get; set; } 
}

/// <summary>
/// 系统部门表表单参数
/// </summary>
public class SysDeptFormInput : SysDeptDO
{
       
}
 

