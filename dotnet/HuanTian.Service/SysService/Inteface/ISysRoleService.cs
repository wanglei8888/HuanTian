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

namespace HuanTian.Service;

/// <summary>
/// 系统角色信息表服务
/// </summary>
public interface ISysRoleService
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PageData> Page(SysRoleInput input);
    /// <summary>
    /// 新增数据
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<int> Add(SysRoleFormInput input);
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<int> Update(SysRoleFormInput input);
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
    Task<IEnumerable<SysRoleDO>> Get(SysRoleInput input);
    /// <summary>
    /// 新增角色权限
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<int> AddRolePerms(RolePermsInput input);
    /// <summary>
    /// 新增角色菜单
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<int> AddRoleMenu(RoleMenuInput input);
    /// <summary>
    /// 获取用户角色
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<IEnumerable<SysRoleDO>> UserRole(params long[] userId);
    /// <summary>
    /// 删除用户角色权限
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<int> DeleteUserRole(params long[] userId);
    /// <summary>
    /// 新增用户角色
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<int> AddUserRole(SysUserRoleInput input);
    /// <summary>
    /// 获取角色权限-按钮格式
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<IEnumerable<Role>> RolePermisionButton(IEnumerable<SysRoleDO> input, bool ignoreNull = true);
    /// <summary>
    /// 获取用户角色权限-按钮格式
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<IEnumerable<Role>> UserGetRoleButton(long userId, bool ignoreNull = true);
}
