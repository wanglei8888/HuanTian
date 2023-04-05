using HuanTian.Entities;

namespace HuanTian.Service.SysService
{
    public interface IMneuService
    {
        public Task<IEnumerable<MenuOutput>> GetUserMenu(int userId);
    }
}
