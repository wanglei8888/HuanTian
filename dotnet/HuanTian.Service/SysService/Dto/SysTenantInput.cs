﻿#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 文件名：SysTenantInput
 * 代码生成文件
 * 创建时间：2023-08-11 17:12:27
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
/// 系统租户表输入参数
/// </summary>
public class SysTenantInput : PageInput
{
    /// <summary>
    /// 租户名字
    /// </summary>
    public string Name { get; set; } 
    public long Id { get; set; }
    public int PageSize { get; set ; }
    public int PageNo { get; set; }
}

/// <summary>
/// 系统租户表表单参数
/// </summary>
public class SysTenantFormInput : SysTenantDO
{
       
}
 

