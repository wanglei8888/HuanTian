using MathNet.Numerics.Statistics.Mcmc;
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
        public async Task<IEnumerable<SysMenuDO>> Get([FromQuery] SysMenuTypeInput input)
        {
            var allMenu = await _menu
                .WhereIf(!string.IsNullOrEmpty(input.MenuType), t => t.MenuType == input.MenuType)
                .WhereIf(input.Id != 0, t => t.Id == input.Id)
                .ToListAsync();
            var tree = TreeHelper<SysMenuTreeOutput>.DoTreeBuild(allMenu.Adapt<List<SysMenuTreeOutput>>());
            return tree;
        }
        [HttpGet]
        public async Task<IEnumerable<SysMenuDO>> RoleMenu([FromQuery] SysRoleMenuTypeInput input)
        {
            var menuRoleList = await _menuRole.ToListAsync(t => t.RoleId == input.RoleId);
            var allMenu = await _menu
                .WhereIf(!string.IsNullOrEmpty(input.MenuType), t => t.MenuType == input.MenuType)
                .Where(t => menuRoleList.Select(q => q.MenuId).Contains(t.Id))
                .ToListAsync();
            var tree = TreeHelper<SysMenuTreeOutput>.DoTreeBuild(allMenu.Adapt<List<SysMenuTreeOutput>>());
            return tree;
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
        public async Task<List<SysMenuOutput>> GetUserMenu([FromQuery] SysUserMenyInput input)
        {
            // 优先读取入参，否则读取当前登陆账户
            var userId = input.UserId != 0 ? input.UserId : App.GetUserId();
            Expression<Func<SysMenuDO, bool>> menuExpression = t => true;
            // 读取用户角色权限下的所有菜单权限
            var roleId = await _userRole.FirstOrDefaultAsync(t => t.UserId == userId);
            
            // 判断是否是超级管理员  是，就返回所有菜单信息
            if ((await _user.FirstOrDefaultAsync(t => t.Id == userId)).Type != SysUserTypeEnum.SuperAdmin)
            {
                var menuRoleList = await _menuRole.ToListAsync(t => t.RoleId == roleId.RoleId);
                menuExpression = t => menuRoleList.Select(q => q.MenuId).Contains(t.Id);
            }

            var allMenu = await _menu.Where(menuExpression)
                .OrderBy(t => t.Order, false)
                .ToListAsync();
            var menuInfo = allMenu.Adapt<List<SysMenuOutput>>();
            return menuInfo;
        }

    }
}