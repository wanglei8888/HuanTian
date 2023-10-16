#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 文件名：SysDeptInput
 * 代码生成文件
 * 创建时间：2023-07-07 16:56:57
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
public class SysDeptInput : IPageInput
{
    /// <summary>
    /// 父级部门id
    /// </summary>
    public long ParentId { get; set; }
    /// <summary>
    /// 部门名字
    /// </summary>
    public string Name { get; set; } 
    /// <summary>
    /// 部门描述
    /// </summary>
    public string Describe { get; set; } 
    /// <summary>
    /// 部门启用
    /// </summary>
    public string Enable { get; set; } 
    public long Id { get; set; }
    public int PageSize { get; set ; }
    public int PageNo { get; set; }
}

/// <summary>
/// 系统部门表表单参数
/// </summary>
public class SysDeptFormInput : SysDeptDO
{
       
}
 

