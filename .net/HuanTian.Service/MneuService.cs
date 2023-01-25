using HuanTian.Domain;
using HuanTian.EntityFrameworkCore.MySql;
using HuanTian.Interface;
using MathNet.Numerics.Distributions;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IEnumerable<string>> GetUserMenu(int userId)
        {
            //获取用户所包含的权限
            var userRole = await _mySqlContext.SysUserRoleDO.Where(t => t.UserId == userId).ToListAsync();
            var roleMneu = await _mySqlContext.SysMneuRoleDO.Where(t => userRole.Select(s => s.ID).Contains(t.RoleId)).ToListAsync();
            var userMenu = await _mySqlContext.SysMenuDO.Where(t => roleMneu.Select(s => s.MenuId).Contains(t.ID)).ToListAsync();

            
            return null;
        }
    }
}