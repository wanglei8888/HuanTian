#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-4P8G8RH
 * 公司名称：
 * 命名空间：HuanTian.Service
 * 唯一标识：1c802320-32e2-493b-81f8-b5a82b66b275
 * 文件名：SysUserService
 * 当前用户域：DESKTOP-4P8G8RH
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/4/5 21:03:30
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

namespace HuanTian.Service
{
    /// <summary>
    /// 用户信息服务
    /// </summary>
    public class SysUserService : ISysUserService, IDynamicApiController
    {
        private readonly ILogger<SysUserService> _logger;
        private readonly IRepository<SysUserDO> _userInfo;
        private readonly IRepository<SysAppDO> _app;
        private readonly ISysRoleService _sysRoleService;
        private readonly ISysMenuService _sysMenuService;

        public SysUserService(
            ILogger<SysUserService> logger,
            IRepository<SysUserDO> userInfo,
            ISysRoleService sysRoleService,
            IRepository<SysAppDO> app,
            ISysMenuService sysMenuService)
        {
            _logger = logger;
            _userInfo = userInfo;
            _sysRoleService = sysRoleService;
            _app = app;
            _sysMenuService = sysMenuService;
        }
        /// <summary>
        /// 获取用户信息跟用户权限信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<dynamic> Info()
        {
            var userId = App.GetUserId();
            // 获取用户信息
            var userInfo = (await _userInfo.FirstOrDefaultAsync(t => t.Id == userId)).Adapt<SysUserOutput>();
            var roleList = await _sysRoleService.UserGetRoleButton(userId);

            // 剔除多个角色重复的权限 如果menuId相同，合并ActionEntitySet
            var distinctPermissionList = roleList
                .SelectMany(roleItem => roleItem.Permissions)
                .GroupBy(t => new { t.PermissionName })
                .Select(t =>
                new Permission
                {
                    PermissionName = t.Key.PermissionName,
                    ActionEntitySet = t.SelectMany(t => t.ActionEntitySet)
                    .DistinctBy(t => t.Id)
                    .ToList()
                });

            userInfo.Role = distinctPermissionList;
            userInfo.App = await _app.OrderBy(t => t.Order).ToListAsync();
            userInfo.Menu = await _sysMenuService.GetUserMenu(null);
            return userInfo;
        }
        public async Task<int> Add(SysUserDO input)
        {
            // 判断是否存在名字
            if ((await _userInfo.FirstOrDefaultAsync(t => t.UserName == input.UserName)) != null)
            {
                throw new Exception("登陆名已存在");
            }
            var count = await _userInfo.InitTable(input)
                .CallEntityMethod(t => t.CreateFunc())
                .AddAsync();
            return count;
        }
        public async Task<int> Update(SysUserDO input)
        {
            // 判断是否存在名字
            if ((await _userInfo.FirstOrDefaultAsync(t => t.UserName == input.UserName)) != null)
            {
                throw new Exception("登陆名已存在");
            }
            var count = await _userInfo.InitTable(input)
                .CallEntityMethod(t => t.UpdateFunc())
                .UpdateAsync();
            return count;
        }
        public async Task<int> Delete(IdInput input)
        {
            var count = await _userInfo.DeleteAsync(input.Id.Split(',').Adapt<long[]>());
            return count;
        }
        /// <summary>
        /// 查询用户列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageData> Page([FromQuery] UserInput input)
        {
            var pageData = await _userInfo
                .WhereIf(!string.IsNullOrEmpty(input.Name), t => t.Name.Contains(input.Name))
                .WhereIf(!string.IsNullOrEmpty(input.UserName), t => t.UserName.Contains(input.UserName))
                .WhereIf(input.DeptId != 0, t => t.DeptId == input.DeptId)
                .WhereIf(!string.IsNullOrEmpty(input.Enable), t => t.Enable == input.Enable.ObjToBool())
                .ToPageListAsync(input.PageNo, input.PageSize);
            return pageData;
        }
    }
}
