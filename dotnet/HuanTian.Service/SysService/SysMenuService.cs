using HuanTian.Entities;
using HuanTian.Infrastructure;
using HuanTian.WebCore;
using Mapster;

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
            var allMenu = await _menu.ToListAsync();
            var menuInfo = allMenu.Adapt<List<SysMenuOutput>>();
            var sd = menuInfo.ToJsonString();
            return menuInfo;
        }
    }
}