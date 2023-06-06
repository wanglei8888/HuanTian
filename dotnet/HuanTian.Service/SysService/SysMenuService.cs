namespace HuanTian.Service
{
    /// <summary>
    /// 菜单服务
    /// </summary>
    public class SysMenuService : ISysMenuService, IDynamicApiController, IScoped
    {
        private readonly IRepository<SysMenuDO> _menu;
        private readonly IRepository<SysMenuRoleDO> _menuRole;
        public SysMenuService(IRepository<SysMenuDO> menu, IRepository<SysMenuRoleDO> menuRole)
        {
            _menu = menu;
            _menuRole = menuRole;
        }
        public async Task<IEnumerable<SysMenuDO>> Get([FromQuery] SysMenuTypeInput input)
        {
            var allMenu = await _menu
                .WhereIf(!string.IsNullOrEmpty(input.MenuType), t => t.MenuType == input.MenuType)
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
        public async Task<List<SysMenuOutput>> GetUserMenu()
        {
            var allMenu = await _menu.OrderBy(t => t.Order,false)
                .ToListAsync();
            var menuInfo = allMenu.Adapt<List<SysMenuOutput>>();
            return menuInfo;
        }

    }
}