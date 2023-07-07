#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 文件名：SysDeptService
 * 代码生成文件
 * 创建时间：2023-07-07 16:56:56
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
/// 系统部门表服务
/// </summary>
public interface ISysDeptService
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PageData> Page(SysDeptInput input);
    /// <summary>
    /// 新增数据
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<int> Add(SysDeptFormInput input);
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<int> Update(SysDeptFormInput input);
    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<int> Delete(IdInput input);
    /// <summary>
    /// 查询数据
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<IEnumerable<SysDeptDO>> Get(SysDeptInput input);
}
