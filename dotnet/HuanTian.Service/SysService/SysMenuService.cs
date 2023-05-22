namespace HuanTian.Service
{
    /// <summary>
    /// 菜单服务
    /// </summary>
    public class SysMenuService : ISysMenuService, IDynamicApiController, IScoped
    {
        private readonly IRepository<SysMenuDO> _menu;
        public SysMenuService(IRepository<SysMenuDO> menu)
        {
            _menu = menu;
        }
        public async Task<List<SysMenuOutput>> GetUserMenu()
        {
            var allMenu = await _menu.OrderBy(t => t.Order)
                .ToListAsync();
            var menuInfo = allMenu.Adapt<List<SysMenuOutput>>();
            return menuInfo;
        }
        [HttpGet]
        public async Task<List<SysMenuTreeOutput>> Page([FromQuery] SysMenuInput input)
        {
            var allMenu = await _menu.ToListAsync();
            var tree = TreeHelper<SysMenuTreeOutput>.DoTreeBuild(allMenu.Adapt<List<SysMenuTreeOutput>>());
            return tree;
        }

    }
}