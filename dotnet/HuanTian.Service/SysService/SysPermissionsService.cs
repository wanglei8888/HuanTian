#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 文件名：SysPermissionsService
* 代码生成文件
* 创建时间：2023-06-05 10:29:25
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
/// 系统权限服务
/// </summary>
public class SysPermissionsService : ISysPermissionsService, IDynamicApiController, IScoped
{
    private readonly IRepository<SysPermissionsDO> _sysPermissions;
    private readonly IRepository<SysRolePermissionsDO> _sysRolePermissions;
    public SysPermissionsService(IRepository<SysPermissionsDO> sysPermissions, IRepository<SysRolePermissionsDO> sysRolePermissions)
    {
        _sysPermissions = sysPermissions;
        _sysRolePermissions = sysRolePermissions;
    }

    [HttpGet]
    public async Task<PageData> Page([FromQuery] SysPermissionsInput input)
    {
        var list = await _sysPermissions
            .WhereIf(!string.IsNullOrEmpty(input.Id), t => t.Id == long.Parse(input.Id))
            .WhereIf(!string.IsNullOrEmpty(input.MenuId), t => t.MenuId == long.Parse(input.MenuId))
            .WhereIf(!string.IsNullOrEmpty(input.Code), t => t.Code == input.Code)
            .WhereIf(!string.IsNullOrEmpty(input.Name), t => t.Name == input.Name)
            .ToPageListAsync(input.PageNo, input.PageSize);
        return list;
    }
    public async Task<int> Add(List<SysPermissionsFormInput> input)
    {
        var entityList = input.Adapt<List<SysPermissionsDO>>();
        var count = 0;
        foreach (var item in entityList.GroupBy(t => t.MenuId))
        {
            // 先删除数据
            await _sysPermissions.DeleteAsync(t => t.MenuId == item.Key);
            // 再添加数据
            count += await _sysPermissions.InitTable(entityList.Where(t => t.MenuId == item.Key))
                .CallEntityMethod(t => t.CreateFunc())
                .AddAsync();
        }

        return count;
    }
    public async Task<int> Update(SysPermissionsFormInput input)
    {
        var entity = input.Adapt<SysPermissionsDO>();
        var count = await _sysPermissions.InitTable(entity)
        .UpdateAsync();
        return count;
    }
    public async Task<int> Delete(IdInput input)
    {
        var count = await _sysPermissions.DeleteAsync(input.Id.Split(',').Adapt<long[]>());
        return count;
    }
    public async Task<IEnumerable<SysPermissionsDO>> Get([FromQuery] SysPermissionsInput input)
    {
        var list = await _sysPermissions
            .WhereIf(!string.IsNullOrEmpty(input.Id), t => t.Id == long.Parse(input.Id))
            .WhereIf(!string.IsNullOrEmpty(input.MenuId), t => t.MenuId == long.Parse(input.MenuId))
            .WhereIf(!string.IsNullOrEmpty(input.Code), t => t.Code == input.Code)
            .WhereIf(!string.IsNullOrEmpty(input.Name), t => t.Name == input.Name)
            .ToListAsync();
        return list;
    }
    [HttpGet]
    public async Task<IEnumerable<SysPermissionsDO>> RolePermission([FromQuery] SysRolePermissionsInput input)
    {
        var rolePermsList = await _sysRolePermissions
            .Where(t => t.RoleId == input.RoleId).ToListAsync();
        var permsList = await _sysPermissions
            .Where(t => rolePermsList.Select(x => x.PermissionsId).Contains(t.Id))
            .Where(t => t.MenuId == input.MenuId).ToListAsync();
        return permsList;
    }
}