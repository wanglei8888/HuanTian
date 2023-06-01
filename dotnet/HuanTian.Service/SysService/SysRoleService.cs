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
using NPOI.SS.Formula.Functions;
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
    public SysRoleService(
        IRepository<SysRoleDO> sysRole,
        IRepository<SysRolePermissionsDO> sysRolePerm,
        IRepository<SysPermissionsDO> sysPerm,
        IRepository<SysMenuDO> sysMenu)
    {
        _sysRole = sysRole;
        _sysRolePerm = sysRolePerm;
        _sysPerm = sysPerm;
        _sysMenu = sysMenu;
    }

    [HttpGet]
    public async Task<PageData> Page([FromQuery] SysRoleInput input)
    {
        var pageData = await _sysRole
            .WhereIf(!string.IsNullOrEmpty(input.Id), t => t.Id == long.Parse(input.Id))
            .WhereIf(!string.IsNullOrEmpty(input.RoleName), t => t.RoleName.Contains(input.RoleName))
            .WhereIf(!string.IsNullOrEmpty(input.Enable), t => t.Enable ==input.Enable.ObjToBool())
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
        // 菜单表
        var menuList = await _sysMenu
            .ToListAsync(t => permList.Select(x => x.MenuId).Contains(t.Id));

        var roleList = new List<Role>();
        // 循环所有角色
        foreach (var item in roleData)
        {
            var data = item.Adapt<Role>();
            var permListData = new List<Permission>();
            var rolePermListItem = rolePermList.FirstOrDefault(t => t.RoleId == item.Id);
            // 按权限菜单分组 循环分组信息
            foreach (var permMenu in permList.GroupBy(t => t.MenuId))
            {
                // 构建返回数据
                var permissionModel = new Permission();
                var permListItem = permList
                    .Where(t => t.MenuId == permMenu.Key).ToList();
                var actionList = new List<ActionEntity>();
                foreach (var permItem in permListItem)
                {
                    var model = new ActionEntity();
                    model.Action = permItem.Code;
                    model.Describe = permItem.Name ?? "";
                    model.DefaultCheck = false;
                    actionList.Add(model);
                }
                permissionModel.PermissionName = menuList.FirstOrDefault(t => t.Id == permListItem[0]?.MenuId)?.Name ?? "";
                permissionModel.Actions = permList.Select(t => t.Code).ToJsonString();
                permissionModel.actionEntitySet = actionList;
                permListData.Add(permissionModel);
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

