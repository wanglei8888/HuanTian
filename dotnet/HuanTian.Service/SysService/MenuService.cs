using HuanTian.Entities;
using HuanTian.EntityFrameworkCore;
using HuanTian.Infrastructure;
using HuanTian.WebCore;
using Newtonsoft.Json;

namespace HuanTian.Service
{
    public class MenuService : IMenuService, IDynamicApiController, IScoped
    {
        private readonly EfSqlContext _mySqlContext;
        public MenuService(EfSqlContext mySqlContext)
        {
            _mySqlContext = mySqlContext;
        }
        public dynamic GetUserMenu()
        {
            ////获取用户所包含的权限
            //var userRole = await _mySqlContext.SysUserRoleDO.Where(t => t.UserId == userId).ToListAsync();
            //var roleMneu = await _mySqlContext.SysMneuRoleDO.Where(t => userRole.Select(s => s.Id).Contains(t.RoleId)).ToListAsync();
            //var userMenu = await _mySqlContext.SysMenuDO.Where(t => roleMneu.Select(s => s.MenuId).Contains(t.Id)).ToListAsync();

            var jsonString = File.ReadAllText(Path.Combine(App.WebHostEnvironment.WebRootPath, "MenuInfo.json"));
                
            var menuInfo = JsonConvert.DeserializeObject<List<MenuItem>>(jsonString);

            return menuInfo;



        }
    }
}