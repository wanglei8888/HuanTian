#region << 版 本 注 释 >>
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

using Microsoft.EntityFrameworkCore;
using SqlSugar;

namespace HuanTian.Service;

/// <summary>
/// 系统租户表服务
/// </summary>
public class SysTenantService : ISysTenantService, IDynamicApiController, IScoped
{
    private readonly IRepository<SysTenantDO> _sysTenant;
    private readonly IRepository<SysUserDO> _user;
    private readonly IRedisCache _redisCache;
    public SysTenantService(IRepository<SysTenantDO> sysTenant, IRepository<SysUserDO> user, IRedisCache redisCache)
    {
        _sysTenant = sysTenant;
        _user = user;
        _redisCache = redisCache;
    }
    [HttpGet]
    public async Task<PageData> Page([FromQuery] SysTenantInput input)
    {
        var list = await _sysTenant
                    .WhereIf(!string.IsNullOrEmpty(input.Name), t => t.Name == input.Name)
                    .ToPageListAsync(input.PageNo, input.PageSize);
        // 查询租户管理员名称
        var data = list.Data.Adapt<List<SysTenantOutput>>();
        var userList = await _user.Where(t => data.Select(t => t.TenantAdmin).Contains(t.Id)).ToListAsync();
        foreach (var item in data)
        {
            item.TenantAdminName = userList.FirstOrDefault(t => t.Id == item.TenantAdmin)?.Name;
        }
        list.Data = data;
        return list;
    }
    public async Task<int> Add(SysTenantFormInput input)
    {
        var entity = input.Adapt<SysTenantDO>();
        var count = await _sysTenant.InitTable(entity)
            .CallEntityMethod(t => t.CreateFunc())
            .AddAsync();
        return count;
    }
    public async Task<int> Update(SysTenantFormInput input)
    {
        var entity = input.Adapt<SysTenantDO>();
        var count = await _sysTenant.InitTable(entity)
            .CallEntityMethod(t => t.UpdateFunc())
            .UpdateAsync();
        await _redisCache.StringDeleteAsync(RedisKeyConst.TenantInfo);
        return count;
    }
    public async Task<int> Delete(IdInput input)
    {
        var count = await _sysTenant.DeleteAsync(input.Ids);
        await _redisCache.StringDeleteAsync(RedisKeyConst.TenantInfo);
        return count;
    }
    public async Task<IEnumerable<SysTenantDO>> Get([FromQuery] SysTenantInput input)
    {
        // 从缓存中获取
        var value = await _redisCache.StringGetAsync<IEnumerable<SysTenantDO>>(RedisKeyConst.TenantInfo);
        value = value.WhereIF(input.Id != 0, t => t.Id == input.Id)
                     .WhereIF(!string.IsNullOrEmpty(input.Name), t => t.Name == input.Name);
        if (value == null || !value.Any())
        {
            value = await _sysTenant
            .WhereIf(input.Id != 0, t => t.Id == input.Id)
            .WhereIf(!string.IsNullOrEmpty(input.Name), t => t.Name == input.Name)
            .ToListAsync();
            // 设置缓存
            await _redisCache.StringAddAsync(RedisKeyConst.TenantInfo, value);
        }
        return value;
    }
}

