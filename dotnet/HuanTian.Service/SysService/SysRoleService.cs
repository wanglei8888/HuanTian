#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 文件名：SysRoleInput
 * 代码生成文件
 * 创建时间：2023-05-30 21:56:41
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

using Newtonsoft.Json;
using SqlSugar.Extensions;

namespace HuanTian.Service;

/// <summary>
/// 系统角色信息表服务
/// </summary>
public class SysRoleService : ISysRoleService, IDynamicApiController, IScoped
{
    private readonly IRepository<SysRoleDO> _sysRole;
    private readonly IRepository<SysRolePermissionsDO> _sysRolePerm;
    private readonly IRepository<SysPermissionsDO> _sysPerm;
    private readonly IRepository<SysMenuDO> _sysMenu;
    private readonly IRepository<SysMenuRoleDO> _sysMenuRole;
    public SysRoleService(
        IRepository<SysRoleDO> sysRole,
        IRepository<SysRolePermissionsDO> sysRolePerm,
        IRepository<SysPermissionsDO> sysPerm,
        IRepository<SysMenuDO> sysMenu,
        IRepository<SysMenuRoleDO> sysMenuRole)
    {
        _sysRole = sysRole;
        _sysRolePerm = sysRolePerm;
        _sysPerm = sysPerm;
        _sysMenu = sysMenu;
        _sysMenuRole = sysMenuRole;
    }

    [HttpGet]
    public async Task<PageData> Page([FromQuery] SysRoleInput input)
    {
        var pageData = await _sysRole
            .WhereIf(!string.IsNullOrEmpty(input.Id), t => t.Id == long.Parse(input.Id))
            .WhereIf(!string.IsNullOrEmpty(input.RoleName), t => t.RoleName.Contains(input.RoleName))
            .WhereIf(!string.IsNullOrEmpty(input.Enable), t => t.Enable == input.Enable.ObjToBool())
            .ToPageListAsync(input.PageNo, input.PageSize);

        // 角色表
        var roleData = pageData.Data.Adapt<List<SysRoleDO>>();
        // 角色权限表
        var rolePermList = await _sysRolePerm
            .ToListAsync(t => roleData.Select(x => x.Id).Contains(t.RoleId));
        // 权限表
        var permList = await _sysPerm
            .ToListAsync(t => rolePermList.Select(x => x.PermissionsId).Contains(t.Id)
                && t.Type == PermissionTypeEnum.Button);
        // 角色菜单表
        var roleMenuList = await _sysMenuRole
            .ToListAsync(t => roleData.Select(x => x.Id).Contains(t.RoleId));
        // 菜单表
        var menuList = await _sysMenu
            .ToListAsync(t => roleMenuList.Select(x => x.MenuId).Contains(t.Id));

        var roleList = new List<Role>();
        // 循环所有角色
        foreach (var roleItem in roleData)
        {
            var data = roleItem.Adapt<Role>();
            var permListData = new List<Permission>();
            // 查询角色所属的菜单
            var roleMenuListItem = roleMenuList.Where(t => t.RoleId == roleItem.Id);
            // 查询角色所属的菜单按钮
            var rolePermListItem = rolePermList.Where(t => t.RoleId == roleItem.Id);
            var permListMenuItem = permList
                .Where(t => rolePermListItem.Select(q => q.PermissionsId).Contains(t.Id)).ToList();
            // 循环角色的菜单
            foreach (var menu in menuList.Where(t => roleMenuListItem.Select(q => q.MenuId).Contains(t.Id)))
            {
                var perm = new Permission();

                var actionList = new List<ActionEntity>();
                foreach (var permItem in permListMenuItem.Where(t => t.MenuId == menu.Id))
                {
                    var model = new ActionEntity();
                    model.Action = permItem.Code;
                    model.Describe = permItem.Name ?? "";
                    model.DefaultCheck = true;
                    actionList.Add(model);
                }
                perm.PermissionName = menu?.Name ?? "";
                perm.actionEntitySet = actionList;
                perm.Actions = permList.Where(t => t.MenuId == menu.Id).Select(t => t.Code).ToJsonString();
                permListData.Add(perm);
            }
            data.Permissions = permListData;
            roleList.Add(data);
        }

        pageData.Data = roleList;
        return pageData;
    }
    public async Task<int> Add(SysRoleFormInput input)
    {
        var entity = input.Adapt<SysRoleDO>();
        var count = await _sysRole.InitTable(entity)
            .CallEntityMethod(t => t.CreateFunc())
            .AddAsync(); ;
        return count;
    }
    public async Task<int> Update(SysRoleFormInput input)
    {
        var entity = input.Adapt<SysRoleDO>();
        var count = await _sysRole.InitTable(entity)
            .UpdateAsync();
        return count;
    }
    public async Task<int> Delete(IdInput input)
    {
        var count = await _sysRole.DeleteAsync(input.Id.Split(',').Adapt<long[]>());
        return count;
    }
    public async Task<IEnumerable<SysRoleDO>> Get([FromQuery] SysRoleInput input)
    {
        var list = await _sysRole
            .WhereIf(!string.IsNullOrEmpty(input.Id), t => t.Id == long.Parse(input.Id))
            .WhereIf(!string.IsNullOrEmpty(input.RoleName), t => t.RoleName == input.RoleName)
            .ToListAsync();
        return list;
    }
    public async Task<int> AddRoleMenu(RoleMenuInput input)
    {
        var permsList = new List<SysMenuRoleDO>();
        foreach (var item in input.MenuId)
        {
            var perms = new SysMenuRoleDO();
            perms.MenuId = item;
            perms.RoleId = long.Parse(input.RoleId);
            permsList.Add(perms);
        }
        // 删除已经存在的数据
        var num = await _sysMenuRole.DeleteAsync(t => t.RoleId == long.Parse(input.RoleId));
        var count = await _sysMenuRole.InitTable(permsList)
            .CallEntityMethod(t => t.CreateFunc())
            .AddAsync();
        return count;
    }
    public async Task<int> AddRolePerms(RolePermsInput input)
    {
        var permsList = new List<SysRolePermissionsDO>();
        foreach (var item in input.PermissionsId)
        {
            var perms = new SysRolePermissionsDO();
            perms.PermissionsId = item;
            perms.RoleId = long.Parse(input.RoleId);
            permsList.Add(perms);
        }
        // 查询按钮的菜单Id
        var perm = await _sysPerm.FirstOrDefaultAsync(t=>t.Id == input.PermissionsId[0]);
        var permList = await _sysPerm
            .Where(t => t.MenuId == perm.MenuId && t.Type == PermissionTypeEnum.Button).ToListAsync();
        // 删除已经存在的数据
        var num = await _sysRolePerm.DeleteAsync(t => t.RoleId == long.Parse(input.RoleId) 
                && permList.Select(q=>q.Id).Contains(t.PermissionsId));
        var count = await _sysRolePerm.InitTable(permsList)
            .CallEntityMethod(t => t.CreateFunc())
            .AddAsync();
        return count;
    }
    /// <summary>
    /// 获取用户权限
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<dynamic> UserRole()
    {
        var jsonString = File.ReadAllText(Path.Combine(App.WebHostEnvironment.WebRootPath, "UserRole.json"));
        var role = JsonConvert.DeserializeObject<List<UserPermission>>(jsonString);
        var pageData = new PageData();
        pageData.Data = role;
        pageData.PageNo = 1;
        pageData.PageSize = 10;
        pageData.TotalCount = 5;
        return await Task.FromResult(role);
    }
}

