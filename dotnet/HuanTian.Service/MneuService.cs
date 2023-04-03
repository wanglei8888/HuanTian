using HuanTian.Entities;
using HuanTian.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HuanTian.Service
{
    public class MneuService : IMneuService
    {
        private readonly EfSqlContext _mySqlContext;
        public MneuService(EfSqlContext mySqlContext)
        {
            _mySqlContext = mySqlContext;
        }
        public async Task<IEnumerable<MenuOutput>> GetUserMenu(int userId)
        {
            //获取用户所包含的权限
            var userRole = await _mySqlContext.SysUserRoleDO.Where(t => t.UserId == userId).ToListAsync();
            var roleMneu = await _mySqlContext.SysMneuRoleDO.Where(t => userRole.Select(s => s.Id).Contains(t.RoleId)).ToListAsync();
            var userMenu = await _mySqlContext.SysMenuDO.Where(t => roleMneu.Select(s => s.MenuId).Contains(t.Id)).ToListAsync();

            var menuOutput = new List<MenuOutput>();


            var menuModelTemplate = new MenuOutput
            {
                ParentId = 0,
                Id = 1,
                Name = "dashboard",
                Meta = new Meta { Title = "menu.dashboard", Icon = "dashboard", Show = true },
                Redirect = "/dashboard/workplace",
                Component = "RouteView"
            };
            var menuModelTemplate1 = new MenuOutput
            {
                ParentId = 1,
                Id = 7,
                Name = "workplace",
                Meta = new Meta { Title = "menu.dashboard.monitor", Show = true },
                Component = "Workplace"
            };
            var menuModelTemplate2 = new MenuOutput
            {
                ParentId = 1,
                Path = "https://www.baidu.com/",
                Id = 3,
                Name = "monitor",
                Meta = new Meta { Title = "menu.dashboard.workplace", Show = true }
            };

            menuOutput.Add(menuModelTemplate);
            menuOutput.Add(menuModelTemplate1);
            menuOutput.Add(menuModelTemplate2);
            return menuOutput;
        }
    }
}