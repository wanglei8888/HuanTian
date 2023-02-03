using HuanTian.Domain;
using HuanTian.DtoModel;
using HuanTian.EntityFrameworkCore.MySql;
using HuanTian.Interface;
using MathNet.Numerics.Distributions;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using System.IO;
using System.Linq;

namespace HuanTian.Service
{
    public class MneuService : IMneuService
    {
        private readonly MySqlContext _mySqlContext;
        public MneuService(MySqlContext mySqlContext)
        {
            _mySqlContext = mySqlContext;
        }
        public async Task<IEnumerable<MenuOutput>> GetUserMenu(int userId)
        {
            //获取用户所包含的权限
            var userRole = await _mySqlContext.SysUserRoleDO.Where(t => t.UserId == userId).ToListAsync();
            var roleMneu = await _mySqlContext.SysMneuRoleDO.Where(t => userRole.Select(s => s.ID).Contains(t.RoleId)).ToListAsync();
            var userMenu = await _mySqlContext.SysMenuDO.Where(t => roleMneu.Select(s => s.MenuId).Contains(t.ID)).ToListAsync();

            var menuOutput = new List<MenuOutput>();

            var menuModelChild2_1 = new MenuOutput { Path = "/dashboard/analysis/:pageNo([1-9]\\d*)?", Name = "Analysis", Meta = new Meta { Title = "menu.dashboard.analysis", KeepAlive = false } };
            var menuModelChild2_2 = new MenuOutput { Path = "/dashboard/workplace", Name = "Workplace", Meta = new Meta { Title = "menu.dashboard.workplace", KeepAlive = false } };

            var menuModelChild = new MenuOutput { Path = "/dashboard", Name = "dashboard", Redirect = "/dashboard/workplace", Component = new Component { Name = "RouteView" }, Meta = new Meta { Title = "menu.dashboard", KeepAlive = true, Icon = "form" },Child = new List<MenuOutput> { menuModelChild2_1 , menuModelChild2_2} };
           
            var menuModelTemplate = new MenuOutput { Path = "/", Name = "index", Meta = new Meta { Title = "menu.home" }, Redirect = "/dashboard/workplace",Child = new List<MenuOutput>{ menuModelChild } };
            menuOutput.Add(menuModelTemplate);
            return menuOutput;
        }
    }
}