#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 文件名：SysAppsInput
 * 代码生成文件
 * 创建时间：2023-07-29 11:03:02
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
/// 系统应用表输入参数
/// </summary>
public class SysAppsInput : PageInput
{
    /// <summary>
    /// 应用名称
    /// </summary>
    public string Name { get; set; } 
    /// <summary>
    /// 应用编码
    /// </summary>
    public string Code { get; set; } 
    public long Id { get; set; }
    public int PageSize { get; set ; }
    public int PageNo { get; set; }
}

/// <summary>
/// 系统应用表表单参数
/// </summary>
public class SysAppsFormInput : SysAppsDO
{
       
}
 

