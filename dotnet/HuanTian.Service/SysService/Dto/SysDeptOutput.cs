﻿#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 文件名：SysDeptOutput
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

using System.Collections;

namespace HuanTian.Service;

/// <summary>
/// 系统部门表输出参数
/// </summary>
public class SysDeptOutput : SysDeptDO
{
    public string ParentName { get; set; }
}
public class SysDeptTreeOutput : SysDeptDO, ITreeBuild
{
    public List<SysDeptTreeOutput> Children { get; set; }

    public long GetId()
    {
        return Id;
    }

    public long GetParentId()
    {
        return ParentId;
    }

    public void SetChildren(IList children)
    {
        Children = (List<SysDeptTreeOutput>)children;
    }
}

