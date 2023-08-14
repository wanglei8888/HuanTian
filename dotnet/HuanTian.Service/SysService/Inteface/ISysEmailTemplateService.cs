#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 文件名：SysEmailTemplateService
 * 代码生成文件
 * 创建时间：2023-08-07 17:04:34
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

using HuanTian.Service.SysService.Dto;

namespace HuanTian.Service;

/// <summary>
/// 系统邮箱模板表服务
/// </summary>
public interface ISysEmailTemplateService
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PageData> Page(SysEmailTemplateInput input);
    /// <summary>
    /// 新增数据
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<int> Add(SysEmailTemplateFormInput input);
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<int> Update(SysEmailTemplateFormInput input);
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
    Task<IEnumerable<SysEmailTemplateDO>> Get(SysEmailTemplateInput input);
}
