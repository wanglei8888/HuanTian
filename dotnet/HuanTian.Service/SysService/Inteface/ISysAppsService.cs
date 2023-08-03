#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 文件名：SysAppsService
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
/// 系统应用表服务
/// </summary>
public interface ISysAppsService
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PageData> Page(SysAppsInput input);
    /// <summary>
    /// 新增数据
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<int> Add(SysAppsFormInput input);
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<int> Update(SysAppsFormInput input);
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
    Task<IEnumerable<SysAppsDO>> Get(SysAppsInput input);
}
