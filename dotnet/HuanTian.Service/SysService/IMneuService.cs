using HuanTian.Entities;

namespace HuanTian.Service
{
    public interface IMneuService
    {
        public Task<IEnumerable<MenuOutput>> GetUserMenu(int userId);
    }
}
