using System.Linq.Expressions;

namespace HuanTian.Service
{
    /// <summary>
    /// 菜单服务
    /// </summary>
    public class SysMenuService : ISysMenuService, IDynamicApiController, IScoped
    {
        private readonly IRepository<SysUserDO> _user;
        private readonly IRepository<SysMenuDO> _menu;
        private readonly IRepository<SysMenuRoleDO> _menuRole;
        private readonly IRepository<SysUserRoleDO> _userRole;
        public SysMenuService(IRepository<SysMenuDO> menu,
            IRepository<SysMenuRoleDO> menuRole,
            IRepository<SysUserRoleDO> userRole,
            IRepository<SysUserDO> user)
        {
            _menu = menu;
            _menuRole = menuRole;
            _userRole = userRole;
            _user = user;
        }
        [HttpGet]
        public async Task<IEnumerable<SysMenuTreeOutput>> Tree([FromQuery] SysMenuTypeInput input)
        {
            var allMenu = await _menu
                .WhereIf(!string.IsNullOrEmpty(input.Name), t => t.Name.Contains(input.Name))
                .WhereIf(!string.IsNullOrEmpty(input.MenuType), t => t.MenuType == input.MenuType)
                .WhereIf(input.Id != 0, t => t.Id == input.Id)
                .ToListAsync();
            var tree = TreeHelper<SysMenuTreeOutput>.DoTreeBuild(allMenu.Adapt<List<SysMenuTreeOutput>>());
            return tree;
        }
        [HttpGet]
        public async Task<IEnumerable<SysMenuDO>> Get([FromQuery] SysMenuTypeInput input)
        {
            var allMenu = await _menu
                .WhereIf(!string.IsNullOrEmpty(input.MenuType), t => t.MenuType == input.MenuType)
                .WhereIf(input.Id != 0, t => t.Id == input.Id)
                .ToListAsync();
            return allMenu;
        }
        [HttpGet]
        public async Task<IEnumerable<SysMenuDO>> RoleMenu([FromQuery] SysRoleMenuTypeInput input)
        {
            var menuRoleList = await _menuRole.Where(t => t.RoleId == input.RoleId).ToListAsync();
            var allMenu = await _menu
                .WhereIf(!string.IsNullOrEmpty(input.MenuType), t => t.MenuType == input.MenuType)
                .Where(t => menuRoleList.Select(q => q.MenuId).Contains(t.Id))
                .ToListAsync();
            return allMenu;
        }
        public async Task<int> Add(SysMenuDO input)
        {
            // 判断是否存在相同的菜单名称
            var isExist = await _menu.FirstOrDefaultAsync(t => t.Name == input.Name);
            if (isExist != null)
            {
                throw new Exception($"{input.Name}菜单名称已存在,请修改后再试");
            }
            var count = await _menu.InitTable(input)
                .CallEntityMethod(t => t.CreateFunc())
                .AddAsync();
            return count;
        }
        public async Task<int> Delete(IdInput input)
        {
            var count = await _menu.DeleteAsync(input.Id.Split(',').Adapt<long[]>());
            return count;
        }
        public async Task<int> Update(SysMenuDO input)
        {
            var count = await _menu.InitTable(input)
                .UpdateAsync();
            return count;
        }
        public async Task<List<SysMenuOutput>> GetUserMenuOutput([FromQuery] SysUserMenuInput input)
        {
            // 优先读取入参，否则读取当前登陆账户
            var userId = (input == null || input.UserId == 0) ? App.GetUserId() : input.UserId;
            Expression<Func<SysMenuDO, bool>> menuExpression = t => true;
            // 读取用户角色权限下的所有菜单权限
            var roleId = await _userRole.FirstOrDefaultAsync(t => t.UserId == userId);

            if (roleId == null)
            {
                throw new Exception("当前用户没有分配菜单权限，请联系管理员");
            }
            // 判断是否是超级管理员  是，就返回所有菜单信息
            if ((await _user.FirstOrDefaultAsync(t => t.Id == userId)).Type != SysUserTypeEnum.SuperAdmin)
            {
                var menuRoleList = await _menuRole.Where(t => t.RoleId == roleId.RoleId).ToListAsync();
                menuExpression = t => menuRoleList.Select(q => q.MenuId).Contains(t.Id);
            }

            var allMenu = await _menu.Where(menuExpression)
                .OrderBy(t => t.Order, false)
                .ToListAsync();
            var menuInfo = allMenu.Adapt<List<SysMenuOutput>>();

            return menuInfo;
        }
        [NonAction]
        public async Task<IEnumerable<SysMenuDO>> GetUserMenuInfo(long input)
        {
            // 优先读取入参，否则读取当前登陆账户
            var userId = input == 0 ? App.GetUserId() : input;
            Expression<Func<SysMenuDO, bool>> menuExpression = t => true;
            // 读取用户角色权限下的所有菜单权限
            var roleId = await _userRole.FirstOrDefaultAsync(t => t.UserId == userId);

            // 判断是否是超级管理员  是，就返回所有菜单信息
            if ((await _user.FirstOrDefaultAsync(t => t.Id == userId)).Type != SysUserTypeEnum.SuperAdmin)
            {
                var menuRoleList = await _menuRole.Where(t => t.RoleId == roleId.RoleId).ToListAsync();
                menuExpression = t => menuRoleList.Select(q => q.MenuId).Contains(t.Id);
            }

            var allMenu = await _menu.Where(menuExpression)
                .OrderBy(t => t.Order, false)
                .ToListAsync();

            return allMenu;
        }

    }
}