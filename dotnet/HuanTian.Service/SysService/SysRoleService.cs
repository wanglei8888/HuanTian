﻿#region << 版 本 注 释 >>
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
    private readonly IRepository<SysUserRoleDO> _sysUserRole;
    public SysRoleService(
        IRepository<SysRoleDO> sysRole,
        IRepository<SysRolePermissionsDO> sysRolePerm,
        IRepository<SysPermissionsDO> sysPerm,
        IRepository<SysMenuDO> sysMenu,
        IRepository<SysMenuRoleDO> sysMenuRole,
        IRepository<SysUserRoleDO> sysUserRole)
    {
        _sysRole = sysRole;
        _sysRolePerm = sysRolePerm;
        _sysPerm = sysPerm;
        _sysMenu = sysMenu;
        _sysMenuRole = sysMenuRole;
        _sysUserRole = sysUserRole;
    }

    [HttpGet]
    public async Task<PageData> Page([FromQuery] SysRoleInput input)
    {
        var pageData = await _sysRole
            .WhereIf(!string.IsNullOrEmpty(input.Id), t => t.Id == long.Parse(input.Id))
            .WhereIf(!string.IsNullOrEmpty(input.RoleName), t => t.RoleName.Contains(input.RoleName))
            .WhereIf(!string.IsNullOrEmpty(input.Enable), t => t.Enable == input.Enable.ObjToBool())
            .ToPageListAsync(input.PageNo, input.PageSize);

        // 角色
        var roleData = pageData.Data.Adapt<List<SysRoleDO>>();
        var roleList = await RolePermisionButton(roleData, false);

        pageData.Data = roleList;
        return pageData;
    }
    public async Task<int> Add(SysRoleFormInput input)
    {
        var entity = input.Adapt<SysRoleDO>();
        var count = await _sysRole.InitTable(entity)
            .CallEntityMethod(t => t.CreateFunc())
            .AddAsync();
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
        var count = await _sysRole.DeleteAsync(input.Ids);
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
            perms.RoleId = input.RoleId;
            permsList.Add(perms);
        }
        // 删除已经存在的数据
        var num = await _sysMenuRole.DeleteAsync(t => t.RoleId == input.RoleId);
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
            perms.RoleId = input.RoleId;
            permsList.Add(perms);
        }
        var permList = await _sysPerm
            .WhereIf(!string.IsNullOrEmpty(input.Type), t => t.Type == input.Type.ToEnum<PermissionTypeEnum>()).ToListAsync();
        // 删除已经存在的数据
        var num = await _sysRolePerm.DeleteAsync(t => t.RoleId == input.RoleId && permList.Select(q => q.Id).Contains(t.PermissionsId));
        var count = await _sysRolePerm.InitTable(permsList)
            .CallEntityMethod(t => t.CreateFunc())
            .AddAsync();
        return count;
    }
    [HttpGet]
    public async Task<IEnumerable<SysRoleDO>> UserRole([FromQuery] params long[] userId)
    {
        if (userId.Length == 0)
        {
            // 不传值就赋默认值
            userId = new long[] { App.GetUserId() };
        }
        // 获取用户角色
        var userRole = await _sysUserRole.Where(t => userId.Contains(t.UserId)).ToListAsync();
        var roleList = await _sysRole.Where(t => userRole.Select(x => x.RoleId).Contains(t.Id)).ToListAsync();
        return roleList;
    }
    [HttpPost("UserRole")]
    public async Task<int> AddUserRole(SysUserRoleInput input)
    {
        var userRoleList = new List<SysUserRoleDO>();
        foreach (var item in input.RoleId)
        {
            var perms = new SysUserRoleDO();
            perms.UserId = input.UserId;
            perms.RoleId = item;
            userRoleList.Add(perms);
        }
        // 删除已经有数据
        var deleteCount = await _sysUserRole.DeleteAsync(t => t.UserId == input.UserId);
        var count = await _sysUserRole.InitTable(userRoleList)
            .CallEntityMethod(t => t.CreateFunc())
            .AddAsync();
        return count;
    }
    [NonAction]
    public async Task<int> DeleteUserRole(params long[] userId)
    {
        var deleteCount = await _sysUserRole.DeleteAsync(t => userId.Contains(t.UserId));
        return deleteCount;
    }
    [NonAction]
    public async Task<IEnumerable<Role>> UserGetRoleButton(long userId, bool ignoreNull = true)
    {
        var roleList = await UserRole(userId);
        return await RolePermisionButton(roleList, ignoreNull);
    }

    /// <summary>
    /// 获取角色权限-按钮格式
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [NonAction]
    public async Task<IEnumerable<Role>> RolePermisionButton(IEnumerable<SysRoleDO> input, bool ignoreNull = true)
    {
        //根据角色信息获取该角色包含的菜单按钮权限

        // 角色权限表
        var rolePermList = await _sysRolePerm
            .Where(t => input.Select(x => x.Id).Contains(t.RoleId))
            .ToListAsync();
        // 权限表
        var permList = await _sysPerm
            .Where(t => rolePermList.Select(x => x.PermissionsId).Contains(t.Id)
                && t.Type == PermissionTypeEnum.Button)
            .ToListAsync();
        // 角色菜单表
        var roleMenuList = await _sysMenuRole
            .Where(t => input.Select(x => x.Id).Contains(t.RoleId))
            .ToListAsync();
        // 菜单表
        var menuList = await _sysMenu
            .Where(t => roleMenuList.Select(x => x.MenuId).Contains(t.Id))
            .ToListAsync();

        var roleList = new List<Role>();
        // 循环所有角色
        foreach (var roleItem in input)
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
                var permListDataItem = permListMenuItem.Where(t => t.MenuId == menu.Id);
                if (!permListDataItem.Any() && ignoreNull)
                {
                    continue;
                }
                foreach (var permItem in permListDataItem)
                {
                    var model = new ActionEntity();
                    model.Action = permItem.Code;
                    model.Describe = permItem.Name ?? "";
                    model.Id = permItem.Id;
                    actionList.Add(model);
                }
                perm.PermissionName = menu?.Name ?? "";
                perm.ActionEntitySet = actionList;
                //perm.MenuId = menu.Id;
                //perm.Actions = permList.Where(t => t.MenuId == menu.Id).Select(t => t.Code).ToJsonString();
                permListData.Add(perm);
            }
            data.Permissions = permListData;
            roleList.Add(data);
        }
        return roleList;
    }
}

