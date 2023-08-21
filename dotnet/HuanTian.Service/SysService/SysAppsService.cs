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
/// 系统应用表服务
/// </summary>
public class SysAppsService : ISysAppsService, IDynamicApiController, IScoped
{
    private readonly IRepository<SysAppsDO> _sysApps;
    public SysAppsService(IRepository<SysAppsDO> sysApps)
    {
        _sysApps = sysApps;
    }

    [HttpGet]
    public async Task<PageData> Page([FromQuery] SysAppsInput input)
    {
        var list  = await _sysApps
            .WhereIf(!string.IsNullOrEmpty(input.Name), t => t.Name == input.Name)                   
            .WhereIf(!string.IsNullOrEmpty(input.Code), t => t.Code == input.Code)                   
            .ToPageListAsync(input.PageNo,input.PageSize);
        return list;
    }
    public async Task<int> Add(SysAppsFormInput input)
    {
        var entity = input.Adapt<SysAppsDO>();
        var count = await _sysApps.InitTable(entity)
            .CallEntityMethod(t => t.CreateFunc())
            .AddAsync();
        return count;
    }
    public async Task<int> Update(SysAppsFormInput input)
    {
        var entity = input.Adapt<SysAppsDO>();
        var count = await _sysApps.InitTable(entity)
            .UpdateAsync();
        return count;
    }
    public async Task<int> Delete(IdInput input)
    {
        var count = await _sysApps.DeleteAsync(input.Ids);
        return count;
    }
    public async Task<IEnumerable<SysAppsDO>> Get([FromQuery] SysAppsInput input)
    {
        var list  = await _sysApps
            .WhereIf(input.Id != 0, t => t.Id == input.Id)
            .WhereIf(!string.IsNullOrEmpty(input.Name), t => t.Name == input.Name)
            .WhereIf(!string.IsNullOrEmpty(input.Code), t => t.Code == input.Code)
            .ToListAsync();
        return list;
    }
}

