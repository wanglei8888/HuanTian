#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 文件名：ISysPermissionsService
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
/// 服务
/// </summary>
public interface ISysPermissionsService
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PageData> Page(SysPermissionsInput input);
    /// <summary>
    /// 新增数据
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<int> Add(List<SysPermissionsFormInput> input);
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<int> Update(SysPermissionsFormInput input);
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
    Task<IEnumerable<SysPermissionsDO>> Get(SysPermissionsInput input);
    /// <summary>
    /// 获取角色权限
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<IEnumerable<SysPermissionsDO>> RolePermission([FromQuery] SysRolePermissionsInput input);
    /// <summary>
    /// 获取用户权限
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<IEnumerable<SysPermissionsDO>> UserPermission(long input);
    /// <summary>
    /// 获取所有菜单权限
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<IEnumerable<SysPermsMenuOutput>> MenuPerms([FromQuery] SysMenuPermsInput input);
}
