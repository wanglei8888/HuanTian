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

using Yitter.IdGenerator;

namespace HuanTian.Service;


/// <summary>
/// 系统权限服务
/// </summary>
public class SysPermissionsService : ISysPermissionsService, IDynamicApiController, IScoped
{
    private readonly IRepository<SysPermissionsDO> _sysPermissions;
    private readonly IRepository<SysUserRoleDO> _userRole;
    private readonly IRepository<SysRolePermissionsDO> _sysRolePermissions;
    private readonly ISysMenuService _menuService;
    private readonly IRedisCache _redisCache;
    public SysPermissionsService(IRepository<SysPermissionsDO> sysPermissions,
        IRepository<SysRolePermissionsDO> sysRolePermissions,
        IRepository<SysUserRoleDO> userRole,
        IRedisCache redisCache,
        ISysMenuService menuService)
    {
        _sysPermissions = sysPermissions;
        _sysRolePermissions = sysRolePermissions;
        _userRole = userRole;
        _redisCache = redisCache;
        _menuService = menuService;
    }

    [HttpGet]
    public async Task<PageData> Page([FromQuery] SysPermissionsInput input)
    {
        var list = await _sysPermissions
            .WhereIf(!string.IsNullOrEmpty(input.Id), t => t.Id == long.Parse(input.Id))
            .WhereIf(!string.IsNullOrEmpty(input.MenuId), t => t.MenuId == long.Parse(input.MenuId))
            .WhereIf(input.Type != 0, t => t.Type == input.Type)
            .WhereIf(!string.IsNullOrEmpty(input.Code), t => t.Code == input.Code)
            .WhereIf(!string.IsNullOrEmpty(input.Name), t => t.Name == input.Name)
            .ToPageListAsync(input.PageNo, input.PageSize);
        return list;
    }
    public async Task<int> Add(List<SysPermissionsFormInput> input)
    {
        var entityList = input.Adapt<List<SysPermissionsDO>>();
        var count = 0;
        foreach (var item in entityList.GroupBy(t =>  t.MenuId))
        {
            var codeGroup = item.Where(t => t.MenuId == item.Key).GroupBy(t =>new { t.Code ,t.Type});
            // 过滤是否有重复code
            if (codeGroup.Count() != item.Count())
            {
                throw new Exception($"权限编码重复,请修改后再试");
            }
            // 先删除数据
            await _sysPermissions.DeleteAsync(t => t.MenuId == item.Key);
            var codeList = codeGroup.SelectMany(group => group).ToList(); ;
            // 再添加数据
            count += await _sysPermissions.InitTable(codeList)
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
        var count = await _sysPermissions.DeleteAsync(input.Ids);
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
    public async Task<IEnumerable<SysPermsMenuOutput>> MenuPerms([FromQuery] SysMenuPermsInput input)
    {
        var menu = await _menuService.Get(new SysMenuTypeInput());
        var allPerms = await _sysPermissions
            .WhereIf(!string.IsNullOrEmpty(input.Type), t => t.Type == input.Type.ToEnum<PermissionTypeEnum>())
            .ToListAsync();
        var permsMenu = allPerms.Adapt<List<SysPermsMenuOutput>>();
        var output = new List<SysPermsMenuOutput>();
        foreach (var item in permsMenu.GroupBy(t => t.MenuId))
        {
            var model = new SysPermsMenuOutput();
            model.Name = menu.FirstOrDefault(t => t.Id == item.Key).Name;
            model.MenuId = item.Key;
            model.Id = YitIdHelper.NextId();
            model.Children = item.ToList();
            output.Add(model);
        }
        return output;
    }
    [HttpGet]
    public async Task<IEnumerable<SysPermissionsDO>> RolePermission([FromQuery] SysRolePermissionsInput input)
    {
        var rolePermsList = await _sysRolePermissions
            .Where(t => t.RoleId == input.RoleId).ToListAsync();
        var permsList = await _sysPermissions
            .Where(t => rolePermsList.Select(x => x.PermissionsId).Contains(t.Id))
            .WhereIf(!string.IsNullOrEmpty(input.PermType), t => t.Type == input.PermType.ToEnum<PermissionTypeEnum>())
            .WhereIf(input.MenuId != 0, t => t.MenuId == input.MenuId)
            .ToListAsync();
        return permsList;
    }
    [NonAction]
    public async Task<IEnumerable<SysPermissionsDO>> UserPermission(long input)
    {
        // 先从缓存中获取
        var userRouts = await _redisCache.HashGetAsync<IEnumerable<SysPermissionsDO>>(RedisKeyConst.UserRouts, input.ToString());
        if (userRouts == null || !userRouts.Any())
        {
            var userRole = await _userRole
           .Where(t => t.UserId == input).ToListAsync();

            var rolePermsList = await _sysRolePermissions
                .Where(t => userRole.Select(q => q.RoleId).Contains(t.RoleId)).ToListAsync();
            userRouts = await _sysPermissions
                .Where(t => rolePermsList.Select(x => x.PermissionsId).Contains(t.Id))
                .ToListAsync();
            await _redisCache.HashSetAsync(RedisKeyConst.UserRouts, input.ToString(), userRouts.ToJsonString());
        }
        return userRouts;
    }
}